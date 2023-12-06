using MedX.Service.DTOs.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        Id = dto.Id;
        ImgBrush.ImageSource = new BitmapImage(new Uri("C:\\Users\\muqim\\source\\repos\\Med-X\\src\\MedX.Desktop\\Assets\\Images\\register-background-image.png", UriKind.Relative));
        lbName.Content = dto.FirstName;
        tbDescription.Text = $"{dto.FirstName} {dto.LastName}";
    }
}
