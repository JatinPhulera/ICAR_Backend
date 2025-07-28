using System.ComponentModel.DataAnnotations;

namespace ICAR.Scanner.Models.DTOs;
public class TREESCreateDTO
{
    [Required]
    public string? COMMONNAME { get; set; } = null!;

    public string? CULTIVERNAME { get; set; } = null!;
    [Required]
    public string? ACCESSIONNUMBER { get; set; } = null!;
    [Required]
    public string? SCIENTIFICNAME { get; set; } = null!;

    public string? DONORORGANIZATION { get; set; } = null!;

    public string? PLACEOFORGIN { get; set; } = null!;
    [Required]
    public string? FGBLOCATION { get; set; } = null!;

    public string? UNIQUEIMPORTANCE { get; set; } = null!;

    public string? PLANTATIONYEAR { get; set; } = null!;

    public string? SENSORTYPE { get; set; } = null!;

    public string? SENSORUID { get; set; } = null!;

    public string? SELECTFIELDSTAFF { get; set; } = null!;
}
