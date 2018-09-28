using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Unity;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Web.IBLL.finance;
using YunMall.Web.IDAL.finance;

namespace YunMall.Web.BLL.finance {
    public class FinanceServiceImpl : IFinanceService {
        private readonly IAccountsRepository accountsRepository;
        private readonly IPaysRepository paysRepository;
        private readonly IDictionarysRepository dictionarysRepository;

        [InjectionConstructor]
        public FinanceServiceImpl(IAccountsRepository accountsRepository, IPaysRepository paysRepository, IDictionarysRepository dictionarysRepository)
        {
            this.accountsRepository = accountsRepository;
            this.paysRepository = paysRepository;
            this.dictionarysRepository = dictionarysRepository;
        }

        public IList<Accounts> GetAccountPageLimit(int page, string limit, string condition, int type, string beginTime, string endTime)
        {
            throw new NotImplementedException();
        }

        public bool Recharge(int uid, double amount) {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public IList<Accounts> GetPageLimit(int page, string limit, string condition, int type, string beginTime, string endTime)
        {
            throw new NotImplementedException();
        }

        public int GetPageLimitCount(string condition, int type, string beginTime, string endTime)
        {
            throw new NotImplementedException();
        }

        public IList<PaysDetail> GetPayPageLimit(int page, string limit, string condition, int tradeType, int type, string beginTime, string endTime)
        {
            throw new NotImplementedException();
        }

        public int GetPaysPageLimitCount(string condition, int tradeType, int type, string beginTime, string endTime)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string> GetUserAmount(UserDetail userDetail)
        {
            throw new NotImplementedException();
        }
    }
}