using MedX.ApiService.Models.Commons;
using MedX.ApiService.Models.Employees;
using System.Windows.Media.Imaging;

namespace MedX.Desktop.Windows.Employees;

/// <summary>
/// Interaction logic for EmployeeUpdateWindow.xaml
/// </summary>
public partial class EmployeeUpdateWindow : Window
{
    private string? imagePath;
    private readonly IEmployeeService service;
    private readonly Response<EmployeeResultDto> dto;
    public EmployeeUpdateWindow(IEmployeeService service,
           Response<EmployeeResultDto> dto)
    {
        InitializeComponent();
        this.service = service;
        this.dto = dto;
        tbFirstName.Text = dto.Data.FirstName;
        tbLastName.Text = dto.Data.LastName;
        tbPatronymic.Text = dto.Data.Patronymic;
        tbEmail.Text = dto.Data.Email;
        tbPhone.Text = dto.Data.Phone;
        tbProfessional.Text = dto.Data.Professional;
        if(dto.Data.Degree == Degree.Primary)
        {
            rbDegree1.IsChecked = true;
            lSalary.Content = "Persentage";
            tbSalary.IsReadOnly = false;
            tbSalary.Text = dto.Data.Percentage.ToString();
        }
        else
        {
            rbDegree2.IsChecked = true;
            lSalary.Content = "Salary";
            tbSalary.IsReadOnly = false;
            tbSalary.Text = dto.Data.Salary.ToString();
        }
        btnSelectImage.Tag = new BitmapImage(new Uri(dto.Data.Image.FilePath));
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
        OpenFileDialog openFileDialog = new()
        {
            Filter = "PNG files (*.png)|*.png|JPEG files (*.jpeg)|*.jpeg|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|BMP files (*.bmp)|*.bmp"
        };

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

    private async void BtnUpdateEmployee_Click(object sender, RoutedEventArgs e)
    {
        EmployeeUpdateDto employeeUpdateDto = new()
        {
            Id = dto.Data.Id,
            Email = tbEmail.Text,
            Phone = tbPhone.Text,
            Password = tbPassword.Text,
            LastName = tbLastName.Text,
            FirstName = tbFirstName.Text,
            Patronymic = tbPatronymic.Text,
            Professional = tbProfessional.Text,
        };

        if (!string.IsNullOrEmpty(value: imagePath))
            employeeUpdateDto.Image = GetFormFile(imagePath: imagePath);

        if (rbDegree1.IsChecked == true)
        {
            employeeUpdateDto.Degree = Degree.Primary;
            employeeUpdateDto.Percentage = Convert.ToInt32(value: tbSalary.Text);
        }
        else if (rbDegree2.IsChecked == true)
        {
            employeeUpdateDto.Degree = Degree.Secondary;
            employeeUpdateDto.Salary = decimal.Parse(s: tbSalary.Text);
        }

        await service.UpdateAsync(dto: employeeUpdateDto);

        this.Close();

        EmployeesPage employeesPage = new EmployeesPage(service);
        await employeesPage.RefreshAsync();
    }

    public static IFormFile GetFormFile(string imagePath)
    {
        if (!File.Exists(path: imagePath))
            return default!;

        byte[] imageData = File.ReadAllBytes(path: imagePath);
        string fileName = Path.GetFileName(path: imagePath);
        MemoryStream stream = new(buffer: imageData);
        return new FormFile(
            baseStream: stream,
            baseStreamOffset: 0,
            length: imageData.Length,
            name: "Image",
            fileName: fileName);
    }
}