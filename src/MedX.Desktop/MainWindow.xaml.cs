using MedX.Desktop.Pages;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.XPath;

namespace MedX.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStyle = WindowStyle.None;
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
                this.WindowState = WindowState.Normal;
            else this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => WindowStyle = WindowStyle.None));
        }

        private void rbDashboard_Click(object sender, RoutedEventArgs e)
        {
            DashboardPage dashboardPage = new();
            PageNavigator.Content = dashboardPage;
        }

        private void rbDoctors_Click(object sender, RoutedEventArgs e)
        {
            EmployeesPage employeesPage = new();
            PageNavigator.Content = employeesPage;
        }

        private void rbPatients_Click(object sender, RoutedEventArgs e)
        {
            PatientsPage patientsPage = new(); // TODO: implement
            PageNavigator.Content = patientsPage;
        }

        private void rbAffairs_Click(object sender, RoutedEventArgs e)
        {
            ReportsPage reportsPage = new();
            PageNavigator.Content = reportsPage;
        }

        private void rbCashDesk_Click(object sender, RoutedEventArgs e)
        {
            CashDesksPage cashPage = new();
            PageNavigator.Content = cashPage;
        }

        private void rbRooms_Click(object sender, RoutedEventArgs e)
        {
            RoomsPage roomsPage = new();
            PageNavigator.Content = roomsPage;
        }

        private void rbReports_Click(object sender, RoutedEventArgs e)
        {
            ReportsPage reportsPage = new();
            PageNavigator.Content = reportsPage;
        }

        private void rbAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutPage aboutPage = new();
            PageNavigator.Content = aboutPage;
        }

        private void rbEmployees_Click(object sender, RoutedEventArgs e)
        {
            EmployeesPage employeesPage = new();
            PageNavigator.Content = employeesPage;
        }

        private void rbInformation_Click(object sender, RoutedEventArgs e)
        {
            InformationPage informationPage = new();
            PageNavigator.Content = informationPage;
        }
    }
}
