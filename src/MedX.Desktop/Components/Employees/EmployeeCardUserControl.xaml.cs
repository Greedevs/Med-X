using System.Windows.Media;
using MedX.Service.Helpers;
using System.Windows.Controls;
using MedX.Service.DTOs.Employees;
using System.Windows.Media.Imaging;

namespace MedX.Desktop.Components.Employees;

/// <summary>
/// Interaction logic for EmployeeCardUserControl.xaml
/// </summary>
public partial class EmployeeCardUserControl : UserControl
{
    public long Id { get; private set; }
    public EmployeeCardUserControl()
    {
        InitializeComponent();
    }

    public void SetData(EmployeeResultDto dto)
    {
        var image = dto.Image is null ? "../../../Assets/Images/register-background-image.png"
            : PathHelper.WebRootPath+"/Images/"+dto.Image.FileName;
        Id = dto.Id;
        ImgBrush.ImageSource = new BitmapImage(new Uri(image, UriKind.Relative));
        ImgBrush.Stretch = Stretch.UniformToFill;    
        lbName.Content = dto.FirstName;
        tbDescription.Text = $"{dto.FirstName} {dto.LastName}";
    }
}
