using MedX.Service.Interfaces;

namespace MedX.Service.Services;

public class AssetService : IAssetService
{
    public async Task<string> UploadAsync(string filePath)
    {
        if (!Directory.Exists("Images"))
            Directory.CreateDirectory("Images");

        FileInfo fileInfo = new FileInfo(filePath);

        var imageName = "IMG" + Guid.NewGuid().ToString() + fileInfo.Extension;

        var path = Path.Combine("Images", imageName);
        byte[] image = await File.ReadAllBytesAsync(filePath);
        await File.WriteAllBytesAsync(path, image);
        return path;
    }
}
