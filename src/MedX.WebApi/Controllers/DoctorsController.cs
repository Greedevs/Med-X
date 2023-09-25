using MedX.Domain.Configurations;
using MedX.Service.DTOs.Doctors;
using MedX.Service.Interfaces;
using MedX.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedX.WebApi.Controllers;

public class DoctorsController : BaseController
{
    private readonly IDoctorService doctorService;
    public DoctorsController(IDoctorService doctorService)
    {
        this.doctorService = doctorService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(DoctorCreationDto dto)
        => Ok(new Response { Data = await doctorService.AddAsync(dto) });

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(new Response { Data = await doctorService.DeleteAsync(id) });

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(DoctorUpdateDto dto)
        => Ok(new Response { Data = await doctorService.UpdateAsync(dto) });

    [HttpPut("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
        => Ok(new Response { Data = await doctorService.GetAsync(id) });

    [HttpPut("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, string search)
    => Ok(new Response { Data = await doctorService.GetAllAsync(@params, search) });
}
