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
        /// 根据id查询用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUserById(int id);

        /// <summary>
        /// 注册 韦德 2018年9月17日17:51:18
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        RegisterResult Register(string username, string password, string contact);

        /// <summary>
        /// 根据用户名查询用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        User GetUserByName(string username);
    }
}