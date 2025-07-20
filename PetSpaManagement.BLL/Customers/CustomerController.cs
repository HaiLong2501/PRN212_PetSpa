using PetSpaManagement.DAL.Entities;
using PetSpaManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaManagement.BLL.Customers
{
    public class CustomerController
    {
        private CustomerRepository _repo = new();

        public List<DAL.Entities.Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public void AddCustomer(DAL.Entities.Customer customer)
        {
            _repo.AddCustomer(customer);
        }

        public void UpdateCustomer(DAL.Entities.Customer customer)
        {
            _repo.UpdateCustomer(customer);
        }

        public void DeleteCustomer(DAL.Entities.Customer customer)
        {
            _repo.DeleteCustomer(customer);
        }

        public Customer LookupCustomerByPhone(string phone)
        {
            var customers = _repo.GetAllCustomers();
            return customers.FirstOrDefault(c => c.Phone == phone);
        }

        public Customer checkEmail(string email)
        {
            var customers = _repo.GetAllCustomers();
            return customers.FirstOrDefault(c => c.Email == email);
        }
    }
}
