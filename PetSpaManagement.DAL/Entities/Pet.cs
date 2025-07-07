using System;
using System.Collections.Generic;

namespace PetSpaManagement.DAL.Entities;

public partial class Pet
{
    public int PetId { get; set; }

    public int? CustomerId { get; set; }

    public string? PetName { get; set; }

    public string? Species { get; set; }

    public string? Breed { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Birthday { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<ServiceHistory> ServiceHistories { get; set; } = new List<ServiceHistory>();
}
