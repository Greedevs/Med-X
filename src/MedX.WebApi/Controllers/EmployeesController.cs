﻿using MedX.Domain.Configurations;
using MedX.Service.DTOs.Employees;
using MedX.Service.Interfaces;
using MedX.Desktop.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedX.Desktop.Controllers;

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
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromForm] EmployeeUpdateDto dto)
        => Ok(new Response { Data = await employeeService.UpdateAsync(dto) });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response { Data = await employeeService.DeleteAsync(id) });

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response { Data = await employeeService.GetAsync(id) });

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, string search)
    => Ok(new Response { Data = await employeeService.GetAllAsync(@params, search) });
    
    [HttpGet("get-all-doctor")]
    public async Task<IActionResult> GetAllDoctorAsync([FromQuery] PaginationParams @params, string search)
    => Ok(new Response { Data = await employeeService.GetAllDoctorAsync(@params, search) });
}
