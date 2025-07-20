
using PetSpaManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaManagement.DAL.Repositories
{
    public class CustomerRepository
    {
        private PetSpaManagementContext _context;
            public CustomerRepository()
            {
                _context = new PetSpaManagementContext();
            }
    
            public List<Customer> GetAllCustomers()
            {
                return _context.Customers.ToList();
            }
    
            public void AddCustomer(Customer customer)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
    
            public void UpdateCustomer(Customer customer)
            {
                var existingCustomer = _context.Customers.Find(customer.CustomerId);
                if (existingCustomer != null)
                {
                    existingCustomer.FullName = customer.FullName;
                    existingCustomer.Email = customer.Email;
                    existingCustomer.Phone = customer.Phone;
                    _context.SaveChanges();
                }
            }
    
            public void DeleteCustomer(Customer customer)
            {
                var existingCustomer = _context.Customers.Find(customer.CustomerId);
                if (existingCustomer != null)
                {
                    _context.Customers.Remove(existingCustomer);
                    _context.SaveChanges();
                }
        }
    }
}
