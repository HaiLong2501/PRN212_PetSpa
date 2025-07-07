using System;
using System.Collections.Generic;

namespace PetSpaManagement.DAL.Entities;

public partial class ServiceHistory
{
    public int HistoryId { get; set; }

    public int? PetId { get; set; }

    public int? ServiceId { get; set; }

    public int? CareStaffId { get; set; }

    public DateOnly? ServiceDate { get; set; }

    public string? Notes { get; set; }

    public virtual User? CareStaff { get; set; }

    public virtual Pet? Pet { get; set; }

    public virtual Service? Service { get; set; }
}
