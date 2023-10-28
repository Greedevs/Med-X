using MedX.Data.IRepositories;
using MedX.Domain.Entities.Assets;
using MedX.Service.DTOs.Assets;
using MedX.Service.Extensions;
using MedX.Service.Helpers;
using MedX.Service.Interfaces;

namespace MedX.Service.Services;

public class AssetService : IAssetService
{
    private readonly IRepository<Asset> repository;
    public AssetService(IRepository<Asset> repository)
    {
        this.repository = repository;
    }

    public async Task<Asset> UploadAsync(AssetCreationDto dto)
    {
        var webRootPath = Path.Combine(PathHelper.WebRootPath, "Images");

        if (!Directory.Exists(webRootPath))
            Directory.CreateDirectory(webRootPath);

        var fileExtention = Path.GetExtension(dto.FormFile.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtention}";
        var filePath = Path.Combine(webRootPath, fileName);

        var fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
        await fileStream.WriteAsync(dto.FormFile.ToByte());

        var asset = new Asset()
        {
            FileName = fileName,
            FilePath = filePath,
        };
        await this.repository.CreateAsync(asset);
        await this.repository.SaveChanges();
        return asset;
    }

    public async Task<bool> RemoveAsync(Asset asset)
    {
        if (asset is null)
            return false;

        var existAsset = await repository.GetAsync(a => a.Id.Equals(asset.Id));
        if (existAsset is null)
            return false;

        this.repository.Delete(existAsset);
        await this.repository.SaveChanges();
        return true;
    }
}
