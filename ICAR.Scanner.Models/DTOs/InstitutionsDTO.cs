using System.Net;

namespace ICAR.Scanner.Models.DTOs;

public class InstitutionsDTO
{
    public Guid InstitutionID { get; set; }
    public string InstitutionName { get; set; }
    public string? InstitutionHead { get; set; }
    public string? InstitutionAdress { get; set; }
    public bool? Status { get; set; }
    public DateOnly? CreatedOn { get; set; }
    public DateOnly? UpdatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}

public class InstitutionsResponse
{
    public string Status { get; set; }
    public List<InstitutionsDTO> Data { get; set; }
}