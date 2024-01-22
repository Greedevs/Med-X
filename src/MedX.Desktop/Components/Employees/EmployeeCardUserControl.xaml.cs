using System.Windows.Media;
using System.Windows.Media.Imaging;
using MedX.ApiService.Models.Employees;
using MedX.Desktop.Helpers;
using MedX.Desktop.Pages;
using MedX.Desktop.Windows.Employees;

namespace MedX.Desktop.Components.Employees;

/// <summary>
/// Interaction logic for EmployeeCardUserControl.xaml
/// </summary>
public partial class EmployeeCardUserControl : UserControl
{
    public long Id { get; private set; }
    private IEmployeeService employeeService;
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
            ImgBrush.ImageSource = new BitmapImage(new Uri(dto.Image.FilePath));

        ImgBrush.Stretch = Stretch.UniformToFill;
        lbFullName.Content = $"{dto.FirstName} {dto.LastName}";
        tbProfessional.Text = dto.Professional;
        tbPhone.Text = dto.Phone;
        tbEmail.Text = dto.Email;
        tbSalary.Text = dto.Salary.ToString();
        tbPersentage.Text = dto.Percentage.ToString() + "%";
    }

    private async void EditItem_Click(object sender, RoutedEventArgs e)
    {
        var existEmployee = await this.employeeService.GetAsync(Id);
        if (existEmployee != null)
        {
            EmployeeUpdateWindow updateWindow = new EmployeeUpdateWindow(employeeService, existEmployee);
            updateWindow.ShowDialog();

            var employeesPage = HelperMethod.FindParent<EmployeesPage>(this);
            if (employeesPage != null)
                await employeesPage.RefreshAsync();
        }
    }

    private async void DeleteItem_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult messageResult = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.OKCancel);
        if (messageResult == MessageBoxResult.OK)
        {
            var result = await this.employeeService.DeleteAsync(Id);
            if (result is null)
            {
                MessageBox.Show("Something went wrong!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var employeesPage = HelperMethod.FindParent<EmployeesPage>(this);
                if (employeesPage != null)
                    await employeesPage.RefreshAsync();
            }
        }
    }

    private void btnMore_Click(object sender, RoutedEventArgs e)
    {
        contextMenu.IsOpen = true;
    }
}
