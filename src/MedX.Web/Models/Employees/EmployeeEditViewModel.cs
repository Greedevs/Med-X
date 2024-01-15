using MedX.ApiService.Models.Employees;

namespace MedX.Web.Models.Employees;

public class EmployeeEditViewModel
{
    public EmployeeResultDto ResultDto { get; set; } = default!;
    public EmployeeUpdateDto UpdateDto { get; set; } = default!;
}
