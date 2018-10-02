using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Web.Http;
using System.Web.Mvc;
using Unity.WebApi;
using System.Web;
using YunMall.Web.BLL.finance;
using YunMall.Web.BLL.order;
using YunMall.Web.BLL.product;
using YunMall.Web.BLL.user;
using YunMall.Web.DAL.finance;
using YunMall.Web.DAL.order;
using YunMall.Web.DAL.product;
using YunMall.Web.IBLL.user;
using YunMall.Web.IDAL.user;
using YunMall.Web.DAL.user;
using YunMall.Web.IBLL.finance;
using YunMall.Web.IBLL.order;
using YunMall.Web.IBLL.product;
using YunMall.Web.IDAL.finance;
using YunMall.Web.IDAL.order;
using YunMall.Web.IDAL.product;
using YunMall.Web.BLL.Facade;
using YunMall.Web.BLL.Facade.impl;

namespace YunMall.Web.Core
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            // 添加Unity拦截器 2017年9月19日21:49:39
            container.AddNewExtension<Interception>();

            // 用户逻辑和仓储接口 2018年9月15日22:22:54
            container.RegisterType<IUserService, UserServiceImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IUserService>(new InterfaceInterceptor());

            container.RegisterType<IUserRepository, UserRepositoryImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IUserRepository>(new InterfaceInterceptor());


            // 权限逻辑和仓储接口 2018年9月16日14:59:03
            container.RegisterType<IPermissionRepository, PermissionRepositoryImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IPermissionRepository>(new InterfaceInterceptor());

            container.RegisterType<IPermissionRelationRepository, PermissionRelationRepositoryImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IPermissionRelationRepository>(new InterfaceInterceptor());


            // 商品发布逻辑和仓储接口 2018年9月20日19:11:49
            container.RegisterType<IProductRepository, ProductRepositoryImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IProductRepository>(new InterfaceInterceptor());

            container.RegisterType<IProductService, ProductServiceImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IProductService>(new InterfaceInterceptor());


            // 经营类目逻辑和仓储接口 2018年9月26日09:43:45
            container.RegisterType<ICategoryRepository, CategoryRepositoryImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<ICategoryRepository>(new InterfaceInterceptor());

            container.RegisterType<ICategoryService, CategoryServiceImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<ICategoryService>(new InterfaceInterceptor());



            // 财务逻辑和仓储接口 2018年9月26日09:43:45
            container.RegisterType<IPaysRepository, PaysRepositoryImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IPaysRepository>(new InterfaceInterceptor());


            container.RegisterType<IAccountsRepository, AccountsRepositoryImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IAccountsRepository>(new InterfaceInterceptor());


            container.RegisterType<IFinanceService, AccountsServiceImpl>("AccountsServiceImpl", new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IFinanceService>(new InterfaceInterceptor());

            container.RegisterType<IFinanceService, PaysServiceImpl>("PaysServiceImpl", new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IFinanceService>(new InterfaceInterceptor());

            container.RegisterType<IPayService, PaysServiceImpl>("PayBusinessServiceImpl", new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IPayService>(new InterfaceInterceptor());
            

            container.RegisterType<IFinanceService, FinanceServiceImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IFinanceService>(new InterfaceInterceptor());


            container.RegisterType<IDictionarysRepository, DictionarysRepositoryImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IDictionarysRepository>(new InterfaceInterceptor());


            container.RegisterType<IWalletRepository, WalletRepositoryImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IWalletRepository>(new InterfaceInterceptor());


            // 订单逻辑和仓储接口 2018年9月26日09:43:45
            container.RegisterType<IOrderService, OrderServiceImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IOrderService>(new InterfaceInterceptor());

            container.RegisterType<IOrderRepository, OrderRepositoryImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IOrderRepository>(new InterfaceInterceptor());

            container.RegisterType<IPayService, PaysServiceImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IPayService>(new InterfaceInterceptor());

            container.RegisterType<IPaysRepository, PaysRepositoryImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IPaysRepository>(new InterfaceInterceptor());

            container.RegisterType<IOrderFacade, OrderFacadeImpl>(new ContainerControlledLifetimeManager())
                .Configure<Interception>().SetInterceptorFor<IOrderFacade>(new InterfaceInterceptor());

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

    }
}