using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MedX.Service.DTOs.Employees;
using MedX.Service.Interfaces;
using System.Windows;
using MedX.Desktop.Pages;
using MedX.Desktop.Windows.Employees;

namespace MedX.Desktop.Components.Employees;

/// <summary>
/// Interaction logic for EmployeeCardUserControl.xaml
/// </summary>
public partial class EmployeeCardUserControl : UserControl
{
    public long Id { get; private set; }
    private readonly IEmployeeService employeeService;
    public EmployeeCardUserControl(IEmployeeService employeeService)
    {
        InitializeComponent();
        this.employeeService = employeeService;
    }

    public void SetData(EmployeeResultDto dto)
    {
        Id = dto.Id;
        if (dto.Image is null)
            ImgBrush.ImageSource = new BitmapImage(new Uri("../../../Assets/Images/register-background-image.png", UriKind.Relative));
        else
            ImgBrush.ImageSource = new BitmapImage(new Uri(dto.Image, UriKind.Relative));

        ImgBrush.Stretch = Stretch.UniformToFill;
        lbFullName.Content = $"{dto.FirstName} {dto.LastName}";
        tbProfessional.Text = dto.Professional;
        tbPatronymic.Text = dto.Patronymic;
        tbPhone.Text = dto.Phone;
        tbEmail.Text = dto.Email;
    }

    private async void DeleteItem_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var result = await this.employeeService.DeleteAsync(Id);
        if (result)
            MessageBox.Show("Employee deleted successfully!");
        else
            MessageBox.Show("Failed to delete employee. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        
        EmployeesPage employeesPage = new EmployeesPage(employeeService);
        await employeesPage.RefreshAsync();
    }

    private async void EditItem_Click(object sender, RoutedEventArgs e)
    {
        var existEmployee = await this.employeeService.GetAsync(Id);

        EmployeeUpdateWindow updateWindow = new EmployeeUpdateWindow(employeeService, existEmployee);
        updateWindow.ShowDialog();
    }
}
