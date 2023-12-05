using MedX.Desktop.Windows.Employees;
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
}

