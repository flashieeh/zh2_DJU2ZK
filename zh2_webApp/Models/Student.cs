using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace zh2_webApp.Models;

public partial class Student
{
    [Key]
    public int StudentId { get; set; }

    public string? Name { get; set; }

    public DateTime? Birthdate { get; set; }
}
