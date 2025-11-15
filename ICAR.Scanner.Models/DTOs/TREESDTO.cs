namespace ICAR.Scanner.Models.DTOs;

public class TreesDto
{
    public Guid Id { get; set; }

    public string? DisplayId { get; set; }

    public string? AssetType { get; set; }

    public string? Location { get; set; }

    public string? Alerts { get; set; }

    public string? AddedBy { get; set; }

    public Guid? SENSORID { get; set; }

    public string? AccessionNumber { get; set; }

    public string? AssetId { get; set; }

    public string? RfidTagCreatedOn { get; set; }

    public string? LastAuditTime { get; set; }

    public string? AssetSubType { get; set; }

    public string? SensorType { get; set; }

    public string? OperatorId { get; set; }

    public string? AddedByName { get; set; }

    public string? OperatorName { get; set; }

    public string? Age { get; set; }

    public string? AgeUnits { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string? BotanicalName { get; set; }

    public string? ExpiryDate { get; set; }

    public DateTime? InstallationDate { get; set; }

    public string? Origin { get; set; }

    public string? UniqueImportance { get; set; }

    public string? Value { get; set; }

    public string? AccessionOrigin { get; set; }

    public string? CommonName { get; set; }

    public string? ScientificName { get; set; }

    public DateTime? LastUpdated { get; set; }

    public string? Status { get; set; }

    public string? CultiverName { get; set; }

    public string? DonorOrganization { get; set; }

    public string? Importance { get; set; }

    public string? PlaceOfOrigin { get; set; }

    public string? PlantationYear { get; set; }

    public string? OperatorFirstName { get; set; }

    public string? OperatorLastName { get; set; }

    public string? OperatorPhone { get; set; }

    public string? OperatorState { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public Guid? SENSOR { get; set; }
}

public class TreesResponse
{
    public string Status { get; set; }
    public int DataCount { get; set; }
    public List<TreesDto> Data { get; set; }
}
