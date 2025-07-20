using Microsoft.EntityFrameworkCore;
using PetSpaManagement.BLL.Bookings;
using PetSpaManagement.DAL;
using PetSpaManagement.DAL.Entities;
using PetSpaManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaManagement.BLL.Users
{
    public class UserController
    {
        private UsersRepository _repo = new();
        private BookingController _booking = new();
        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }
        public void AddUser(User user)
        {
            _repo.AddUser(user);
        }
        public void UpdateUser(User user)
        {
            _repo.UpdateUser(user);
        }
        public void DeactivateUser(int userId)
        {
            _repo.deactivateUser(userId);
        }
        public List<User> GetUserByRole(int RoleID)
        {
            var users = _repo.GetAllUsers();

            return users.Where(u => u.Role.RoleId == RoleID).ToList();
        }

        public List<User> GetAvailableCareStaff(DateOnly bookingDate, TimeOnly bookingTime)
        {
            // Lấy tất cả nhân viên có vai trò CareStaff (giả sử RoleId = 3 cho CareStaff)
            List<User> allCareStaff = GetUserByRole(3); // Giả sử 3 là RoleId cho CareStaff

            List<User> availableCareStaff = new List<User>();

            foreach (var staff in allCareStaff)
            {
                // Kiểm tra xem nhân viên này có đang bận vào thời gian đã chọn không
                // Giả sử mỗi booking kéo dài 1 giờ, hoặc bạn có thể định nghĩa khoảng thời gian dịch vụ
                bool isBusy = _booking.IsCareStaffBusy(staff.UserId, bookingDate, bookingTime);

                if (!isBusy)
                {
                    availableCareStaff.Add(staff);
                }
            }
            return availableCareStaff;
        }
    }
}
