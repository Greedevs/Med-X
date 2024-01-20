using System.Windows.Media;
using System.Windows.Media.Imaging;
using MedX.ApiService.Models.Employees;
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
        lbName.Content = dto.FirstName;
        tbDescription.Text = $"{dto.FirstName} {dto.LastName}";
    }

    private async void btnUpdate_Click(object sender, RoutedEventArgs e)
    {
        var existEmployee = await this.employeeService.GetAsync(Id);
        if (existEmployee != null)
        {
            EmployeeUpdateWindow updateWindow = new EmployeeUpdateWindow(employeeService, existEmployee);
            updateWindow.ShowDialog();
        }
    }
}
