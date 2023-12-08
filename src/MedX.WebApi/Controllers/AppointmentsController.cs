using MedX.Domain.Configurations;
using MedX.Service.DTOs.Appointments;
using MedX.Service.Interfaces;
using MedX.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedX.WebApi.Controllers;

public class AppointmentsController : BaseController
{
    private readonly IAppointmentService appointmentService;
    public AppointmentsController(IAppointmentService appointmentService)
    {
        this.appointmentService = appointmentService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(AppointmentCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await appointmentService.AddAsync(dto)
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await appointmentService.DeleteAsync(id)
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(AppointmentUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await appointmentService.UpdateAsync(dto)
        });
    }

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await appointmentService.GetAsync(id)
        });
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, string search)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await appointmentService.GetAllAsync(@params, search)
        });
    }

    [HttpGet("get-all-by-patient/{patientId:long}")]
    public async Task<IActionResult> GetAllByPatientIdAsync(long patientId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await appointmentService.GetAllByPatientIdAsync(patientId)
        });
    }

    [HttpGet("get-all-by-doctor/{doctorId:long}")]
    public async Task<IActionResult> GetAllByDoctorIdAsync(long doctorId, PaginationParams @params, string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await appointmentService.GetAllByDoctorIdAsync(doctorId, @params, search)
        });
    }
}
