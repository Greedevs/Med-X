using System.Windows.Controls.Primitives;

namespace MedX.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IServiceProvider services;

    public MainWindow(IServiceProvider services)
    {
        InitializeComponent();
        this.services = services;
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

    private void RbDashboard_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new DashboardPage();
    }

    private void RbDoctors_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new EmployeesPage(services.
            GetRequiredService<IEmployeeService>());
    }

    private void RbPatients_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new PatientsPage(services.
            GetRequiredService<IPatientService>());
    }

    private void RbAffairs_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new ReportsPage();
    }

    private void RbCashDesk_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new CashDesksPage();
    }

    private void RbRooms_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new RoomsPage();
    }

    private void RbReports_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new ReportsPage();
    }

    private void RbAbout_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new AboutPage();
    }

    private void RbEmployees_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new EmployeesPage(services.
            GetRequiredService<IEmployeeService>());
    }

    private void RbInformation_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new InformationPage();
    }

    private void WindowLoaded(object sender, RoutedEventArgs e)
    {
        if (services != null)
        {
            PageNavigator.Content = new EmployeesPage(services.GetRequiredService<IEmployeeService>());
            rbDoctors.IsChecked = true;
        }
        else
        {
            // services obyekti null bo'lganida qandaydir xatolikni aniqlash va qo'shimcha qo'ng'iroq
            Console.WriteLine("ServiceProvider is null");
        }

    }

    private void RbNightMode_Click(object sender, RoutedEventArgs e)
    {
        if (btnNightMode.IsChecked == true)
        {
            // Apply DarkTheme
            Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary
            {
                Source = new Uri("/Themes/DarkTheme.xaml", UriKind.Relative)
            };

            ToggleSwitch.Margin = new Thickness(0, -5.1, -17, -5);
        }
        else
        {
            // Apply LightTheme
            Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary
            {
                Source = new Uri("/Themes/LightTheme.xaml", UriKind.Relative)
            };

            ToggleSwitch.Margin = new Thickness(0, -5.1, 17, -5);
        }
    }
}
