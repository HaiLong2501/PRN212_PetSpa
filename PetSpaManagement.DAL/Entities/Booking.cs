using System;
using System.Collections.Generic;

namespace PetSpaManagement.DAL.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? PetId { get; set; }

    public int? ReceptionistId { get; set; }

    public int? CareStaffId { get; set; }

    public DateOnly? BookingDate { get; set; }

    public TimeOnly? BookingTime { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual User? CareStaff { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Pet? Pet { get; set; }

    public virtual User? Receptionist { get; set; }
}
