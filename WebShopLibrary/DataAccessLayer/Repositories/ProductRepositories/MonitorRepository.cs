using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShopLibrary.DataAccessLayer.Database;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products.Monitors;

namespace WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories
{
    public interface IMonitorRepository
    {
        Task<List<Monitor>> SelectAllMonitors();
        Task<Monitor> SelectMonitorById(int monitorId);
        Task<Monitor> InsertNewMonitor(Monitor monitor);
        Task<Monitor> DeleteMonitorById(int monitorId);
        Task<Monitor> UpdateExistingMonitor(int monitorId, Monitor monitor);
    }

    public class MonitorRepository : IMonitorRepository
    {
        private readonly WebShopDBContext _dbContext;

        public MonitorRepository(WebShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Monitor> InsertNewMonitor(Monitor monitor)
        {
            await _dbContext.Monitor.AddAsync(monitor);
            await _dbContext.SaveChangesAsync();

            return monitor;
        }

        public async Task<Monitor> DeleteMonitorById(int monitorId)
        {
            Monitor monitorToDelete = await _dbContext.Monitor.FirstOrDefaultAsync(x => x.Id == monitorId);

            if(monitorToDelete != null)
            {
                _dbContext.Monitor.Remove(monitorToDelete);

                await _dbContext.SaveChangesAsync();
            }

            return monitorToDelete;
        }

        public async Task<List<Monitor>> SelectAllMonitors()
        {
            return await _dbContext.Monitor
                .Include(p => p.Product)
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<Monitor> SelectMonitorById(int monitorId)
        {
            return await _dbContext.Monitor
                .Include(p => p.Product)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(x => x.Id == monitorId);
        }

        public async Task<Monitor> UpdateExistingMonitor(int monitorId, Monitor monitor)
        {
            Monitor monitorToUpdate = await _dbContext.Monitor.FirstOrDefaultAsync(x => x.Id == monitorId);

            if (monitorToUpdate != null)
            {
                monitorToUpdate.Brand = monitor.Brand;
                monitorToUpdate.CategoryId = monitor.CategoryId;
                monitorToUpdate.ProductId = monitor.ProductId;
                monitorToUpdate.ReleaseYear = monitor.ReleaseYear;
                monitorToUpdate.Size = monitor.Size;

                await _dbContext.SaveChangesAsync();
            }

            return monitorToUpdate;
        }
    }
}
