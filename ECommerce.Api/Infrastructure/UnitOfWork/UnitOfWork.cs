using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Repository;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ECommerce.Api.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private IClientSessionHandle _session;

        public ICustomerRepository Customers { get; }
        public IOrderRepository Orders { get; }
        public IProductRepository Products { get; }
        public IUserRepository Users { get; }
        public IClientSessionHandle Session => _session;

        public UnitOfWork(IOptions<MongoDBSettings> settings,
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IUserRepository userRepository)
        {
            _client = new MongoClient(settings.Value.ConnectionString);
            _database = _client.GetDatabase(settings.Value.DatabaseName);
            Customers = customerRepository;
            Orders = orderRepository;
            Products = productRepository;
            Users = userRepository;
        }

        public async Task StartSessionAsync()
        {
            _session = await _client.StartSessionAsync();
            Session.StartTransaction();
        }
        public async Task CommitTransactionAsync()
        {
            if (_session == null) return;
            await _session.CommitTransactionAsync();
        }
        public async Task RollbackAsync()
        {
            if (_session == null) return;
            await _session.AbortTransactionAsync();
        }
        public void Dispose()
        {
            _session?.Dispose();
        }
    }
}