using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MedX.Service.DTOs.Appointments;

public class AppointmentCreationDto
{
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
    public long PaymentId { get; set; }
    public string Disease { get; set; }
}