using YunMall.Entity.db;
using YunMall.Entity.dbExt;
using YunMall.Entity.enums;

namespace YunMall.Web.IBLL.user {
    /// <summary>
    /// 用户业务逻辑接口
    /// </summary>
    public interface IUserService {

        /// <summary>
        /// 登录 韦德 2018年9月15日22:31:12
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        LoginResult Login(string username, string password, ref UserDetail user);
    }
}