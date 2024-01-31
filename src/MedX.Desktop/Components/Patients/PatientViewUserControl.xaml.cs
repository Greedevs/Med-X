using MedX.ApiService.Models.Patients;
using MedX.Desktop.Helpers;
using MedX.Desktop.Windows.Patients;
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

namespace MedX.Desktop.Components.Patients;

/// <summary>
/// Interaction logic for PatientViewUserControl.xaml
/// </summary>
public partial class PatientViewUserControl : UserControl
{
    public long Id { get; private set; }
    private IPatientService patientService;
    public PatientViewUserControl(IPatientService patientService)
    {
        InitializeComponent();
        this.patientService = patientService;
    }

    private async void btnViewProfile_Click(object sender, RoutedEventArgs e)
    {
        var existPatient = await this.patientService.GetAsync(Id);
        PatientDetailsShowWindow patientDetails = new PatientDetailsShowWindow();
        patientDetails.SetData(existPatient.Data);
        patientDetails.ShowDialog();
    }

    public void SetData(PatientResultDto dto)
    {
        Id = dto.Id;
        lbFirstName.Content = dto.FirstName;
        lbLastName.Content = dto.LastName;
        lbPhone.Content = dto.Phone;
        lbAddress.Content = dto.Address;
        lbGender.Content = dto.Gender;
        lbDateOfBirth.Content = dto.DateOfBirth;
    }

    private async void EditItem_Click(object sender, RoutedEventArgs e)
    {
        var existPatient = await this.patientService.GetAsync(Id);
        if (existPatient != null)
        {
            PatientUpdateWindow updateWindow = new PatientUpdateWindow(patientService, existPatient.Data);
            updateWindow.ShowDialog();

            var employeesPage = HelperMethod.FindParent<PatientsPage>(this);
            if (employeesPage != null)
                await employeesPage.RefreshAsync();
        }
    }

    private async void DeleteItem_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult messageResult = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
        if (messageResult == MessageBoxResult.OK)
        {
            var result = await this.patientService.DeleteAsync(Id);
            if (result is null)
            {
                MessageBox.Show("Something went wrong!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var patientPage = HelperMethod.FindParent<PatientsPage>(this);
                if (patientPage != null)
                    await patientPage.RefreshAsync();
            }
        }
    }

    private void BtnMore(object sender, RoutedEventArgs e)
    {
        contextMenu.IsOpen = true;
    }

    private void Border_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        contextMenu.IsOpen = true;
    }
}
