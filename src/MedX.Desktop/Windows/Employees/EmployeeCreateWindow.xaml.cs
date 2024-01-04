using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Net.Http;
using MedX.Domain.Enums;
using System.Windows.Input;
using MedX.Desktop.Helpers;
using MedX.Desktop.Constants;
using Microsoft.AspNetCore.Http;
using MedX.Service.DTOs.Employees;
using System.Net.Http.Headers;
using System.Windows.Media;

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
        link = HttpConstant.BaseLink + "api/employees/create/";
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
        EmployeeCreationDto employeeCreationDto = new();
        using (var multipartFormContent = new MultipartFormDataContent())
        {
            multipartFormContent.Add(new StringContent(tbFirstName.Text), nameof(employeeCreationDto.FirstName));
            multipartFormContent.Add(new StringContent(tbLastName.Text), nameof(employeeCreationDto.LastName));
            multipartFormContent.Add(new StringContent(tbPatronymic.Text), nameof(employeeCreationDto.Patronymic));
            multipartFormContent.Add(new StringContent(tbEmail.Text), nameof(employeeCreationDto.Email));
            multipartFormContent.Add(new StringContent(tbPhone.Text), nameof(employeeCreationDto.Phone));
            multipartFormContent.Add(new StringContent(tbProfessional.Text), nameof(employeeCreationDto.Professional));
            multipartFormContent.Add(new StringContent(tbPassword.Text), nameof(employeeCreationDto.Password));

            if(imagePath is not null)
            {
                var fileStreamContent = new StreamContent(File.OpenRead(imagePath));
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/*");
                multipartFormContent.Add(fileStreamContent, name: nameof(employeeCreationDto.Image), fileName: "image" + Path.GetExtension(imagePath));
            }

            if (rbDegree1.IsChecked == true)
            {
                multipartFormContent.Add(new StringContent(Degree.Primary.ToString()), nameof(employeeCreationDto.Degree));
                multipartFormContent.Add(new StringContent(tbSalary.Text), nameof(employeeCreationDto.Percentage));
            }
            else if (rbDegree2.IsChecked == true)
            {
                multipartFormContent.Add(new StringContent(Degree.Secondary.ToString()), nameof(employeeCreationDto.Degree));
                multipartFormContent.Add(new StringContent(tbSalary.Text), nameof(employeeCreationDto.Salary));
            }

            //var content = ContentHelper.GetContent(employeeCreationDto);
            var response = await httpClient.PostAsync(link, multipartFormContent);
            response.EnsureSuccessStatusCode();

            //var resultDto = await ContentHelper.GetContentAsync<EmployeeResultDto>(response);

            if (response is not null)
                MessageBox.Show($"Employee created successfully");
            else MessageBox.Show(await response.Content.ReadAsStringAsync());
            this.Close();
        }
    }
}
