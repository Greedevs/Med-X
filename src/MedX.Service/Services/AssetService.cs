using MedX.Data.IRepositories;
using MedX.Domain.Entities.Assets;
using MedX.Service.DTOs.Assets;
using MedX.Service.Helpers;
using MedX.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MedX.Service.Services;

public class AssetService : IAssetService
{
    private readonly IRepository<Asset> repository;
    private readonly IHttpContextAccessor httpContextAccessor;
    public AssetService(IRepository<Asset> repository, IHttpContextAccessor httpContextAccessor)
    {
        this.repository = repository;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<Asset> UploadAsync(AssetCreationDto dto)
    {
        var webRootPath = Path.Combine(PathHelper.WebRootPath, "Images");

        if (!Directory.Exists(webRootPath))
            Directory.CreateDirectory(webRootPath);

        var fileExtension = Path.GetExtension(dto.FormFile.FileName);
        var fileName = $"{Guid.NewGuid().ToString("N")}{fileExtension}";
        var filePath = Path.Combine(webRootPath, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await dto.FormFile.CopyToAsync(fileStream);
        }

        var imageUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/Images/{fileName}";

        var asset = new Asset()
        {
            FileName = fileName,
            FilePath = imageUrl,
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
