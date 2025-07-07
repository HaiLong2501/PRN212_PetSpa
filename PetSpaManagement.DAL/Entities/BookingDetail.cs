using System;
using System.Collections.Generic;

namespace PetSpaManagement.DAL.Entities;

public partial class BookingDetail
{
    public int BookingDetailId { get; set; }

    public int? BookingId { get; set; }

    public int? ServiceId { get; set; }

    public int? Quantity { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Service? Service { get; set; }
}
