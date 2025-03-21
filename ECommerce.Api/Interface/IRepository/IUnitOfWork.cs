using ECommerce.Api.Dtos;
using ECommerce.Api.Entities;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace ECommerce.Api.Interface.IRepository
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
