using System.ComponentModel.DataAnnotations;

namespace ICAR.Scanner.Models.DTOs;

public class TREESCreateDTO
{
    public string? ImageUrl { get; set; }

    public string? CommonName { get; set; }

    public string? ScientificName { get; set; }

    public string? CultiverName { get; set; }

    public string? AccessionNumber { get; set; }

    public string? BotanicalName { get; set; }

    public string? DonorOrganization { get; set; }

    public string? PlaceOfOrigin { get; set; }

    public string? Location { get; set; }

    public string? UniqueImportance { get; set; }

    public string? Age { get; set; }

    public string? AgeUnits { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public DateTime? InstallationDate { get; set; }

    public string? PlantationYear { get; set; }

    public Guid? SensorTypeId { get; set; }

    public Guid? SensorId { get; set; }

    public string? OperatorName { get; set; }

    public string? OperatorId { get; set; }

    public string? AddedBy { get; set; }

    public string? AddedByName { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}