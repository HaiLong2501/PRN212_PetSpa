using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PetSpaManagement.DAL.Entities;
using PetSpaManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaManagement.BLL.Bookings
{
    public class BookingController
    {
        private BookingRepository _repo = new BookingRepository();

        public List<Booking> GetAllBookings()
        {
            return _repo.GetAllBookings();
        }

        public void AddBooking(Booking booking)
        {
            _repo.AddBooking(booking);
        }

        public void RemoveBooking(Booking booking)
        {
            _repo.RemoveBooking(booking);
        }

        public void UpdateBooking(Booking booking) {
                        // Assuming the BookingRepository has an Update method
            _repo.UpdateBooking(booking);
        }

        public bool IsCareStaffBusy(int careStaffId, DateOnly bookingDate, TimeOnly bookingTime)
        {
            
            return _repo.GetAllBookings().Any(b =>
                b.CareStaffId == careStaffId &&
                b.BookingDate == bookingDate &&
                b.BookingTime == bookingTime); // Kiểm tra trùng khớp chính xác thời gian
        }

    }
}
