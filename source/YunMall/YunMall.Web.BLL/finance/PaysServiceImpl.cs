using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using DF.Common;
using Microsoft.Practices.Unity;
using YunMall.Entity;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Entity.ModelView;
using YunMall.Web.BLL.util;
using YunMall.Web.Exceptions;
using YunMall.Web.IBLL.finance;
using YunMall.Web.IDAL.finance;
using YunMall.Web.IDAL.user;

namespace YunMall.Web.BLL.finance {
    /// <summary>
    /// 财务交易流水表
    /// </summary>
    public class PaysServiceImpl : BasePageQuery<Pays>, IFinanceService, IPayService
    {

        private readonly IPaysRepository paysRepository;
        private readonly IUserRepository userRepository;
        private readonly IWalletRepository walletRepository;
        private readonly IAccountsRepository accountsRepository;


        [InjectionConstructor]
        public PaysServiceImpl(IPaysRepository paysRepository, IUserRepository userRepository, IWalletRepository walletRepository, IAccountsRepository accountsRepository) {
            this.paysRepository = paysRepository;
            this.userRepository = userRepository;
            this.walletRepository = walletRepository;
            this.accountsRepository = accountsRepository;
        }


        public int GetPaysPageLimitCount(string condition, int tradeType, int type, string beginTime, string endTime) {
            String where = ExtractLimitWhere(condition, type, beginTime, endTime);
            return paysRepository.SelectLimitCount(type, beginTime, endTime, where);
        }

        public override int GetCount()
        {
            return paysRepository.Count();
        }

        public override IList<PaysDetail> GetPayPageLimit(int page, string limit, string condition, int tradeType, int type, string beginTime, string endTime)
        {
            page = ConditionUtil.ExtractPageIndex(page, limit);
            String where = ExtractLimitWhere(condition, tradeType, type, beginTime, endTime);
            IList<PaysDetail> list = paysRepository.SelectLimit(page, limit, tradeType, type, beginTime, endTime, where);
            return list;
        } 

