using MedX.Desktop.Windows.Employees;
using MedX.Desktop.Components.Employees;

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

    public async Task RefreshAsync()
    {
        wrpEmployees.Children.Clear();

        #region Old Code with HttpClient
        ////http://localhost:5298/api/Employees/get-all?PageSize=10&PageIndex=1
        //string uri = $"{link}get-all?PageSize=10&PageIndex={1}";
        //HttpClient httpClient = new();
        //var response = await httpClient.GetAsync(uri);
        //var employees = await ContentHelper.GetContentAsync<List<EmployeeResultDto>>(response);
        #endregion
        PaginationParams paginationParams = new PaginationParams()
        {
            PageIndex = 1,
            PageSize = 30
        };

        var employees = await service.GetAllAsync(paginationParams);

        foreach (var employee in employees.Data)
        {
            var employeeCardUserControl = new EmployeeCardUserControl(service);
            employeeCardUserControl.SetData(dto: employee);
            wrpEmployees.Children.Add(element: employeeCardUserControl);
        }
    }
}

