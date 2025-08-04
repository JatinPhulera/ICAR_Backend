namespace ICAR.Scanner.Models.DTOs;

public class TreesDto
{
    public Guid TreeId { get; set; }
    public string? CommonName { get; set; }
    public string? CultiverName { get; set; }
    public string? AccessionNumber { get; set; }
    public string? ScientificName { get; set; }
    public string? DonorOrganization { get; set; }
    public string? PlaceOfOrigin { get; set; }
    public string? FgbLocation { get; set; }
    public string? UniqueImportance { get; set; }
    public string? PlantationYear { get; set; }
    public string? SensorType { get; set; }
    public string? SensorUid { get; set; }
    public string? SelectFieldStaff { get; set; }
    public string? AgeUnits { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public bool? IsActive { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public Guid? SensorId { get; set; }
}
