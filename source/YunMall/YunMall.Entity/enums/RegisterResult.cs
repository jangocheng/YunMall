using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunMall.Entity.enums
{
    public enum RegisterResult
    {
        /// <summary>
        /// 注册成功
        /// </summary>
        R00000,

        /// <summary>
        /// 异常
        /// </summary>
        R00001,

        /// <summary>
        /// 账号或密码填写格式不正确
        /// </summary>
        R00002,


        /// <summary>
        /// 用户名已存在
        /// </summary>
        R00003
    }
}
