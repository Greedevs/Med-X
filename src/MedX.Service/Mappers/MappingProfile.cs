using AutoMapper;
using MedX.Domain.Enitities;
using MedX.Domain.Entities.Administrators;
using MedX.Service.DTOs.Administrators;
using MedX.Service.DTOs.Appointments;
using MedX.Service.DTOs.Doctors;
using MedX.Service.DTOs.Patients;
using MedX.Service.DTOs.Payments;
using MedX.Service.DTOs.Rooms;
using MedX.Service.DTOs.Transactions;
using MedX.Service.DTOs.Treatments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedX.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Room
        CreateMap<Room, RoomUpdateDto>().ReverseMap();
        CreateMap<Room, RoomResultDto>().ReverseMap();
        CreateMap<Room, RoomCreationDto>().ReverseMap();
        
        //Doctor
        CreateMap<Doctor, DoctorUpdateDto>().ReverseMap();
        CreateMap<Doctor, DoctorResultDto>().ReverseMap();
        CreateMap<Doctor, DoctorCreationDto>().ReverseMap();

        //Patient
        CreateMap<Patient, PatientUpdateDto>().ReverseMap();
        CreateMap<Patient, PatientResultDto>().ReverseMap();
        CreateMap<Patient, PatientCreationDto>().ReverseMap();
        
        //Administrator
        CreateMap<Administrator, AdminUpdateDto>().ReverseMap();
        CreateMap<Administrator, AdminResultDto>().ReverseMap();
        CreateMap<Administrator, AdminCreationDto>().ReverseMap();

        //Payment
        CreateMap<Payment, PaymentUpdateDto>().ReverseMap();
        CreateMap<Payment, PaymentResultDto>().ReverseMap();
        CreateMap<Payment, PaymentCreationDto>().ReverseMap();

        //Treatment
        CreateMap<Treatment, TreatmentCreationDto>().ReverseMap();
        CreateMap<Treatment, TreatmentUpdateDto>().ReverseMap();
        CreateMap<Treatment, TreatmentResultDto>().ReverseMap();

        //Appointment
        CreateMap<Appointment, AppointmentCreationDto>().ReverseMap();
        CreateMap<Appointment, AppointmentUpdateDto>().ReverseMap();
        CreateMap<Appointment, AppointmentResultDto>().ReverseMap();
        //Transaction
        CreateMap<Transaction, TransactionUpdateDto>().ReverseMap();
        CreateMap<Transaction, TransactionResultDto>().ReverseMap();
        CreateMap<Transaction, TransactionCreationDto>().ReverseMap();
    }
}
