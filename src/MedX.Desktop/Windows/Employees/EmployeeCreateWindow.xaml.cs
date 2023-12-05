using MedX.Domain.Enums;
using MedX.Service.DTOs.Doctors;
using MedX.Service.Interfaces;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace MedX.Desktop.Windows.Employees;

/// <summary>
/// Interaction logic for EmployeeCreateWindow.xaml
/// </summary>
public partial class EmployeeCreateWindow : Window
{
    private readonly IEmployeeService employeeService;

    public EmployeeCreateWindow(IEmployeeService employeeService)
    {
        InitializeComponent();
        this.employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
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
            string path = openFileDialog.FileName;
            btnSelectImage.Tag = path;
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

        var resultDto = await employeeService.AddAsync(employeeCreationDto);
        if (resultDto is not null)
            MessageBox.Show($"{resultDto.FirstName} {resultDto.LastName} employee created");
        else MessageBox.Show("Something goes wrong");
    }
}
