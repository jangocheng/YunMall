using DF.Redis.Cache;
using DF.Tmall.Model;

namespace DF.Tmall.Common.Login {
    public class SessionUtility {
        public static Users GetSession() {
            // 获取用户信息
            var cookie = CookieHelper.GetCookieValue(ConstBaseData.LoginCookieKey);
            Users user = RedisString.GetValue<Users>(cookie);
            return user;
        }
    }
}