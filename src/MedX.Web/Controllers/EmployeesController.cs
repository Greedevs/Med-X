using MedX.ApiService.Models.Employees;

namespace MedX.Web.Controllers;

[Route("[controller]/[action]")]
public class EmployeesController(IEmployeeService service) : Controller
{
    public async Task<IActionResult> Index()
    {
        var result = await service.GetAllAsync(new PaginationParams
        {
            PageSize = 10,
            PageIndex = 1
        });

        return View(result.Data);
    }

    [Route("{id?}")]
    public async Task<IActionResult> Detail(long? id)
    {
        var result = await service.GetAsync(id ?? 1);

        return View(result.Data);
    }

    public async Task<IActionResult> Delete(long id)
    {
        var result = await service.DeleteAsync(id);
        return result.StatusCode switch
        {
            200 => RedirectToAction(nameof(Index)),
            _ => View(result.Message)
        };
    }

    public IActionResult Create()
        => View();

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeCreationDto dto)
    {
        var result = await service.AddAsync(dto);
        return result.StatusCode switch
        {
            200 => RedirectToAction(nameof(Index)),
            _ => View(result.Message)
        };
    }

    public IActionResult Edit()
        => View();

    [HttpPost]
    public async Task<IActionResult> Edit(EmployeeUpdateDto dto)
    {
        var result = await service.UpdateAsync(dto);
        return RedirectToAction(nameof(Index));
    }
}
