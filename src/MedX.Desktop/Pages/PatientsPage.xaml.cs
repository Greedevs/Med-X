using MedX.Desktop.Components.Employees;
using MedX.Desktop.Components.Patients;
using MedX.Desktop.Windows.Patients;

namespace MedX.Desktop.Pages;

/// <summary>
/// Interaction logic for PatientsPage.xaml
/// </summary>
public partial class PatientsPage : Page
{
    private readonly IPatientService patientService;
    public PatientsPage(IPatientService patientService)
    {
        InitializeComponent();
        this.patientService = patientService;
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await RefreshAsync();
    }

    public async Task RefreshAsync(string search = null)
    {
        wrpPatient.Children.Clear();
        PaginationParams paginationParams = new PaginationParams()
        {
            PageIndex = 1,
            PageSize = 30
        };

        var patients = await patientService.GetAllAsync(paginationParams, search);

        wrpPatient.Children.Clear();


        foreach (var patient in patients.Data)
        {
            var employeeCardUserControl = new PatientViewUserControl(patientService);
            employeeCardUserControl.SetData(dto: patient);
            wrpPatient.Children.Add(element: employeeCardUserControl);
        }
    }

    private async void btnCreate_Click(object sender, RoutedEventArgs e)
    {
        PatientCreateWindow patientCreateWindow = new PatientCreateWindow(patientService);
        patientCreateWindow.ShowDialog();
        await RefreshAsync();
    }

    private async void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        await RefreshAsync(tbSearch.Text);
    }
}
