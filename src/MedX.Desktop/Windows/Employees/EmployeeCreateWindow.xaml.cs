using System.Windows;

namespace MedX.Desktop.Windows.Employees;

/// <summary>
/// Interaction logic for EmployeeCreateWindow.xaml
/// </summary>
public partial class EmployeeCreateWindow : Window
{
    public EmployeeCreateWindow()
    {
        InitializeComponent();
    }

    private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
