using Microsoft.EntityFrameworkCore;
using PetSpaManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSpaManagement.DAL.Repositories
{
    public class BookingRepository
    {
        PetSpaManagementContext  _context;

        public BookingRepository()
        {
            _context = new PetSpaManagementContext();
        }

        public List<Booking> GetAllBookings()
        {
            return _context.Bookings.Include(b => b.Pet).ThenInclude(b => b.Customer)
                                    .Include(b => b.CareStaff)
                                    .OrderBy(b => b.BookingId)
                                    .ToList();
        }

        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public void RemoveBooking(Booking booking) {
                        _context.Bookings.Remove(booking);
            _context.SaveChanges();
        }

        public void UpdateBooking(Booking booking)
        {
            var existingBooking = _context.Bookings.Find(booking.BookingId);
            if (existingBooking != null)
            {
                existingBooking.BookingDetails = booking.BookingDetails;
                existingBooking.BookingDate = booking.BookingDate;
                existingBooking.BookingTime = booking.BookingTime;
                _context.SaveChanges();
            }
        }
    }
}
