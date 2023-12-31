﻿using System.Windows;
using MedX.Desktop.Services;
using System.Windows.Controls;
using MedX.Desktop.Windows.Employees;
using MedX.Desktop.Components.Employees;

namespace MedX.Desktop.Pages;

/// <summary>
/// Interaction logic for EmployeesPage.xaml
/// </summary>
// EmployeesPage.xaml.cs
public partial class EmployeesPage : Page
{
    private readonly IEmployeeApiService employeeService;

    public EmployeesPage()
    {
        InitializeComponent();
        employeeService = RestService.For<IEmployeeApiService>(HttpConstant.BaseLink);
    }

    private void BtnCreate_Click(object sender, RoutedEventArgs e)
    {
        EmployeeCreateWindow employeeCreateWindow = new EmployeeCreateWindow();
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

        var employees = await employeeService.GetAllAsync();

        foreach (var employee in employees.Data)
        {
            var employeeCardUserControl = new EmployeeCardUserControl();
            employeeCardUserControl.SetData(employee);
            wrpEmployees.Children.Add(employeeCardUserControl);
        } 
    }
}

