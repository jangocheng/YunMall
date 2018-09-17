﻿using System;
using System.Collections;
using System.Linq;
using DF.Common;
using DF.Common.StringHelper;
using Microsoft.Practices.Unity;
using YunMall.Entity.db;
using YunMall.Entity.dbExt;
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
        private readonly IPermissionRepository permissionRepository;
        private readonly IPermissionRelationRepository permissionRelationRepository;

        [InjectionConstructor]
        public UserServiceImpl(IUserRepository userRepository, IPermissionRepository permissionRepository, IPermissionRelationRepository permissionRelationRepository)
        {
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
            this.permissionRelationRepository = permissionRelationRepository ?? throw new ArgumentNullException(nameof(permissionRelationRepository));
        }



        /// <summary>
        /// 登录 韦德 2018年9月15日22:31:12
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public LoginResult Login(string username, string password, ref UserDetail user) {
            if (username.IsEmpty() || password.IsEmpty()
                || !Util.LengthValid(username) || !Util.LengthValid(password)) return LoginResult.L00005;

            // 查询用户基础信息
            QueryParam queryParam = new QueryParam();
            queryParam.StrWhere = $"username = '{username}' AND password = '{MD5Encrypt.MD5(MD5Encrypt.MD5(username + password))}'";
            var list = userRepository.Query<User>(queryParam);
            if (!(list != null && list.Count > 0)) return LoginResult.L00001;
            user = new UserDetail
            {
                User = list.First()
            };

            // 查询用户权限列表
            var permissions = permissionRepository.SelectList(user.User.Uid);
            if (!(permissions != null && permissions.Count > 0)) return LoginResult.L00006;
            user.Permissions = permissions;

            // 查询上级用户
            if (!user.User.ParentId.IsEmpty() || user.User.ParentId.Length > 0) {
                queryParam = new QueryParam();
                queryParam.StrWhere = $"uid IN({user.User.ParentId})";
                queryParam.OrderBy = "depth DESC";
                var parentUsers = userRepository.Query<User>(queryParam);
                if (parentUsers != null && parentUsers.Count > 0) {
                    user.ParentUsers = parentUsers;
                }
            }

            return LoginResult.L00000;
        }

        /// <summary>
        /// 注册 韦德 2018年9月17日17:51:18
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        public RegisterResult Register(string username, string password, string contact) {
            if (username.IsEmpty() || password.IsEmpty() || contact.IsEmpty()
                                   || !Util.LengthValid(username) || !Util.LengthValid(password)) return RegisterResult.R00002;
            var hsTable = new Hashtable();

            // 1.添加新会员
            var pk = AddUser(username, password, contact);
            if(pk == 0) return RegisterResult.R00001;

            // 2.关联身份权限
            hsTable = AddPermissionRelation(pk, hsTable);

            // 3.无锁事务提交
            userRepository.CommitTransaction(hsTable);

            return RegisterResult.R00000;
        }

        private Hashtable AddPermissionRelation(long pk, Hashtable hsTable) {
            permissionRelationRepository.Insert(new PermissionRelations() {
                Uid = Convert.ToInt32(pk),
                PermissionList = "4"
            }, ref hsTable);
            return hsTable;
        }

        private long AddUser(string username, string password, string contact) {
            long pk = 0L;
            userRepository.InsertReturn(new User() {
                Username = username,
                Password = MD5Encrypt.MD5(MD5Encrypt.MD5(username + password)),
                QQ = contact
            }, ref pk);
            return pk;
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