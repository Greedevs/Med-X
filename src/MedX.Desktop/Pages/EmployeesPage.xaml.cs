using MedX.Desktop.Components.Employees;
using MedX.Desktop.Windows.Employees;
using MedX.Domain.Configurations;
using MedX.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MedX.Desktop.Pages;

/// <summary>
/// Interaction logic for EmployeesPage.xaml
/// </summary>
// EmployeesPage.xaml.cs
public partial class EmployeesPage : Page
{
    private readonly IEmployeeService employeeService;

    public EmployeesPage()
    {
        InitializeComponent();
        // EmployeeService obyekti yaratilganda shu yerda o'rnating
        this.employeeService = (IEmployeeService)App.ServiceProvider.GetService(typeof(IEmployeeService));
        if (employeeService == null)
        {
            throw new InvalidOperationException("IEmployeeService not registered in the service provider");
        }
    }

    private void btnCreate_Click(object sender, RoutedEventArgs e)
    {
        EmployeeCreateWindow employeeCreateWindow = new EmployeeCreateWindow(employeeService);
        employeeCreateWindow.ShowDialog();
    }

    private async void PageLoaded(object sender, RoutedEventArgs e)
    {
        wrpEmployees.Children.Clear();
        var paginationParams = new PaginationParams()
        {
            PageIndex = 1,
            PageSize = 10
        };

        var employees = await employeeService.GetAllAsync(paginationParams);
        foreach (var employee in employees)
        {
            var employeeCardUserControl = new EmployeeCardUserControl();
            employeeCardUserControl.SetData(employee);
            wrpEmployees.Children.Add(employeeCardUserControl);
        }
    }
}

