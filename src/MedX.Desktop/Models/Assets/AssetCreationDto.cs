using Microsoft.AspNetCore.Http;

namespace MedX.Desktop.Models.Assets;

public class AssetCreationDto
{
    public IFormFile FormFile { get; set; }
}
