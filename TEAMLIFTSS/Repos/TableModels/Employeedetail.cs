using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TEAMLIFTSS.Repos.TableModels;

[Table("EMPLOYEEDETAILS")]
public partial class Employeedetail
{
    [Key]
    [Column("EMPID")]
    [StringLength(50)]
    public string Empid { get; set; } = null!;

    [Column("FIRSTNAME")]
    [StringLength(50)]
    public string Firstname { get; set; } = null!;

    [Column("MIDDLENAME")]
    [StringLength(50)]
    public string? Middlename { get; set; }

    [Column("LASTNAME")]
    [StringLength(50)]
    public string? Lastname { get; set; }

    [Column("SECRETCODE")]
    [StringLength(100)]
    public string Secretcode { get; set; } = null!;

    [Column("ACTIVE")]
    public bool Active { get; set; }

    [Column("DEPARTMENT")]
    [StringLength(20)]
    public string? Department { get; set; }

    [Column("DESIGNATION")]
    [StringLength(30)]
    public string? Designation { get; set; }

    [Column("EMPROLE")]
    [StringLength(30)]
    public string Emprole { get; set; } = null!;

    [Column("REGISTEREDDATE", TypeName = "datetime")]
    public DateTime? Registereddate { get; set; }

    [Column("REGISTEREDBY")]
    [StringLength(50)]
    public string? Registeredby { get; set; }

    [Column("DEVICEDETAILS")]
    [StringLength(100)]
    public string? Devicedetails { get; set; }

    [Column("EMPVIOLATION")]
    public bool Empviolation { get; set; }

    [Column("DEVICEVIOLATION")]
    public int Deviceviolation { get; set; }

    [Column("REMARKS")]
    [StringLength(100)]
    public string? Remarks { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Attendancedetail> Attendancedetails { get; set; } = new List<Attendancedetail>();

    [InverseProperty("TaskassignersNavigation")]
    public virtual ICollection<Taskdetail> TaskdetailTaskassignersNavigations { get; set; } = new List<Taskdetail>();

    [InverseProperty("TaskreceiverNavigation")]
    public virtual ICollection<Taskdetail> TaskdetailTaskreceiverNavigations { get; set; } = new List<Taskdetail>();
}
