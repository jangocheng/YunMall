using System.Collections.Generic;
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

        /// <summary>
        /// 注册 韦德 2018年9月17日17:51:18
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        RegisterResult Register(string username, string password, string contact);


        /// <summary>
        /// 通用分页查询 韦德 2018年9月22日16:11:04
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="condition"></param>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        IList<ProductDetail> GetLimit(int page, string limit, string condition, int state, string beginTime, string endTime);


        /// <summary>
        /// 查询总数 韦德 2018年9月22日16:12:26
        /// </summary>
        /// <returns></returns>
        int GetCount();


        /// <summary>
        /// 查询分页总数 韦德 2018年9月22日16:12:21
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="state"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        int GetLimitCount(string condition, int state, string beginTime, string endTime);

    }
}