using System;
using System.Collections.Generic;

namespace ICAR.Scanner.DataAccess.Models;

public partial class SENSOR
{
    public Guid SENSORID { get; set; }

    public string? SENSORTYPE { get; set; }

    public string? SENSORUID { get; set; }

    public string? CommonName { get; set; }

    public string? AccessionNumber { get; set; }

    public string? UserName { get; set; }

    public string? addedBy { get; set; }

    public string? custID { get; set; }

    public string? assetID { get; set; }

    public string? displayID { get; set; }

    public string? accession_number { get; set; }

    public DateTime? expiry_date { get; set; }

    public string? batteryPercentage { get; set; }

    public string? messageType { get; set; }

    public bool? isHooterOn { get; set; }

    public bool? isSensitivity { get; set; }

    public string? sensitivityValue { get; set; }

    public DateTime? installation_date { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public Guid? SENSORTYPEID { get; set; }

    public virtual SENSORTYPE? SENSORTYPENavigation { get; set; }

    public virtual ICollection<TREE> TREEs { get; set; } = new List<TREE>();
}
