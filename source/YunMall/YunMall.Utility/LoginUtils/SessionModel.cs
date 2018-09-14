
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunMall.Utility.LoginUtils
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 父级主键
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>

        public string Email { get; set; }

        /// <summary>
        /// 用户状态：1未审核，2正常，3冻结，4停用，5未通过
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 社区主键(没有地址的为0 有的为默认地址)
        /// </summary>
        public int RegionId { get; set; }
        /// <summary>
        /// 对应的社区主主键
        /// </summary>
        public int comuserId { get; set; }

        /// <summary>
        /// 社区名称
        /// </summary>
        public string Community { get; set; }
        /// <summary>
        /// 用户类型1：零售用户2：商户3：社区主4：便利店
        /// </summary>
        public int UserType { get; set; }
        /// <summary>
        /// 用户唯一注册码
        /// </summary>
        public string UserIdentity { get; set; }

        /// <summary>
        /// 用户头像---于雪雷 2017-4-28
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 通用用户类型
        /// </summary>
        public int CommonType { get; set; }

        /// <summary>
        ///用户类型集合
        /// </summary>

        public IList<int> PersonType { get; set; }


        /// <summary>
        /// 是否有财务权限
        /// </summary>
        public bool Jurisdiction { get; set; }

        /// <summary>
        /// 角色ID --- 孟令辉 2017-07-26添加
        /// </summary>
        public IList<string> R_IDS { get; set; }

        /// <summary>
        /// 是否是小李宁
        /// </summary>
        public bool DownsTream { get; set; }

        /// <summary>
        /// 是否有广大门店 崔萌 2017-10-10 17:23:46
        /// </summary>
        public bool IsGDStore { get; set; }
    }
}
