using System.Windows;
using Microsoft.Win32;
using MedX.Domain.Enums;
using System.Windows.Input;
using MedX.Service.Interfaces;
using MedX.Service.DTOs.Employees;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using MedX.Desktop.Constants;
using MedX.Desktop.Helpers;
using System.Security.Policy;

namespace MedX.Desktop.Windows.Employees;

/// <summary>
/// Interaction logic for EmployeeCreateWindow.xaml
/// </summary>
public partial class EmployeeCreateWindow : Window
{
    private string imagePath;
    private string link;
    private HttpClient httpClient;

    public EmployeeCreateWindow()
    {
        InitializeComponent();
        httpClient = new HttpClient();
        link = HttpConstant.BaseLink + "api/employees/";
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
        EmployeeCreationDto employeeCreationDto = new()
        {
            FirstName = tbFirstName.Text,
            LastName = tbLastName.Text,
            Patronymic = tbPatronymic.Text,
            Email = tbEmail.Text,
            Phone = tbPhone.Text,
            Professional = tbProfessional.Text,
            Password = tbPassword.Text,
        };

        if (rbDegree1.IsChecked == true)
        {
            employeeCreationDto.Degree = Degree.Primary;
            employeeCreationDto.Percentage = Convert.ToInt32(tbSalary.Text);
        }
        else if (rbDegree2.IsChecked == true)
        {
            employeeCreationDto.Degree = Degree.Secondary;
            employeeCreationDto.Salary = Convert.ToDecimal(tbSalary.Text);
        }

        using FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
        employeeCreationDto.Image = new FormFile(stream, 0, stream.Length, null!, Path.GetFileName(stream.Name))
        {
            Headers = new HeaderDictionary(),
            ContentType = "image/*"
        };

        var content = ContentHelper.GetContent(employeeCreationDto);
        var response = await httpClient.PostAsync(link, content);
        var resultDto = await ContentHelper.GetContentAsync<EmployeeResultDto>(response);

        if (resultDto is not null)
            MessageBox.Show($"{resultDto.FirstName} {resultDto.LastName} employee created");
        else MessageBox.Show("Something goes wrong");
        this.Close();
    }
}
