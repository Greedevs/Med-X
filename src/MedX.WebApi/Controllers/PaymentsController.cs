using MedX.Domain.Configurations;
using MedX.Service.DTOs.Payments;
using MedX.Service.Interfaces;
using MedX.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedX.WebApi.Controllers;

public class PaymentsController : BaseController
{
    private readonly IPaymentService paymentService;
    public PaymentsController(IPaymentService paymentService)
    {
        this.paymentService = paymentService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] PaymentCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await paymentService.AddAsync(dto)
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await paymentService.DeleteAsync(id)
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromForm] PaymentUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await paymentService.UpdateAsync(dto)
        });
    }

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await paymentService.GetAsync(id)
        });
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, string search)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await paymentService.GetAllAsync(@params, search)
        });
    }

    [HttpGet("get-all-by-patient/{patientId:long}")]
    public async Task<IActionResult> GetAllByPatientIdAsync(long patientId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await paymentService.GetAllByPatientIdAsync(patientId)
        });
    }
}
