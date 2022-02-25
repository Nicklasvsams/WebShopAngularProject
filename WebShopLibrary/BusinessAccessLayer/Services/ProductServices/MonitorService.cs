using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopLibrary.BusinessAccessLayer.DTOs.MonitorDTO;
using WebShopLibrary.DataAccessLayer.Database.Entities.Products.Monitors;
using WebShopLibrary.DataAccessLayer.Repositories.ProductRepositories;

namespace WebShopLibrary.BusinessAccessLayer.Services.ProductServices
{
    public interface IMonitorService
    {
        Task<List<MonitorResponse>> GetAllMonitors();
        Task<MonitorResponse> GetMonitorById(int monitorId);
        Task<MonitorResponse> CreateMonitor(MonitorRequest newMonitor);
        Task<MonitorResponse> DeleteMonitor(int monitorId);
        Task<MonitorResponse> UpdateMonitor(int monitorId, MonitorRequest monitorUpdate);
    }

    public class MonitorService : IMonitorService
    {
        private readonly IMonitorRepository _monitorRepository;

        public MonitorService(IMonitorRepository monitorRepository)
        {
            _monitorRepository = monitorRepository;
        }

        public async Task<MonitorResponse> CreateMonitor(MonitorRequest newMonitor)
        {
            Monitor monitor = await _monitorRepository.InsertNewMonitor(MapMonitorRequestToMonitor(newMonitor));

            if (monitor != null)
            {
                return MapMonitorToMonitorResponse(monitor);
            }

            return null;
        }

        public async Task<MonitorResponse> DeleteMonitor(int monitorId)
        {
            Monitor monitor = await _monitorRepository.DeleteMonitorById(monitorId);

            if (monitor != null)
            {
                return MapMonitorToMonitorResponse(monitor);
            }

            return null;
        }

        public async Task<List<MonitorResponse>> GetAllMonitors()
        {
            List<Monitor> monitors = await _monitorRepository.SelectAllMonitors();
            return monitors.Select(monitor => MapMonitorToMonitorResponse(monitor)).ToList();
        }

        public async Task<MonitorResponse> GetMonitorById(int monitorId)
        {
            Monitor monitor = await _monitorRepository.SelectMonitorById(monitorId);

            if (monitor != null)
            {
                return MapMonitorToMonitorResponse(monitor);
            }

            return null;
        }

        public async Task<MonitorResponse> UpdateMonitor(int monitorId, MonitorRequest monitorUpdate)
        {
            Monitor monitor = await _monitorRepository.UpdateExistingMonitor(monitorId, MapMonitorRequestToMonitor(monitorUpdate));

            if (monitor != null)
            {
                return MapMonitorToMonitorResponse(monitor);
            }

            return null;
        }

        private Monitor MapMonitorRequestToMonitor(MonitorRequest monReq)
        {
            return new Monitor
            {
                Brand = monReq.Brand,
                ReleaseYear = monReq.ReleaseYear,
                CategoryId = monReq.CategoryId,
                ProductId = monReq.ProductId,
                Size = monReq.Size
            };
        }

        private MonitorResponse MapMonitorToMonitorResponse(Monitor monitor)
        {
            MonitorResponse monitorRes = new MonitorResponse
            {
                Id = monitor.Id,
                Brand = monitor.Brand,
                ReleaseYear = monitor.ReleaseYear,
                Size = monitor.Size,
                CategoryId = monitor.CategoryId,
                ProductId = monitor.ProductId,
                Product = new MonitorProductResponse(),
                Category = new MonitorCategoryResponse()
            };

            if (monitor.Product != null)
            {
                monitorRes.Product.Id = monitor.Product.Id;
                monitorRes.Product.Name = monitor.Product.Name;
                monitorRes.Product.Description = monitor.Product.Description;
                monitorRes.Product.Price = monitor.Product.Price;
                monitorRes.Product.Stock = monitor.Product.Stock;
            }
            if (monitor.Category != null)
            {
                monitorRes.Category.Id = monitor.Category.Id;
                monitorRes.Category.Name = monitor.Category.Name;
                monitorRes.Category.Description = monitor.Category.Description;
            }

            return monitorRes;
        }
    }
}
