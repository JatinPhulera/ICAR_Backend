using System.Net;

namespace ICAR.Scanner.Models.DTOs;

public class SensorDTO
{
    public Guid SensorId { get; set; }

    public string? SensorType { get; set; }

    public string? SensorUid { get; set; }

    public string? CommonName { get; set; }

    public string? AccessionNumber { get; set; }

    public string? UserName { get; set; }

    public string? AddedBy { get; set; }

    public string? CustId { get; set; }

    public string? AssetId { get; set; }

    public string? DisplayId { get; set; }

    public string? AccessionNumberAlt { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? BatteryPercentage { get; set; }

    public string? MessageType { get; set; }

    public bool? IsHooterOn { get; set; }

    public bool? IsSensitivity { get; set; }

    public string? SensitivityValue { get; set; }

    public DateTime? InstallationDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public Guid? SENSORTYPEID { get; set; }

   // public virtual SENSORTYPE? SENSORTYPENavigation { get; set; }

    //public virtual ICollection<TREE> TREEs { get; set; } = new List<TREE>();
}