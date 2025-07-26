using System;
using System.Collections.Generic;

namespace ICAR.Scanner.DataAccess.Models;

public partial class TREE
{
    public Guid TREEID { get; set; }

    public string? COMMONNAME { get; set; }

    public string? CULTIVERNAME { get; set; }

    public string? ACCESSIONNUMBER { get; set; }

    public string? SCIENTIFICNAME { get; set; }

    public string? DONORORGANIZATION { get; set; }

    public string? PLACEOFORGIN { get; set; }

    public string? FGBLOCATION { get; set; }

    public string? UNIQUEIMPORTANCE { get; set; }

    public string? PLANTATIONYEAR { get; set; }

    public string? SENSORTYPE { get; set; }

    public string? SENSORUID { get; set; }

    public string? SELECTFIELDSTAFF { get; set; }

    public string? age_units { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public Guid? SENSORID { get; set; }

    public virtual SENSOR? SENSOR { get; set; }
}
