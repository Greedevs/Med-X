using MedX.Domain.Configurations;
using MedX.Service.DTOs.MedicalRecords;
using MedX.Service.Interfaces;
using MedX.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedX.WebApi.Controllers;

public class MedicalRecordsControllers : BaseController
{
    private readonly IMedicalRecordService recordService;
    public MedicalRecordsControllers(IMedicalRecordService recordService)
    {
        this.recordService = recordService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(MedicalRecordCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await recordService.AddAsync(dto)
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await recordService.DeleteAsync(id)
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(MedicalRecordUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await recordService.UpdateAsync(dto)
        });
    }

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await recordService.GetAsync(id)
        });
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, string search)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await recordService.GetAllAsync(@params, search)
        });
    }

    [HttpGet("get-all-by-patient/{patientId:long}")]
    public async Task<IActionResult> GetAllByPatientIdAsync(long patientId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await recordService.GetAllByPatientIdAsync(patientId)
        });
    }

    [HttpGet("get-all-by-doctor/{doctorId:long}")]
    public async Task<IActionResult> GetAllByDoctorIdAsync(long doctorId, PaginationParams @params, string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await recordService.GetAllByDoctorIdAsync(doctorId, @params, search)
        });
    }
}
