using System.Windows;
using System.Net.Http;
using MedX.Desktop.Helpers;
using MedX.Desktop.Constants;
using System.Windows.Controls;
using MedX.Service.DTOs.Employees;
using MedX.Desktop.Windows.Employees;
using MedX.Desktop.Components.Employees;
using Newtonsoft.Json;
using MedX.WebApi.Models;

namespace MedX.Desktop.Pages;

/// <summary>
/// Interaction logic for EmployeesPage.xaml
/// </summary>
// EmployeesPage.xaml.cs
public partial class EmployeesPage : Page
{

    private string link;
    private HttpClient httpClient;

    public EmployeesPage()
    {
        InitializeComponent();
        httpClient = new HttpClient();
        link = HttpConstant.BaseLink + "api/employees/";
    }

    private void btnCreate_Click(object sender, RoutedEventArgs e)
    {
        EmployeeCreateWindow employeeCreateWindow = new EmployeeCreateWindow();
        employeeCreateWindow.ShowDialog();
    }

    private async void PageLoaded(object sender, RoutedEventArgs e)
    {
        wrpEmployees.Children.Clear();

        //http://localhost:5298/api/Employees/get-all?PageSize=10&PageIndex=1
        string uri = $"{link}get-all?PageSize=10&PageIndex={1}";

        var response = await httpClient.GetAsync(uri);

        var employees = await ContentHelper.GetContentAsync<List<EmployeeResultDto>>(response);

        foreach (var employee in employees)
        {
            var employeeCardUserControl = new EmployeeCardUserControl();
            employeeCardUserControl.SetData(employee);
            wrpEmployees.Children.Add(employeeCardUserControl);
        } 
    }
}

