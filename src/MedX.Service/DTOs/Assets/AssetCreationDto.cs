using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedX.Service.DTOs.Assets;

public class AssetCreationDto
{
    public IFormFile FormFile { get; set; }
}
