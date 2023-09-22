using MedX.WebApi.Models;
using MedX.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MedX.Domain.Configurations;
using MedX.Service.DTOs.Administrators;

namespace MedX.WebApi.Controllers;

public class AdminsController : BaseController
{
    private readonly IAdminService adminService;
    public AdminsController(IAdminService adminService)
    {
        this.adminService = adminService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(AdminCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await adminService.AddAsync(dto)
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await adminService.DeleteAsync(id)
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(AdminUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await adminService.UpdateAsync(dto)
        });
    }

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await adminService.GetAsync(id)
        });
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, string search)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await adminService.GetAllAsync(@params, search)
        });
    }
}