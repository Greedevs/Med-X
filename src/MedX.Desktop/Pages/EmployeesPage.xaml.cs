using System.Windows;
using MedX.Desktop.Services;
using System.Windows.Controls;
using MedX.Desktop.Windows.Employees;
using MedX.Desktop.Components.Employees;
using MedX.Service.Interfaces;

namespace MedX.Desktop.Pages;

/// <summary>
/// Interaction logic for EmployeesPage.xaml
/// </summary>
// EmployeesPage.xaml.cs
public partial class EmployeesPage : Page
{
    private readonly IEmployeeService employeeService;

    public EmployeesPage(IEmployeeService employeeService)
    {
        InitializeComponent();
        this.employeeService = employeeService;
    }

    private void BtnCreate_Click(object sender, RoutedEventArgs e)
    {
        EmployeeCreateWindow employeeCreateWindow = new EmployeeCreateWindow(employeeService);
        employeeCreateWindow.ShowDialog();
    }

    private async void PageLoaded(object sender, RoutedEventArgs e)
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
            PageSize = 20,
        };

        var employees = await employeeService.GetAllAsync(paginationParams);
        if (employees is not null)
        {
            foreach (var employee in employees)
            {
                var employeeCardUserControl = new EmployeeCardUserControl();
                employeeCardUserControl.SetData(employee);
                wrpEmployees.Children.Add(employeeCardUserControl);
            }
        }
    }
}

