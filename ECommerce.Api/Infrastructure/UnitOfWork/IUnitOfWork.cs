using ECommerce.Shared.Dtos;
using ECommerce.Api.Entities;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using ECommerce.Api.Interface.IRepository;

namespace ECommerce.Api.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IUserRepository Users { get; }

        Task StartSessionAsync();
        Task CommitTransactionAsync();
        Task RollbackAsync();
        IClientSessionHandle Session { get; }
    }
}