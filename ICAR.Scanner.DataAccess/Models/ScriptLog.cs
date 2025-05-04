using System;
using System.Collections.Generic;

namespace ICAR.Scanner.DataAccess.Models;

public partial class ScriptLog
{
    public Guid ScriptLogId { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? Remarks { get; set; }
}
