using System.Net;

namespace ICAR.Scanner.Models.DTOs;

public class SensorDTO
{
    public Guid Id { get; set; }

    public string? SensorID { get; set; }

    public string? Type { get; set; }

    public DateTime? Installation_date { get; set; }

    public string? Status { get; set; }

    public string? AddedBy { get; set; }

    public string? CustID { get; set; }

    public string? AssetID { get; set; }

    public string? Accession_Number { get; set; }

    public string? SensorUID { get; set; }

    public string? Sensitivity { get; set; }

    public string? CommonName { get; set; }

    public string? UserName { get; set; }

    public DateTime? Expiry_date { get; set; }

    public string? batteryPercentage { get; set; }

    public string? messageType { get; set; }

    public bool? isHooterOn { get; set; }

    public bool? isSensitivity { get; set; }

    public string? sensitivityValue { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public Guid? SENSORTYPEID { get; set; }

    // public virtual SENSORTYPE? SENSORTYPENavigation { get; set; }

    //public virtual ICollection<TREE> TREEs { get; set; } = new List<TREE>();
}

public class SensorResponse
{
    public string Status { get; set; }
    public List<SensorDTO> Data { get; set; }
}