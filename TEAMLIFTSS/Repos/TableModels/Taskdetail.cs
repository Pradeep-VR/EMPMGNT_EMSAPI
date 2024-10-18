using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TEAMLIFTSS.Repos.TableModels;

[Table("TASKDETAILS")]
public partial class Taskdetail
{
    [Key]
    [Column("TASKID")]
    public Guid Taskid { get; set; }

    [Column("TASKASSIGNERS")]
    [StringLength(50)]
    public string? Taskassigners { get; set; }

    [Column("TASKRECEIVER")]
    [StringLength(50)]
    public string? Taskreceiver { get; set; }

    [Column("TASKCONTENT")]
    public string? Taskcontent { get; set; }

    [Column("TASKENDTIME", TypeName = "datetime")]
    public DateTime? Taskendtime { get; set; }

    [Column("TASKSTATUS")]
    [StringLength(50)]
    public string? Taskstatus { get; set; }

    [Column("REASON")]
    public string? Reason { get; set; }

    [ForeignKey("Taskassigners")]
    [InverseProperty("TaskdetailTaskassignersNavigations")]
    public virtual Employeedetail? TaskassignersNavigation { get; set; }

    [ForeignKey("Taskreceiver")]
    [InverseProperty("TaskdetailTaskreceiverNavigations")]
    public virtual Employeedetail? TaskreceiverNavigation { get; set; }
}
