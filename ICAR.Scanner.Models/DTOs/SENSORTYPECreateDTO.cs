using System.ComponentModel.DataAnnotations;

namespace ICAR.Scanner.Models.DTOs;
    public class SENSORTYPECreateDTO
{
    public Guid SENSORTYPEID { get; set; }

    public string SENSORTYPE1 { get; set; } = null!;

    public string? SENSORUID { get; set; } = null!;

    public DateTime CreatedOn { get; set; } 

    public DateTime? UpdatedOn { get; set; } = null!;

    public string? CreatedBy { get; set; } = null!;

    public string? UpdatedBy { get; set; } = null!;

    //public virtual ICollection<SENSOR> SENSORs { get; set; } = new List<SENSOR>();
}
