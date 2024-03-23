using Mas.Domain.Aggregate;
using Mas.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Mas.Infrastructure.Data
{
    public class ApplicationRepository:IApplicationRepository
    {
        private readonly ApplicationContext _dbContext;
        public ApplicationRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Application> GetByIdAsync(Guid id)
        {
            return await _dbContext.Applications.Include(i => i.ReferralProcessInfo).SingleOrDefaultAsync(e => e.Id.Equals(id));
        }
        public async Task<Application> AddAsync(Application entity)
        {
            await _dbContext.Set<Application>().AddAsync(entity);
            
            return entity;
        }

        public async Task<Application?> GetFirstUnassignedAsync(string userId)
        {
            var application = await _dbContext.Set<Application>()
                .Where(a => a.Status == ApplicationStatus.Assigned && a.AssignToUserId==userId)
                .OrderBy(x=> x.DateInitiated).FirstOrDefaultAsync();
            if(application!= null)
                return application;

            return await _dbContext.Set<Application>()
                .Where(a => a.Status == ApplicationStatus.Unassigned)
                .OrderBy(x => x.DateInitiated).FirstOrDefaultAsync();

        }

        public async Task<List<Application>> GetAsync(ApplicationStatus status)
        {
            return await _dbContext.Set<Application>().Where(a=> a.Status== status).ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var application = await _dbContext.Applications.Include(i => i.ReferralProcessInfo).SingleOrDefaultAsync(e => e.Id.Equals(id));
            _dbContext.Set<Application>().Remove(application);
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();

        }
    }
}
