using Autofac.Core;
using EasyTime.InfraStracure.Context;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace EasyTime.InfraStracure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Begin();
        Task Commit();
        Task RoleBack();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly EasyTimeContext context;
        private readonly IDbContextTransaction transaction;

        public UnitOfWork(EasyTimeContext context)
        {
            this.context = context;
        }

        public async Task Begin()
        {
             context.Database.BeginTransaction();
        }

        public async Task Commit()
        {
            context.Database.CommitTransaction();
        }

        public async Task RoleBack()
        {
            context.Database.RollbackTransaction();
        }

    }
}
