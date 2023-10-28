using Microsoft.AspNetCore.Http;

namespace MedX.Service.DTOs.Assets;

public class AssetCreationDto
{
    public IFormFile FormFile { get; set; }
}
