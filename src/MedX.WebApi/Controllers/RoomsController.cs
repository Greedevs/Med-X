using MedX.Domain.Configurations;
using MedX.Service.DTOs.Rooms;
using MedX.Service.Interfaces;
using MedX.Desktop.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedX.Desktop.Controllers;

public class RoomsController : BaseController
{
    private readonly IRoomService roomService;
    public RoomsController(IRoomService roomService)
    {
        this.roomService = roomService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync([FromForm] RoomCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await roomService.AddAsync(dto)
        });
    }

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await roomService.DeleteAsync(id)
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(RoomUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await roomService.UpdateAsync(dto)
        });
    }

    [HttpGet("get/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await roomService.GetAsync(id)
        });
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, [FromQuery] int? search)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "OK",
            Data = await roomService.GetAllAsync(@params, search)
        });
    }
}
