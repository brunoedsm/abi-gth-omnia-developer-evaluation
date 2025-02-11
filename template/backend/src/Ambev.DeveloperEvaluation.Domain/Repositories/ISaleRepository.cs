using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task CreateAsync(Sale sale, CancellationToken cancellationToken = default);

        Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<List<Sale>?> GetAllAsync(int? skip, int? take, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
