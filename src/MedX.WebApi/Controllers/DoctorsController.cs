using MedX.Service.DTOs.Doctors;
using MedX.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MedX.WebApi.Controllers;

public class DoctorsController : BaseController
{
    private readonly IDoctorService doctorService;
    public DoctorsController(IDoctorService doctorService)
    {
        this.doctorService = doctorService;
    }

    [HttpPost]

    public Task<IActionResult> PostAsync(DoctorCreationDto dto)
    {
        return Ok()
    }
}
