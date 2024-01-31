using MedX.ApiService.Models.Commons;
using MedX.ApiService.Models.Patients;
using System.Windows.Media.Imaging;

namespace MedX.Desktop.Windows.Patients
{
    /// <summary>
    /// Interaction logic for PatientUpdateWindow.xaml
    /// </summary>
    public partial class PatientUpdateWindow : Window
    {
        private IPatientService patientService;
        private PatientResultDto dto;
        public PatientUpdateWindow(IPatientService patientService,
            PatientResultDto dto)
        {
            InitializeComponent();
            this.patientService = patientService;
            this.dto = dto;
            tbFirstName.Text = dto.FirstName;
            tbLastName.Text = dto.LastName;
            tbPatronymic.Text = dto.Patronymic;
            tbPhone.Text = dto.Phone;
            tbAddress.Text = dto.Address;
            dpDateOfBirth.Text = dto.DateOfBirth.ToString();
            tbPinfl.Text = dto.Pinfl;
            if (dto.Gender == Gender.Male)
                rbMale.IsChecked = true;
            else
                rbFemale.IsChecked = true;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void BtnUpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            var updatePatient = new PatientUpdateDto()
            {
                Id = dto.Id,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Patronymic = tbPatronymic.Text,
                Phone = tbPhone.Text,
                Address = tbAddress.Text,
                DateOfBirth = dpDateOfBirth.DisplayDate,
                Pinfl = tbPinfl.Text,
            };

            if(rbMale.IsChecked == true)
                updatePatient.Gender = Gender.Male;
            else
                updatePatient.Gender = Gender.Female;

            var result = await patientService.UpdateAsync(dto: updatePatient);

            if (result != null)
            {
                MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to update employee.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.Close();
        }

        private void rbMale_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbFemale_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
