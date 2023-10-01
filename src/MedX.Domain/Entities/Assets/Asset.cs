using MedX.Domain.Commons;

namespace MedX.Domain.Entities.Assets;

public class Asset : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
}