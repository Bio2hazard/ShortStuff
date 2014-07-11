// ShortStuff.Web
// NinjectWebCommon.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using ShortStuff.Domain;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Services;
using ShortStuff.Repository;
using ShortStuff.Web;
using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof (NinjectWebCommon), "Stop")]

namespace ShortStuff.Web
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        ///     Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        ///     Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        ///     Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>()
                      .ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>()
                      .To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        ///     Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUserService>()
                  .To<UserService>()
                  .InRequestScope();

            kernel.Bind<IMessageService>()
                  .To<MessageService>()
                  .InRequestScope();

            kernel.Bind<IEchoService>()
                  .To<EchoService>()
                  .InRequestScope();

            kernel.Bind<INotificationService>()
                  .To<NotificationService>()
                  .InRequestScope();

            kernel.Bind<ITopicService>()
                  .To<TopicService>()
                  .InRequestScope();

            kernel.Bind<IUnitOfWork>()
                  .To<UnitOfWork>()
                  .InRequestScope();

            kernel.Bind<IRepository<User, decimal>>()
                  .To<UserRepository>()
                  .InRequestScope();

            kernel.Bind<IRepository<Message, int>>()
                  .To<MessageRepository>()
                  .InRequestScope();

            kernel.Bind<IRepository<Echo, int>>()
                  .To<EchoRepository>()
                  .InRequestScope();

            kernel.Bind<IRepository<Notification, int>>()
                  .To<NotificationRepository>()
                  .InRequestScope();

            kernel.Bind<IRepository<Topic, int>>()
                  .To<TopicRepository>()
                  .InRequestScope();
        }
    }
}
