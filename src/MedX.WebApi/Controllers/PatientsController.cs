using MedX.Domain.Configurations;
using MedX.Service.DTOs.Patients;
using MedX.Service.Interfaces;
using MedX.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedX.WebApi.Controllers;

public class PatientsController : BaseController
{
    private readonly IPatientService patientService;
    public PatientsController(IPatientService patientService)
    {
        this.patientService = patientService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(PatientCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await patientService.AddAsync(dto)
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await patientService.DeleteAsync(id)
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(PatientUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await patientService.UpdateAsync(dto)
        });
    }

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await patientService.GetAsync(id)
        });
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, string search)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await patientService.GetAllAsync(@params, search)
        });
    }
}
