using MedX.Desktop.Pages;
using MedX.Domain.Enums;
using MedX.Service.DTOs.Employees;
using MedX.Service.Interfaces;
using MedX.Service.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MedX.Desktop.Windows.Employees;

/// <summary>
/// Interaction logic for EmployeeUpdateWindow.xaml
/// </summary>
public partial class EmployeeUpdateWindow : Window
{
    private string? imagePath;
    private readonly IEmployeeService employeeService;
    private EmployeeResultDto dto;
    public EmployeeUpdateWindow(IEmployeeService employeeService, EmployeeResultDto dto)
    {
        InitializeComponent();
        this.employeeService = employeeService;
        this.dto = dto;
        tbFirstName.Text = dto.FirstName;
        tbLastName.Text = dto.LastName;
        tbPatronymic.Text = dto.Patronymic;
        tbEmail.Text = dto.Email;
        tbPhone.Text = dto.Phone;
        tbProfessional.Text = dto.Professional;
        btnSelectImage.Tag = new BitmapImage(new Uri(dto.Image, UriKind.RelativeOrAbsolute));
        if (dto.Degree == Degree.Primary)
        {
            lSalary.Content = "Persentage";
            tbSalary.IsReadOnly = false;
            rbDegree1.IsChecked = true;
            tbSalary.Text = dto.Percentage.ToString();
        }
        else 
        {
            lSalary.Content = "Salary";
            tbSalary.IsReadOnly = false;
            rbDegree2.IsChecked = true;
            tbSalary.Text = dto.Salary.ToString();
        }
    }

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    bool isSelectImage = false;
    private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "PNG files (*.png)|*.png|JPEG files (*.jpeg)|*.jpeg|JPG files (*.jpg)|*.jpg|JFIF files (*.jfif)|*.jfif|GIF files (*.gif)|*.gif|BMP files (*.bmp)|*.bmp";


        if (openFileDialog.ShowDialog() == true)
        {
            imagePath = openFileDialog.FileName;
            ImBImage.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            btnSelectImage.Tag = imagePath;
            isSelectImage = true;
        }
    }

    private void RbDegree1_Checked(object sender, RoutedEventArgs e)
    {
        lSalary.Content = "Persentage";
        tbSalary.IsReadOnly = false;
    }

    private void RbDegree2_Checked(object sender, RoutedEventArgs e)
    {
        lSalary.Content = "Salary";
        tbSalary.IsReadOnly = false;
    }


    private async void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
    {
        var updateEmployee = new EmployeeUpdateDto()
        {
            Id = dto.Id,
            FirstName = tbFirstName.Text,
            LastName = tbLastName.Text,
            Patronymic = tbPatronymic.Text,
            Email = tbEmail.Text,
            Phone = tbPhone.Text,
            Professional = tbProfessional.Text,
            Image = dto.Image,
        };

        try
        {
            if (isSelectImage)
            {

                string imagePath = ImBImage.ImageSource.ToString();

                if (!string.IsNullOrEmpty(imagePath))
                {
                    updateEmployee.Image = imagePath;
                    updateEmployee.IsSelectImage = true;
                }
            }
            else { updateEmployee.IsSelectImage = false;}

            if (rbDegree1.IsChecked == true)
            {
                updateEmployee.Degree = Degree.Primary;
                updateEmployee.Percentage = int.Parse(tbSalary.Text);
            }
            else if (rbDegree2.IsChecked == true)
            {
                updateEmployee.Degree = Degree.Secondary;
                updateEmployee.Salary = decimal.Parse(tbSalary.Text);
            }

            var response = await employeeService.UpdateAsync(updateEmployee);

            if (response is not null)
                MessageBox.Show($"Employee updated successfully");
            else
                MessageBox.Show("Employee no update");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}");
        }

        this.Close();

        EmployeesPage employeesPage = new EmployeesPage(employeeService);
        await employeesPage.RefreshAsync();
    }
}
