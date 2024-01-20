using MedX.ApiService.Models.Employees;

namespace MedX.Desktop.Windows.Employees;

/// <summary>
/// Interaction logic for EmployeeCreateWindow.xaml
/// </summary>
public partial class EmployeeCreateWindow : Window
{
    private string? imagePath;
    private readonly IEmployeeService service;

    public EmployeeCreateWindow(IEmployeeService service)
    {
        InitializeComponent();
        this.service = service;
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

        if(!string.IsNullOrEmpty(value: imagePath))
            employeeCreationDto.Image = GetFormFile(imagePath: imagePath);

        if (rbDegree1.IsChecked == true)
        {
            employeeCreationDto.Degree = Degree.Primary;
            employeeCreationDto.Percentage = Convert.ToInt32(value: tbSalary.Text);
        }
        else if (rbDegree2.IsChecked == true)
        {
            employeeCreationDto.Degree = Degree.Secondary;
            employeeCreationDto.Salary = decimal.Parse(s: tbSalary.Text);
        }

        await service.AddAsync(dto: employeeCreationDto);

        this.Close();
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
