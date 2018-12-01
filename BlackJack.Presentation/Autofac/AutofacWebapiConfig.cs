﻿using Autofac;
using Autofac.Integration.WebApi;
using BlackJack.BusinessLogic.Services;
using DataAccess;
using DataAccess.DapperModels;
using System.Reflection;
using System.Web.Http;

namespace BlackJack.Presentation.Config
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<BaseRepository<Player>>()
                    .WithParameter("tableName", "Players")
                    .WithParameter("connectionFactory", new ConnectionFactory())
                    .WithParameter("nameOrConnectionString", "BlackJackContainerConnectionString")
                    .InstancePerRequest();

            builder.RegisterType<BaseRepository<Game>>()
                    .WithParameter("tableName", "Games")
                    .WithParameter("connectionFactory", new ConnectionFactory())
                    .WithParameter("nameOrConnectionString", "BlackJackContainerConnectionString")
                    .InstancePerRequest();

            builder.RegisterType<BaseRepository<Round>>()
                    .WithParameter("tableName", "Rounds")
                    .WithParameter("connectionFactory", new ConnectionFactory())
                    .WithParameter("nameOrConnectionString", "BlackJackContainerConnectionString")
                    .InstancePerRequest();

            builder.RegisterType<BaseRepository<RoundPlayer>>()
                    .WithParameter("tableName", "RoundPlayers")
                    .WithParameter("connectionFactory", new ConnectionFactory())
                    .WithParameter("nameOrConnectionString", "BlackJackContainerConnectionString")
                    .InstancePerRequest();

            builder.RegisterType<PlayerService>()
                    .InstancePerRequest();

            builder.RegisterType<GameService>()
                   .InstancePerRequest();

            builder.RegisterType<RoundService>()
                   .InstancePerRequest();

            builder.RegisterType<RoundPlayerService>()
                   .InstancePerRequest();

            builder.RegisterType<GameLogicService>()
                  .InstancePerRequest();


            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }
    }
}