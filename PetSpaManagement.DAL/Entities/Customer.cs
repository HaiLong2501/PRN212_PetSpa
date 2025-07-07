using System;
using System.Collections.Generic;

namespace PetSpaManagement.DAL.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
