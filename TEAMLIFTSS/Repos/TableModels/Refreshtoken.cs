using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TEAMLIFTSS.Repos.TableModels;

[Table("REFRESHTOKEN")]
public partial class Refreshtoken
{
    [Key]
    [Column("EMPID")]
    [StringLength(50)]
    [Unicode(false)]
    public string Empid { get; set; } = null!;

    [Column("TOKENID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Tokenid { get; set; }

    [Column("REFRESHTOKEN")]
    [Unicode(false)]
    public string? Refreshtoken1 { get; set; }
}
