using System;
using System.Linq;
using DF.Common;
using DF.Common.StringHelper;
using Microsoft.Practices.Unity;
using YunMall.Entity.db;
using YunMall.Entity.enums;
using YunMall.Entity.ModelView;
using YunMall.Web.IBLL.user;
using YunMall.Web.IDAL.user;

namespace YunMall.Web.BLL.user {
    /// <summary>
    /// 用户业务逻辑接口实现类
    /// </summary>
    public class UserServiceImpl : IUserService {

        private readonly IUserRepository userRepository;

        [InjectionConstructor]
        public UserServiceImpl(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// 登录 韦德 2018年9月15日22:31:12
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public LoginResult Login(string username, string password, ref User user) {
            user = null;
            if (username.IsEmpty() || password.IsEmpty()
                || !Util.LengthValid(username) || !Util.LengthValid(password)) return LoginResult.L00005;


            QueryParam queryParam = new QueryParam();
            queryParam.StrWhere = $"username = '{username}' AND password = '{MD5Encrypt.MD5(MD5Encrypt.MD5(username + password))}'";


            var list = userRepository.Query<User>(queryParam);
            if (list != null && list.Count > 0) {
                user = list.First();
                return LoginResult.L00000;
            }

            return LoginResult.L00001;
        }

        class Util {
            /// <summary>
            /// 长度验证
            /// </summary>
            /// <param name="input"></param>
            /// <returns></returns>
            public static bool LengthValid(string input) {
                if (input.Length >= 5 && input.Length <= 18) return true;
                return false;
            }
        }
    }
}