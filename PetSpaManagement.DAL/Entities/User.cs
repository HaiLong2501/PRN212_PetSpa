using System;
using System.Collections.Generic;

namespace PetSpaManagement.DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string Username { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public int? RoleId { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Booking> BookingCareStaffs { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingReceptionists { get; set; } = new List<Booking>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<ServiceHistory> ServiceHistories { get; set; } = new List<ServiceHistory>();
}
