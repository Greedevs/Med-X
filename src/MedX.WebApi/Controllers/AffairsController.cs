using MedX.Domain.Configurations;
using MedX.Service.DTOs.Services;
using MedX.Service.Interfaces;
using MedX.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedX.WebApi.Controllers;

public class AffairsController : BaseController
{
    private readonly IAffairService service;
    public AffairsController(IAffairService service)
    {
        this.service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(AffairCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await service.AddAsync(dto)
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await service.DeleteAsync(id)
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(AffairUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await service.UpdateAsync(dto)
        });
    }

    [HttpPut("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await service.GetAsync(id)
        });
    }

    [HttpPut("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await service.GetAllAsync(@params, search)
        });
    }
}
