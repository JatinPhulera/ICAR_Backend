using System.ComponentModel.DataAnnotations;

namespace ICAR.Scanner.Models.DTOs;
    public class FileDetailCreateDTO
{
    public Guid Id { get; set; }

    public string? Filename { get; set; } = null!;

    public string? Filetype { get; set; } = null!;

    public string? FilePath { get; set; } = null!;

    public bool? IsActive { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; } = null!;

    public string? UpdatedBy { get; set; } = null!;

    public Guid TreeId { get; set; }
}
