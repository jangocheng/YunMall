using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunMall.Entity.enums
{
    public enum LoginResult
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        L00000,

        /// <summary>
        /// 异常
        /// </summary>
        L00001,

        /// <summary>
        /// 用户不存在
        /// </summary>
        L00002,

        /// <summary>
        /// 密码错误
        /// </summary>
        L00003,

        /// <summary>
        /// 冻结/禁止登录
        /// </summary>
        L00004,

        /// <summary>
        /// 账号或密码填写格式不正确
        /// </summary>
        L00005,

        /// <summary>
        /// 非法权限
        /// </summary>
        L00006,
    }
}
