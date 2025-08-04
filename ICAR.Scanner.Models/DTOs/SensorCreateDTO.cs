using System.ComponentModel.DataAnnotations;

namespace ICAR.Scanner.Models.DTOs.Request;
    public class SensorCreateDTO
{

    [Required]
    public string? SENSORTYPE { get; set; } = null!;

    public string? SENSORUID { get; set; } = null!;

    [Required]
    public string? CommonName { get; set; } = null!;

    public string? AccessionNumber { get; set; } = null!;

    public string? UserName { get; set; } = null!;

    public string? addedBy { get; set; } = null!;

    public string? custID { get; set; } = null!;
}
