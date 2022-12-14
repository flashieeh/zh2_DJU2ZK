using System;
using System.Collections.Generic;

namespace zh2_DJU2ZK.Models;

public partial class Work
{
    public int WorkId { get; set; }

    public int? EmployerId { get; set; }

    public int? StudentId { get; set; }

    public string? JobTitle { get; set; }

    public DateTime? Date { get; set; }

    public int? PricePerHour { get; set; }

    public int? Hours { get; set; }

    public bool IsSecondary { get; set; }
}
