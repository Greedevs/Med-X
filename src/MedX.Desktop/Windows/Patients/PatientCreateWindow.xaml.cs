using MedX.ApiService.Models.Patients;

namespace MedX.Desktop.Windows.Patients
{
    /// <summary>
    /// Interaction logic for PatientCreateWindow.xaml
    /// </summary>
    public partial class PatientCreateWindow : Window
    {
        private string imagePath;
        private IPatientService patientService;
        public PatientCreateWindow(IPatientService patientService)
        {
            InitializeComponent();
            this.patientService = patientService;
        }

        private void rbMale_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbFemale_Checked(object sender, RoutedEventArgs e)
        {

        }

        private async void BtnCreatePatient_Click(object sender, RoutedEventArgs e)
        {
            var createPatient = new PatientCreationDto()
            {
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Patronymic = tbPatronymic.Text,
                Phone = tbPhone.Text,
                Address = tbAddress.Text,
                DateOfBirth = dpDateOfBirth.DisplayDate,
                Pinfl = tbPinfl.Text,
            };

            if (rbMale.IsChecked == true)
                createPatient.Gender = Gender.Male;
            else
                createPatient.Gender = Gender.Female;

            var result = await patientService.AddAsync(dto: createPatient);

            if (result != null)
            {
                MessageBox.Show("Employee created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to create employee.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
