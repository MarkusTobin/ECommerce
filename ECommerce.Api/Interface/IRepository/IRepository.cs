﻿using MongoDB.Driver;

namespace ECommerce.Api.Interface.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task CreateAsync(T entity);
        Task UpdateAsync(string id, T entity);
        Task DeleteAsync(string id);
        void UpdateSession(IClientSessionHandle session);
    }
}
