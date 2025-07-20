using Microsoft.EntityFrameworkCore;
using PetSpaManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaManagement.DAL.Repositories
{
    public class UsersRepository
    {
        private PetSpaManagementContext _context;

        public UsersRepository()
        {
            _context = new PetSpaManagementContext();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Include("Role").ToList();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var existingUser = _context.Users.Find(user.UserId);
            if (existingUser != null)
            {
                existingUser.FullName = user.FullName;
                existingUser.Username = user.Username;
                existingUser.PasswordHash = user.PasswordHash;
                existingUser.Role = user.Role;
                _context.SaveChanges();
            }
        }

        public void deactivateUser(int userId)
        {
            var existingUser = _context.Users.Find(userId);
            if (existingUser != null)
            {
                existingUser.IsActive = false;
                _context.SaveChanges();
            }
        }



    }
}
