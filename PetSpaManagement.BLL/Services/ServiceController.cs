using PetSpaManagement.DAL.Entities;
using PetSpaManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaManagement.BLL.Services
{
    public class ServiceController
    {
        private ServiceRepository _repo = new();

        public List<Service> GetAllService()
        {
            return _repo.GetAll();
        }

        public void AddService(Service service)
        {
            _repo.Add(service);
        }

        public void UpdateService(Service service)
        {
            _repo.Update(service);
        }

        public void DeleteService(Service service)
        {
            _repo.Delete(service);
        }

        public bool ExistedService(string serviceName)
        {
            var services = _repo.GetAll();
            return services.Any(s => s.ServiceName.ToLower() == serviceName.ToLower());
        }
    }
}
