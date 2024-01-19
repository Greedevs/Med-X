namespace MedX.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IEmployeeService service;

    public MainWindow(IEmployeeService service)
    {
        InitializeComponent();
        this.service = service;
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
        PageNavigator.Content = new EmployeesPage(service);
    }

    private void RbPatients_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new PatientsPage();
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
        PageNavigator.Content = new EmployeesPage(service);
    }

    private void RbInformation_Click(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new InformationPage();
    }

    private void WindowLoaded(object sender, RoutedEventArgs e)
    {
        PageNavigator.Content = new EmployeesPage(service);
        rbDoctors.IsChecked = true;
    }
}
