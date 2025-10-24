using System.ComponentModel.DataAnnotations;

namespace ICAR.Scanner.Models.DTOs;
public class TREESCreateDTO
{
 
    [Required]
    public string? CommonName { get; set; } = null!;

    public string? ScientificName { get; set; } = null!;
    [Required]
    public string? AccessionNumber { get; set; } = null!;
    [Required]
    public string? BotanicalName { get; set; } = null!;

    public string? DonorOrganization { get; set; } = null!;

    public string? PlaceOfOrigin { get; set; } = null!;
    [Required]
    public string? Location { get; set; } = null!;

    public string? UniqueImportance { get; set; } = null!;

    public DateTime? InstallationDate { get; set; } = null!;

    // public string? SensorType { get; set; } = null!;

    public string? SENSORID { get; set; } = null!;

    public string? OperatorName { get; set; } = null!;
}
