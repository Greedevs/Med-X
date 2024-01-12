namespace MedX.Service.Interfaces;

public interface IAssetService
{
    Task<string> UploadAsync(string filePath);
}
