using System.Windows;
using MedX.Desktop.Pages;
using System.Windows.Input;
using System.Windows.Threading;

namespace MedX.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
    }

    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (this.WindowState == WindowState.Maximized)
            this.WindowState = WindowState.Normal;
        this.DragMove();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void MaximazeButton_Click(object sender, RoutedEventArgs e)
    {
        if (this.WindowState == WindowState.Maximized)
        {
            this.WindowState = WindowState.Normal;
            PageNavigator.Margin = new Thickness(0);
        }
        else
        {
            this.WindowState = WindowState.Maximized;
            PageNavigator.Margin = new Thickness(10, 0, 10, 0);
        }
        this.WindowStyle = WindowStyle.None;
    }

    protected override void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
        Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => WindowStyle = WindowStyle.None));
    }

    private void rbDashboard_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new DashboardPage();
    }

    private void rbDoctors_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new EmployeesPage();
    }

    private void rbPatients_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new PatientsPage();
    }

    private void rbAffairs_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new ReportsPage();
    }

    private void rbCashDesk_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new CashDesksPage();
    }

    private void rbRooms_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new RoomsPage();
    }

    private void rbReports_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new ReportsPage();
    }

    private void rbAbout_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new AboutPage();
    }

    private void rbEmployees_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new EmployeesPage();
    }

    private void rbInformation_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new InformationPage();
    }

    private void WindowLoaded(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new EmployeesPage();
        rbDoctors.IsChecked = true;
    }
}
