using Autofac;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using User.API.Application.Behaviors;
using User.API.Application.Commands.OperateCommands.UserCommands;
using User.API.Application.IntegrationEvents.EventHandling.UserIntegrationHanding;
using User.API.Application.Validations.UserValide;

namespace Webservice.Services.User.API.Infrastructure.AutofacModules
{
    //中介者们接口与服务注册
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            //注册所有实现了irequesthandler的命令模型 
            builder.RegisterAssemblyTypes(typeof(CreateUserCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));
            //注册所有实现INotificationHandler接口的，事件处理程序

            //builder.RegisterAssemblyTypes(typeof(UserCreatedIntegrationEventHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(INotificationHandler<>));

            //注册所有实现Ivalidator的命令验证模型
            builder
                .RegisterAssemblyTypes(typeof(UserCreateValide).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();


            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));

        }
    }
}
