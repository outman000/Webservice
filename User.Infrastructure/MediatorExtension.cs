using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.SeedWork.BaseEntity;

namespace User.Infrastructure
{
    static class MediatorExtension
    {
        //事务异步派遣拓展方法
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, UserContext ctx)
        {
            //取出用户上下文中，实体正在变更的状态的领域事件集合
            //首先跟踪所有变更的实体
            var domainEntities = ctx.ChangeTracker
               // 获取上下文跟踪的每个实体的实体条目
                .Entries<Entity>()
                //条件，领域事件不为空的变更实体
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());


            //将所有跟踪到的实体的领域事件集合提取出来
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            //清空跟踪到的实体的领域事件
            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            //调用所有实体中的领域事件
            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
