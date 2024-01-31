using MedX.ApiService.Models.Patients;
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
using System.Windows.Shapes;

namespace MedX.Desktop.Windows.Patients;

/// <summary>
/// Interaction logic for PatientDetailsShowWindow.xaml
/// </summary>
public partial class PatientDetailsShowWindow : Window
{
    public long Id { get; private set; }
    public PatientDetailsShowWindow()
    {
        InitializeComponent();
    }

    public void SetData(PatientResultDto dto)
    {
        Id = dto.Id;
        tbFirstName.Text = dto.FirstName;
        tbLastName.Text = dto.LastName;
        tbPhone.Text = dto.Phone;
        tbPinfl.Text = dto.Pinfl;
        tbAddress.Text = dto.Address;
        tbPatronymic.Text = dto.Patronymic; 
        tbAccountNumber.Text = dto.AccountNumber;
        tbBalance.Text = dto.Balance.ToString();
        tbCreatedAt.Text = dto.CreatedAt.ToString("dd-MM-yyyy");
        tbDateOfBirth.Text = dto.DateOfBirth.ToString("dd-MM-yyyy");
        if(dto.Gender == Gender.Male)
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
}
