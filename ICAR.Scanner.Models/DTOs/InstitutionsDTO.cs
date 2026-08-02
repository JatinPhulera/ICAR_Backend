using ICAR.Scanner.DataAccess.Models;
using System.Net;

namespace ICAR.Scanner.Models.DTOs;

public class InstitutionsDTO
{
    public Guid InstitutionID { get; set; }

    public string InstitutionName { get; set; } = null!;

    public string? InstitutionHead { get; set; }

    public string? InstitutionAdress { get; set; }

    public bool? Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

public class InstitutionsResponse
{
    public string Status { get; set; }
    public List<InstitutionsDTO> Data { get; set; }
}