using ICAR.Scanner.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace ICAR.Scanner.Models.DTOs;
    public class InstitutionsCreateDTO
{
    public int InstitutionID { get; set; }
    public string InstitutionName { get; set; } = null!;
    public string? InstitutionHead { get; set; } = null!;
    public string? InstitutionAdress { get; set; } = null!;
    public bool? Status { get; set; } = null!;
    public DateOnly? CreatedOn { get; set; } = null!;
    public DateOnly? UpdatedOn { get; set; } = null!;
    public string? CreatedBy { get; set; } = null!;
    public string? UpdatedBy { get; set; } = null!;
}
