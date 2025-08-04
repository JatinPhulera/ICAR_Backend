using System;
using System.Collections.Generic;

namespace ICAR.Scanner.DataAccess.Models;

public partial class Sensor
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

    public Guid? SensorTypeId { get; set; }

    public virtual SensorType? SensorTypeNavigation { get; set; }

    public virtual ICollection<Tree> Trees { get; set; } = new List<Tree>();
}
