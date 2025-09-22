using System.ComponentModel.DataAnnotations;

namespace ICAR.Scanner.Models.DTOs;
    public class RoleMasterCreateDTO
{
    public int RoleID { get; set; }

    public string Name { get; set; } = null!;

    public bool? Status { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; } = null!;

    public string? UpdatedBy { get; set; } = null!;
}
