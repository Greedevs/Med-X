using MedX.Domain.Entities.Assets;
using MedX.Service.DTOs.Assets;

namespace MedX.Service.Interfaces;

public interface IAssetService
{
    Task<Asset> UploadAsync(AssetCreationDto dto);
    Task<bool> RemoveAsync(Asset asset);
}
