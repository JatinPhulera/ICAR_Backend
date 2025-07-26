using System;
using System.Collections.Generic;

namespace ICAR.Scanner.DataAccess.Models;

public partial class Address
{
    public Guid AddressId { get; set; }

    public string AddressLine1 { get; set; } = null!;

    public string? AddressLine2 { get; set; }

    public string? City { get; set; }

    public int StateId { get; set; }

    public string? PostalCode { get; set; }

    public int CountryId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual State State { get; set; } = null!;
}
