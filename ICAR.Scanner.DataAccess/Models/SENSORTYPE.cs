using System;
using System.Collections.Generic;

namespace ICAR.Scanner.DataAccess.Models;

public partial class SENSORTYPE
{
    public Guid SENSORTYPEID { get; set; }

    public string SENSORTYPE1 { get; set; } = null!;

    public string? SENSORUID { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<SENSOR> SENSORs { get; set; } = new List<SENSOR>();
}
