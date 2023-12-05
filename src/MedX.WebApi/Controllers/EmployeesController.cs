using MedX.Domain.Configurations;
using MedX.Service.DTOs.Doctors;
using MedX.Service.Interfaces;
using MedX.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedX.WebApi.Controllers;

public class EmployeesController : BaseController
{
    private readonly IEmployeeService employeeService;
    public EmployeesController(IEmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] EmployeeCreationDto dto)
        => Ok(new Response { Data = await employeeService.AddAsync(dto) });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response { Data = await employeeService.DeleteAsync(id) });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromForm] EmployeeUpdateDto dto)
        => Ok(new Response { Data = await employeeService.UpdateAsync(dto) });

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response { Data = await employeeService.GetAsync(id) });

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, string search)
    => Ok(new Response { Data = await employeeService.GetAllAsync(@params, search) });
}
