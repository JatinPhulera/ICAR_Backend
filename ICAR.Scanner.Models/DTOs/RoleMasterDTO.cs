using ICAR.Scanner.DataAccess.Models;
using System.Net;

namespace ICAR.Scanner.Models.DTOs;

public class RoleMasterDTO
{
    public Guid RoleID { get; set; }

    public string Name { get; set; } = null!;

    public bool? Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}

public class RoleMasterResponse
{
    public string Status { get; set; }
    public List<RoleMasterDTO> Data { get; set; }
}