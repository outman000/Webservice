using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using User.API.Application.Commands.OperateCommandsHandler.UserCommandsHandler;
using User.API.Application.Queries.IUserQueries;
using User.API.Application.Queries.UserQueries;
using Webservice.BuildingBlocks.EventBus.Abstractions;

namespace Webservice.Services.User.API.Infrastructure.AutofacModules
{
    //应用服务接口于服务注册
    public class ApplicationModule
       : Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {
            //这里注入1对1 接口与服务注册类
            builder.Register(c => new UserQueriesImplement(QueriesConnectionString))
                .As<IUserQueriesInterface>()
                .InstancePerLifetimeScope();


            //注入所有实现了IIntegrationEventHandler接口的命令处理类
            builder.RegisterAssemblyTypes(typeof(CreateUserCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
