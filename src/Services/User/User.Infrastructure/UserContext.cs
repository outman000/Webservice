using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using User.Domain.AggregatesModel.UserAggregates;
using User.Domain.SeedWork.IUnitWork;

namespace User.Infrastructure
{
    public class UserContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "UserTable_Info";
        //表
        public DbSet<UserInfo> UserInfo { get; set; }

        private readonly IMediator _mediator;//中介
        private IDbContextTransaction _currentTransaction;//事务
        /// <summary>
        /// 上下文构造方法
        /// </summary>
        /// <param name="options"></param>
        private UserContext(DbContextOptions<UserContext> options) : base(options) { }
        /// <summary>
        /// 持久化事务接口
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        /// <summary>
        /// 有效事务标记
        /// </summary>
        public bool HasActiveTransaction => _currentTransaction != null;

        /// <summary>
        /// 实体异步存储，他将首先去便利实体相关的领域事件
        /// 在事件统一完成过后一起进行提交处理
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //派遣域事件集合。
            //选择：
            // A）在向数据库提交数据（EF SaveChanges）之前正确进行一次事务，包括
            //来自使用“ InstancePerLifetimeScope”或“作用域”生存期的相同DbContext的域事件处理程序的副作用
            // B）向数据库提交数据（EF SaveChanges）之后，将进行多个事务。
            //如果任何处理程序发生故障，您将需要处理最终的一致性和补偿性措施。 
            await _mediator.DispatchDomainEventsAsync(this);

            //执行此行后的所有更改（来自命令处理程序和域事件处理程序）
            //通过DbContext执行的操作将被提交
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        /// <summary>
        /// 提交此次事务
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                //回滚事务
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

    }
}
