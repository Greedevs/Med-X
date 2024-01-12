using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using MedX.Desktop.Services;
using Microsoft.AspNetCore.Http;
using MedX.Service.DTOs.Employees;
using MedX.Domain.Enums;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Media.Imaging;
using MedX.Service.Interfaces;

namespace MedX.Desktop.Windows.Employees;

/// <summary>
/// Interaction logic for EmployeeCreateWindow.xaml
/// </summary>
public partial class EmployeeCreateWindow : Window
{
    private string? imagePath;
    private readonly HttpClient httpClient = new HttpClient();
    private readonly IEmployeeService employeeService;
    public EmployeeCreateWindow(IEmployeeService employeeService)
    {
        InitializeComponent();
        this.employeeService = employeeService;
    }

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void btnSelectImage_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "PNG files (*.png)|*.png|JPEG files (*.jpeg)|*.jpeg|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|BMP files (*.bmp)|*.bmp";

        if (openFileDialog.ShowDialog() == true)
        {
            imagePath = openFileDialog.FileName;
            ImBImage.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            btnSelectImage.Tag = imagePath;
        }
    }

    private void rbDegree1_Checked(object sender, RoutedEventArgs e)
    {
        lSalary.Content = "Persentage";
        tbSalary.IsReadOnly = false;
    }

    private void rbDegree2_Checked(object sender, RoutedEventArgs e)
    {
        lSalary.Content = "Salary";
        tbSalary.IsReadOnly = false;
    }

    private async void btnCreateEmployee_Click(object sender, RoutedEventArgs e)
    {
        PaginationParams paginationParams = new PaginationParams()
        {
            PageIndex = 1,
            PageSize = 20,
        };

        var emps = await employeeService.GetAllAsync(paginationParams);

        // Validate imagePath
        if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
        {
            MessageBox.Show("Invalid image file path.");
            return;
        }

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
            string imagePath = ImBImage.ImageSource.ToString();

            if (!string.IsNullOrEmpty(imagePath))
            {
                employeeCreationDto.Image = imagePath;
            }

            if (rbDegree1.IsChecked == true)
            {
                employeeCreationDto.Degree = Degree.Primary;
                employeeCreationDto.Percentage = int.Parse(tbSalary.Text);
            }
            else if (rbDegree2.IsChecked == true)
            {
                employeeCreationDto.Degree = Degree.Secondary;
                employeeCreationDto.Salary = decimal.Parse(tbSalary.Text);
            }

            var response = await employeeService.AddAsync(employeeCreationDto);

            if (response is not null)
                MessageBox.Show($"Employee created successfully");
            else
                MessageBox.Show("Employee no create");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}");
        }

        this.Close();
    }




    //private async void btnCreateEmployee_Click(object sender, RoutedEventArgs e)
    //{
    //    EmployeeCreationDto employeeCreationDto = new();
    //    using var multipartFormContent = new MultipartFormDataContent
    //    {
    //        { new StringContent(tbFirstName.Text), nameof(employeeCreationDto.FirstName) },
    //        { new StringContent(tbLastName.Text), nameof(employeeCreationDto.LastName) },
    //        { new StringContent(tbPatronymic.Text), nameof(employeeCreationDto.Patronymic) },
    //        { new StringContent(tbEmail.Text), nameof(employeeCreationDto.Email) },
    //        { new StringContent(tbPhone.Text), nameof(employeeCreationDto.Phone) },
    //        { new StringContent(tbProfessional.Text), nameof(employeeCreationDto.Professional) },
    //        { new StringContent(tbPassword.Text), nameof(employeeCreationDto.Password) }
    //    };

    //    if (imagePath is not null)
    //    {
    //        var fileStreamContent = new StreamContent(File.OpenRead(imagePath));
    //        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/*");
    //        multipartFormContent.Add(fileStreamContent, name: nameof(employeeCreationDto.Image), fileName: "image" + Path.GetExtension(imagePath));
    //    }

    //    if (rbDegree1.IsChecked == true)
    //    {
    //        multipartFormContent.Add(new StringContent(Degree.Primary.ToString()), nameof(employeeCreationDto.Degree));
    //        multipartFormContent.Add(new StringContent(tbSalary.Text), nameof(employeeCreationDto.Percentage));
    //    }
    //    else if (rbDegree2.IsChecked == true)
    //    {
    //        multipartFormContent.Add(new StringContent(Degree.Secondary.ToString()), nameof(employeeCreationDto.Degree));
    //        multipartFormContent.Add(new StringContent(tbSalary.Text), nameof(employeeCreationDto.Salary));
    //    }


    //    var response = await httpClient.PostAsync("http://52.221.226.79/api/Employees/create", multipartFormContent);
    //    response.EnsureSuccessStatusCode();


    //    if (response is not null)
    //        MessageBox.Show($"Employee created successfully");
    //    else MessageBox.Show(await response.Content.ReadAsStringAsync());
    //    this.Close();
    //}
}
