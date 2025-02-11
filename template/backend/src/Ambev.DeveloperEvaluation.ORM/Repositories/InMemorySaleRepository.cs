using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class InMemorySaleRepository : ISaleRepository
    {
        private readonly List<Sale> _sales = [];
        public Task CreateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _sales.Add(sale);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            var index = _sales.FindIndex(s => s.Id == sale.Id);
            if (index != -1)
            {
                _sales[index] = sale;
            }
            else
            {
                _sales.Add(sale);
            }
            return Task.FromResult(true);
        }

        public Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_sales.FirstOrDefault(s => s.Id == id));
        }

        public Task<List<Sale>?> GetAllAsync(int? skip, int? take, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_sales.Skip(skip ?? 0).Take(take ?? 20).ToList() ?? null);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var index = _sales.FindIndex(s => s.Id == id);
            if (index != -1)
            {
                _sales.RemoveAt(index);
            }
            return Task.FromResult(true);
        }
    }
}