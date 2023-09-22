using MedX.Domain.Configurations;
using MedX.Service.DTOs.Treatments;
using MedX.Service.Interfaces;
using MedX.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedX.WebApi.Controllers;

public class TreatmentsController : BaseController
{
    private readonly ITreatmentService treatmentService;
    public TreatmentsController(ITreatmentService treatmentService)
    {
        this.treatmentService = treatmentService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(TreatmentCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await treatmentService.AddAsync(dto)
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await treatmentService.DeleteAsync(id)
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(TreatmentUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await treatmentService.UpdateAsync(dto)
        });
    }

    [HttpPut("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await treatmentService.GetAsync(id)
        });
    }

    [HttpPut("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, string search)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await treatmentService.GetAllAsync(@params, search)
        });
    }
}
