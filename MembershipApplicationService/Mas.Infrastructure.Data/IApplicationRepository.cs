using Mas.Domain.Aggregate;
using Mas.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Mas.Infrastructure.Data
{
    public interface IApplicationRepository
    {
        public Task<Application> GetByIdAsync(Guid id);

        public Task<Application?> GetFirstUnassignedAsync(string userId);
        public Task<List<Application>> GetAsync(ApplicationStatus status);

        public Task<Application> AddAsync(Application entity);

        public Task DeleteAsync(Guid id);

        public Task SaveChangesAsync();
    }
}