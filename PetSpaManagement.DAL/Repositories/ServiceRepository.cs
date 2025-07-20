
using PetSpaManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaManagement.DAL.Repositories
{
    public class ServiceRepository
    {
        private PetSpaManagementContext _context;

        public List<Service> GetAll()
        {
            _context = new();

            return _context.Services.ToList();
        }

        public void Add(Service service) { 
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        public void Update(Service service)
        {
            _context = new();
            var existingService = _context.Services.Find(service.ServiceId);
            if (existingService != null)
            {
                existingService.ServiceName = service.ServiceName;
                existingService.Price = service.Price;
                existingService.Description = service.Description;
                _context.SaveChanges();
            }
        }

        public void Delete(Service service)
        {
            Service existingService = _context.Services.Find(service.ServiceId);
            if (existingService != null)
            {
                _context.Services.Remove(existingService);
                _context.SaveChanges();
            }
        }
    }
}
