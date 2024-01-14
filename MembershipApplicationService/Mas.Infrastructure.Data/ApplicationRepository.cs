using Mas.Domain.Aggregate;
using Mas.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        return await _dbContext.Set<Application>().SingleOrDefaultAsync(e => e.Id.Equals(id));
        }
        public async Task<Application> AddAsync(Application entity)
        {
            await _dbContext.Set<Application>().AddAsync(entity);
            
            return entity;
        }

        public async Task<List<Application>> GetAsync(ApplicationStatus status)
        {
            return await _dbContext.Set<Application>().Where(a=> a.Status== status).ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();

        }
    }
}
