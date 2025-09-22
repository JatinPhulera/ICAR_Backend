using System.Net;

namespace ICAR.Scanner.Models.DTOs;

public class FileDetailDTO
{
    public Guid Id { get; set; }

    public string? Filename { get; set; }

    public string? Filetype { get; set; }

    public string? FilePath { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public Guid TreeId { get; set; }
}

public class FileDetailResponse
{
    public string Status { get; set; }
    public List<FileDetailDTO> Data { get; set; }
}