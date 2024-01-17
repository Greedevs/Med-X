using System.IO;
using System.Windows;
using Microsoft.Win32;
using MedX.Domain.Enums;
using System.Windows.Input;
using MedX.Desktop.Services;
using Microsoft.AspNetCore.Http;
using MedX.Desktop.Models.Employees;

namespace MedX.Desktop.Windows.Employees;

/// <summary>
/// Interaction logic for EmployeeCreateWindow.xaml
/// </summary>
public partial class EmployeeCreateWindow : Window
{
    private string? imagePath;
    private readonly IEmployeeApiService employeeService = RestService.For<IEmployeeApiService>(HttpConstant.BaseLink);

    public EmployeeCreateWindow()
    {
        InitializeComponent();
    }

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "PNG files (*.png)|*.png|JPEG files (*.jpeg)|*.jpeg|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|BMP files (*.bmp)|*.bmp";

        if (openFileDialog.ShowDialog() == true)
        {
            imagePath = openFileDialog.FileName;
            btnSelectImage.Tag = imagePath;
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

    private async void BtnCreateEmployee_Click(object sender, RoutedEventArgs e)
    {
        EmployeeCreationDto employeeCreationDto = new()
        {
            Email = tbEmail.Text,
            Phone = tbPhone.Text,
            Password = tbPassword.Text,
            LastName = tbLastName.Text,
            FirstName = tbFirstName.Text,
            Patronymic = tbPatronymic.Text,
            Professional = tbProfessional.Text,
        };

        try
        {
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                employeeCreationDto.Image = ConvertToIFormFile(imageBytes, "image.jpg") ?? default!;
            }

            if (rbDegree1.IsChecked == true)
            {
                employeeCreationDto.Degree = Degree.Primary;
                employeeCreationDto.Percentage = Convert.ToInt32(tbSalary.Text);
            }
            else if (rbDegree2.IsChecked == true)
            {
                employeeCreationDto.Degree = Degree.Secondary;
                employeeCreationDto.Salary = Convert.ToInt64(tbSalary.Text);
            }

            var response = await employeeService.AddAsync(employeeCreationDto);

            if (response is not null)
                MessageBox.Show($"Employee created successfully");
            else
                MessageBox.Show(response!.Message);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}");
        }

        this.Close();
    }


    public static IFormFile ConvertToIFormFile(byte[] imageData, string fileName) 
        => new FormFile(new MemoryStream(imageData), 0, imageData.Length, "Image", fileName);
}