        protected  string ExtractLimitWhere(string condition, int tradeType, int type, string beginTime, string endTime)
        {
            // 查询模糊条件
            String where = " 1=1";

            if (type == 0) {
                if (condition != null && condition.Length > 1) {
                    condition = condition.Trim();
                    where += " AND (" + ConditionUtil.Like("payId", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("systemRecordId", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("fromName", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("toName", condition, true, "t1");
                    if (condition.Split('-').Length == 2)
                    {
                        where += " OR " + ConditionUtil.Like("addTime", condition, true, "t1");
                    }
                    where += " OR " + ConditionUtil.Like("tradeType", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("remark", condition, true, "t1") + ")";
                }

                // 查询全部数据或者只有一类数据
                if (tradeType != 0)
                {
                    where += $" AND t1.tradeType = {tradeType}";
                }
                // 取两个日期之间或查询指定日期
                where = ExtractBetweenTime(beginTime, endTime, where);

            }
            else {
                if (condition != null && condition.Length > 1)
                {
                    condition = condition.Trim();
                    where += " AND (" + ConditionUtil.Like("fromName", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("toName", condition, true, "t1");
                    where += " OR " + ConditionUtil.Like("amount", condition, true, "t1") + ")";
                }
            }
            return where.Trim();
        }

        #region 人工充值

        /// <summary>
        /// 人工充值转账
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool Recharge(int uid, double amount) {
            // 1.封装参数
            IDictionary<string, DbParameter[]> dictionary = new Dictionary<string, DbParameter[]>();
            PayParam payParam = RechargeUtil.GetParam(uid, amount);

            // 2.检查甲方钱包
            var cause = string.Empty;
            if(!CheckFinance(Constants.HotAccountID, amount, ref cause)) throw new MsgException(cause);

            // 3.生成流水账
            Pays payAccounts = RechargeUtil.GetPayAccounts(Constants.HotAccountID, uid, payParam);
            paysRepository.InsertAccounts(payAccounts, ref dictionary);

            // 4.扣减双方钱包余额
            var users = GetUsers(Constants.HotAccountID, uid);
            if(users == null || users.Count < 2) throw new MsgException("加载用户信息超时");
            var owner = users[0];
            var customer = users[1];
            walletRepository.OutAccounts(owner.Uid, amount, owner.Version, ref dictionary);
            walletRepository.PutAccounts(customer.Uid, amount, customer.Version, ref dictionary);

            // 5.生成往来账
            var currentAccounts = GetCurrentAccounts(payParam);
            accountsRepository.BatchInsertAccounts(currentAccounts, ref dictionary);

            return paysRepository.CommitTransactionLock(dictionary);
        }


        /// <summary>
        /// 直接充值
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool DirectRecharge(int uid, double amount)
        {
            // 1.生成参数
            IDictionary<string, DbParameter[]> dictionary = new Dictionary<string, DbParameter[]>();
            PayParam payParam = RechargeUtil.GetDirectParam(uid, amount);

            // 2.生成流水账
            Pays payAccounts = RechargeUtil.GetPayAccounts(Constants.HotAccountID, uid, payParam);
            paysRepository.InsertAccounts(payAccounts, ref dictionary);

            // 3.目标账户进账
            var user = userRepository.SelectFinanceDetail(uid);
            walletRepository.PutAccounts(user.Uid, amount, user.Version, ref dictionary);

            // 4.生成往来账
            var list = new List<Accounts>() {
                new Accounts()
                    {
                        AccountsId = IdWorkTool.Instance().GetId(),
                        PayId = payParam.SystemRecordId,
                        TradeAccountId = payParam.ToUid,
                        TradeAccountName = payParam.ToUsername,
                        AccountsType = 1,
                        Amount = payParam.Amount,
                        Remark = payParam.Remark,
                        Currency = payParam.Currency,
                        AddTime = DateTime.Now
                    }
            }; 
            accountsRepository.BatchInsertAccounts(list, ref dictionary);

            return walletRepository.CommitTransactionLock(dictionary);
        }

        #endregion

        #region 通用模块

        /// <summary>
        /// 查询甲方财务信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>

        private UserFinanceDetail GetOwnerFinanceDetail(int uid) {
            return userRepository.SelectFinanceDetail(uid);
        }

        /// <summary>
        /// 检查甲方财务信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="amount"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        private bool CheckFinance(int uid, double amount, ref string cause) {
            var ownerFinanceDetail = GetOwnerFinanceDetail(uid);
            if (Math.Abs(ownerFinanceDetail.Balance) < 0) cause = "账户已欠费";
            if (Math.Abs(ownerFinanceDetail.Balance) < amount) cause = "账户余额不足";
            if (Math.Abs(ownerFinanceDetail.Balance - amount) < 0) cause = "扣款余额不足";
            if (cause.Length == 0) return true;
            return false;
        }


        /// <summary>
        /// 查询多个用户的资料
        /// </summary>
        /// <param name="uids"></param>
        /// <returns></returns>
        private IList<UserFinanceDetail> GetUsers(params int[] uids) {
            IList<UserFinanceDetail>  list = userRepository.SelectFinanceDetails(uids);
            return list;
        }

        /// <summary>
        /// 生成往来账
        /// </summary>
        /// <param name="payParam"></param>
        /// <returns></returns>
        private IList<Accounts> GetCurrentAccounts(PayParam payParam) {
            // 生成甲方账单
            Accounts owner = new Accounts() {
                AccountsId = IdWorkTool.Instance().GetId(),
                PayId = payParam.SystemRecordId,
                TradeAccountId = payParam.FromUid,
                TradeAccountName = payParam.FromUsername,
                AccountsType = 2,
                Amount = payParam.Amount,
                Remark = payParam.Remark == null ? "借" : payParam.Remark,
                Currency = payParam.Currency,
                AddTime = DateTime.Now
            };

            // 生成乙方账单
            Accounts customer = new Accounts()
            {
                AccountsId = IdWorkTool.Instance().GetId(),
                PayId = payParam.SystemRecordId,
                TradeAccountId = payParam.ToUid,
                TradeAccountName = payParam.ToUsername,
                AccountsType = 1,
                Amount = payParam.Amount,
                Remark = payParam.Remark == null ? "贷" : payParam.Remark,
                Currency = payParam.Currency,
                AddTime = DateTime.Now
            };

            var list = new List<Accounts>();
            list.Add(owner);
            list.Add(customer);
            return list;
        }

        #endregion

        #region 充值模块

        class RechargeUtil
        {

            public static PayParam GetParam(int uid, double amount)
            {
                return new PayParam()
                {
                    Amount = amount,
                    Currency = 0,
                    FromUid = Constants.HotAccountID,
                    ToUid = uid,
                    Remark = "人工充值"
                };
            }

            public static PayParam GetDirectParam(int uid, double amount)
            {
                return new PayParam()
                {
                    Amount = amount,
                    Currency = 0,
                    FromUid = Constants.HotAccountID,
                    ToUid = Constants.HotAccountID,
                    Remark = "直接充值"
                };
            }

            public static Pays GetPayAccounts(int fromUid, int toUid, PayParam payParam) {
                payParam.SystemRecordId = IdWorkTool.Instance().GetId();
                Pays pays = new Pays()
                {
                    PayId = payParam.SystemRecordId,
                    FromUid = fromUid,
                    ToUid = toUid,
                    ChannelType = Constants.DynamicMap.DefaultChannelType,
                    ProductType = Constants.DynamicMap.DefaultProductType,
                    TradeType = Constants.DynamicMap.RechargeTradeType,
                    Remark = payParam.Remark,
                    Amount = payParam.Amount,
                    Status = 0,
                    SystemRecordId = IdWorkTool.Instance().GetId()
                };
                return pays;
            }
             
        }

        #endregion

        #region 支付相关
        /// <summary>
        /// 验证支付密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="security"></param>
        /// <param name="cause"></param>
        /// <returns></returns>
        public bool CheckSecurityPassword(User user, string security, ref string cause) {
            // 1.安全hash校对
            var securityPassword = user.SecurityPassword;
            var nSecurityPassword = MD5Encrypt.MD5(MD5Encrypt.MD5(user.Username + security));
            if (securityPassword != nSecurityPassword){
                cause = "支付密码不正确";
            }
            else {
                return true;
            }
            return false;
        }

        #endregion

        #region 借贷转账

        /// <summary>
        /// 借贷转账
        /// </summary>
        /// <param name="fromUid"></param>
        /// <param name="toUid"></param>
        /// <param name="amount"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public void Transfer(int fromUid, int toUid, double amount, string remark, ref IDictionary<string, DbParameter[]> dictionary)
        {
            // 1.封装参数
            PayParam payParam = new PayParam()
            {
                Amount = amount,
                Currency = 0,
                FromUid = fromUid,
                ToUid = toUid,
                Remark = remark == null ? "转账" : remark
            };

            // 2.检查甲方钱包
            var cause = string.Empty;
            if (!CheckFinance(fromUid, amount, ref cause)) throw new MsgException(cause);

            // 3.生成流水账
            Pays payAccounts = RechargeUtil.GetPayAccounts(fromUid, toUid, payParam);
            paysRepository.InsertAccounts(payAccounts, ref dictionary);

            // 4.扣减双方钱包余额
            var users = GetUsers(fromUid, toUid);
            if(fromUid == toUid) throw new MsgException("您不能购买自己发布的商品");
            if (users == null || users.Count < 2) throw new MsgException("加载用户信息超时");
            var owner = users.First(item => item.Uid == fromUid);
            var customer = users.First(item => item.Uid == toUid);
            walletRepository.OutAccounts(owner.Uid, amount, owner.Version, ref dictionary);
            walletRepository.PutAccounts(customer.Uid, amount, customer.Version, ref dictionary);

            // 5.生成往来账
            var currentAccounts = GetCurrentAccounts(payParam);
            accountsRepository.BatchInsertAccounts(currentAccounts, ref dictionary);
        }



        #endregion
    }
}