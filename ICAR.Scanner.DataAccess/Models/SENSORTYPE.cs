using System;
using System.Collections.Generic;

namespace ICAR.Scanner.DataAccess.Models;

public partial class SensorType
{
    public Guid SensorTypeId { get; set; }

    public string SensorTypeName { get; set; } = null!;

    public string? SensorUid { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
}
