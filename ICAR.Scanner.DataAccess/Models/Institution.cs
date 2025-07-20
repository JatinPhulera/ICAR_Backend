using System;
using System.Collections.Generic;

namespace ICAR.Scanner.DataAccess.Models;

public partial class Institution
{
    public int InstitutionID { get; set; }

    public string InstitutionName { get; set; } = null!;

    public string? InstitutionHead { get; set; }

    public string? InstitutionAdress { get; set; }

    public bool? Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
