using System.ComponentModel.DataAnnotations;

namespace ICAR.Scanner.Models.DTOs;
    public class AuditTreeCreateDTO
{


    [Required]
    public string? Name { get; set; } = null!;

    public string? AuditId { get; set; } = null!;

    public DateTime? AuditDate { get; set; }

    public string? Girth { get; set; } = null!;

    public string? Height { get; set; } = null!;

    public string? Disease { get; set; } = null!;

    public string? Pest { get; set; } = null!;

    public string? PhysicalDamage { get; set; } = null!;

    public string? Remarks { get; set; } = null!;

    public string? AddedBy { get; set; } = null!;

    public DateTime? LastUpdate { get; set; } = null!;

    public string? State { get; set; } = null!;

    public string? Level { get; set; } = null!;

    public string? V { get; set; } = null!;

    public string? ReviewedBy { get; set; } = null!;

    public DateTime? ReviewedOn { get; set; } = null!;

    public string? AccessionNumber { get; set; } = null!;

    public bool? Deletable { get; set; } = null!;

    public bool? Editable { get; set; } = null!;

    public bool? Acceptable { get; set; } = null!;

    public bool? IsActive { get; set; } = null!;

    public DateTime CreatedOn { get; set; } 

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; } = null!;

    public string? UpdatedBy { get; set; } = null!;

    public Guid TreeId { get; set; } 

    //public virtual Tree Tree { get; set; } = null!;
}
