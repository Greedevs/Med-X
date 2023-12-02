using AutoMapper;
using MedX.Domain.Entities;
using MedX.Domain.Entities.Administrators;
using MedX.Domain.Entities.Appointments;
using MedX.Domain.Entities.Assets;
using MedX.Domain.Entities.MedicalRecords;
using MedX.Domain.Entities.Services;
using MedX.Service.DTOs.Administrators;
using MedX.Service.DTOs.Appointments;
using MedX.Service.DTOs.Assets;
using MedX.Service.DTOs.CashDesks;
using MedX.Service.DTOs.Doctors;
using MedX.Service.DTOs.MedicalRecords;
using MedX.Service.DTOs.Patients;
using MedX.Service.DTOs.Payments;
using MedX.Service.DTOs.Rooms;
using MedX.Service.DTOs.ServiceItems;
using MedX.Service.DTOs.Services;
using MedX.Service.DTOs.Treatments;

namespace MedX.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Room
        CreateMap<Room, RoomUpdateDto>().ReverseMap();
        CreateMap<Room, RoomResultDto>().ReverseMap();
        CreateMap<Room, RoomCreationDto>().ReverseMap();

        //Employee
        CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();
        CreateMap<Employee, EmployeeResultDto>().ReverseMap();
        CreateMap<Employee, EmployeeCreationDto>().ReverseMap();

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

        //Affair
        CreateMap<Affair, AffairCreationDto>().ReverseMap();
        CreateMap<Affair, AffairUpdateDto>().ReverseMap();
        CreateMap<Affair, AffairResultDto>().ReverseMap();

        //AffairItem
        CreateMap<AffairItem, AffairItemCreationDto>().ReverseMap();
        CreateMap<AffairItem, AffairItemUpdateDto>().ReverseMap();
        CreateMap<AffairItem, AffairItemResultDto>().ReverseMap();

        //MedicalRecord
        CreateMap<MedicalRecord, MedicalRecordCreationDto>().ReverseMap();
        CreateMap<MedicalRecord, MedicalRecordUpdateDto>().ReverseMap();
        CreateMap<MedicalRecord, MedicalRecordResultDto>().ReverseMap();

        //CashDesk
        CreateMap<CashDesk, CashDeskCreationDto>().ReverseMap();
        CreateMap<CashDesk, CashDeskUpdateDto>().ReverseMap();
        CreateMap<CashDesk, CashDeskResultDto>().ReverseMap();

        //Asset
        CreateMap<Asset, AssetResultDto>().ReverseMap();
    }
}
