﻿using System.ComponentModel.DataAnnotations;

namespace StudentManagmentSystem.Models.SchoolViewModels;

public class EnrollmentDateGroup
{
    [DataType(DataType.Date)]
    public DateTime? EnrollmentDate { get; set; }

    public int StudentCount { get; set; }
}
