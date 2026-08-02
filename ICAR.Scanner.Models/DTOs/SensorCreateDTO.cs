using System.ComponentModel.DataAnnotations;

namespace ICAR.Scanner.Models.DTOs.Request;
    public class SensorCreateDTO
{



    public string? SensorID { get; set; } = null!;

    public string? Type { get; set; } = null!;

    public DateTime? Installation_date { get; set; } = null!;

    public string? Status { get; set; } = null!;

    public string? AddedBy { get; set; } = null!;

    public string? CustID { get; set; } = null!;

    public string? AssetID { get; set; } = null!;

    public string? Accession_Number { get; set; } = null!;

    public string? SensorUID { get; set; } = null!;

    public string? Sensitivity { get; set; } = null!;

    public string? CommonName { get; set; } = null!;

    public string? UserName { get; set; } = null!;

    public DateTime? Expiry_date { get; set; } = null!;

    public string? batteryPercentage { get; set; } = null!;

    public string? messageType { get; set; } = null!;

    public bool? isHooterOn { get; set; } = null!;

    public bool? isSensitivity { get; set; } = null!;

    public string? sensitivityValue { get; set; } = null!;

    public bool? IsActive { get; set; } = null!;

    public DateTime CreatedOn { get; set; }     

    public DateTime? UpdatedOn { get; set; }    

    public string? CreatedBy { get; set; } = null!;

    public string? UpdatedBy { get; set; } = null!;

    public Guid? SENSORTYPEID { get; set; }
}
