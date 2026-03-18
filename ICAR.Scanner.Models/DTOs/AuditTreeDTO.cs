using System.Net;

namespace ICAR.Scanner.Models.DTOs;

public class AuditTreeDTO
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? AuditId { get; set; }

    public DateTime AuditDate { get; set; }

    public string? Girth { get; set; }

    public string? Height { get; set; }

    public string? Disease { get; set; }

    public string? Pest { get; set; }

    public string? PhysicalDamage { get; set; }

    public string? Remarks { get; set; }
    
    public string? AddedBy { get; set; }

    public DateTime? LastUpdate { get; set; }

    public string? State { get; set; }

    public string? Level { get; set; }

    public string? V { get; set; }

    public string? ReviewedBy { get; set; }

    public DateTime? ReviewedOn { get; set; }

    public string? AccessionNumber { get; set; }

    public bool? Deletable { get; set; }

    public bool? Editable { get; set; }

    public bool? Acceptable { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public Guid TreeId { get; set; }

    //public virtual Tree Tree { get; set; } = null!;
}

public class AuditTreeResponse
{
    public string Status { get; set; }
    public List<AuditTreeDTO> Data { get; set; }
}