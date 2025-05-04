using System;
using System.Collections.Generic;

namespace ICAR.Scanner.DataAccess.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
