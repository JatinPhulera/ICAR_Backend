using System;
using System.Collections.Generic;

namespace ICAR.Scanner.DataAccess.Models;

public partial class RoleMaster
{
    public int RoleID { get; set; }

    public string Name { get; set; } = null!;

    public bool? Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
