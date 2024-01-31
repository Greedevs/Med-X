using MedX.Desktop.Components.Employees;
using MedX.Desktop.Windows.Employees;

namespace MedX.Desktop.Pages;

/// <summary>
/// Interaction logic for EmployeesPage.xaml
/// </summary>
// EmployeesPage.xaml.cs
public partial class EmployeesPage : Page
{
    private readonly IEmployeeService service;
    public EmployeesPage(IEmployeeService service)
    {
        InitializeComponent();
        this.service = service;
    }

    private async void BtnCreate_Click(object sender, RoutedEventArgs e)
    {
        EmployeeCreateWindow employeeCreateWindow = new(service: service);
        employeeCreateWindow.ShowDialog();
        await RefreshAsync();
    }

    private async void PageLoaded(object sender, RoutedEventArgs e)
    {
        await RefreshAsync();
    }

    public async Task RefreshAsync(string search = null)
    {
        wrpEmployees.Children.Clear();
        PaginationParams paginationParams = new PaginationParams()
        {
            PageIndex = 1,
            PageSize = 30
        };

        var employees = await service.GetAllAsync(paginationParams, search);

        wrpEmployees.Children.Clear();


        foreach (var employee in employees.Data)
        {
            var employeeCardUserControl = new EmployeeCardUserControl(service);
            employeeCardUserControl.SetData(dto: employee);
            wrpEmployees.Children.Add(element: employeeCardUserControl);
        }
    }

    private async void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        await RefreshAsync(tbSearch.Text);
    }
}

