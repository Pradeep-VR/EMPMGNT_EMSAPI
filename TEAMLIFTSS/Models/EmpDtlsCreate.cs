namespace TEAMLIFTSS.Models;

public class EmpDtlsCreate
{
    public string Empid { get; set; } = null!;
    public string Firstname { get; set; } = null!;
    public string Secretcode { get; set; } = null!;
    public bool Active { get; set; }
    public string? Department { get; set; }
    public string? Designation { get; set; }
    public string Emprole { get; set; } = null!;
    public DateTime? Registereddate { get; set; }
    public string? Registeredby { get; set; }

}
