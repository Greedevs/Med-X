namespace MedX.ApiService.Models.Assets;

public class AssetResultDto
{
    public long Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}
