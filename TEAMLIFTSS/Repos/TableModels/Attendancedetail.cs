using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TEAMLIFTSS.Repos.TableModels;

[Table("ATTENDANCEDETAILS")]
public partial class Attendancedetail
{
    [Column("ID")]
    public int Id { get; set; }

    [Key]
    [Column("TRANSACTION_ID")]
    public Guid TransactionId { get; set; }

    [Column("EMPLOYEEID")]
    [StringLength(50)]
    public string Employeeid { get; set; } = null!;

    [Column("ISPRESENT")]
    public bool Ispresent { get; set; }

    [Column("CLIENTPLACE")]
    [StringLength(100)]
    public string Clientplace { get; set; } = null!;

    [Column("ATTENDANCEDBY")]
    [StringLength(50)]
    public string Attendancedby { get; set; } = null!;

    [Column("ATTENDANCEDDATE", TypeName = "datetime")]
    public DateTime Attendanceddate { get; set; }

    [ForeignKey("Employeeid")]
    [InverseProperty("Attendancedetails")]
    public virtual Employeedetail Employee { get; set; } = null!;
}
