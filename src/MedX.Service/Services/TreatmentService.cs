using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Service.DTOs.Treatments;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class TreatmentService : ITreatmentService
{
    private readonly IRepository<Room> roomRepository;
    private readonly IRepository<Employee> doctorRepository;
    private readonly IRepository<Patient> patientRepository;
    private readonly IRepository<Treatment> treatmentRepository;
    private readonly IMapper mapper;
    public TreatmentService(IMapper mapper,
        IRepository<Room> roomRepository,
        IRepository<Employee> doctorRepository,
        IRepository<Patient> patientRepository,
        IRepository<Treatment> treatmentRepository)
    {
        this.mapper = mapper;
        this.roomRepository = roomRepository;
        this.doctorRepository = doctorRepository;
        this.patientRepository = patientRepository;
        this.treatmentRepository = treatmentRepository;
    }

    public async Task<TreatmentResultDto> AddAsync(TreatmentCreationDto dto)
    {
        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(dto.PatientId))
            ?? throw new NotFoundException($"This Patient not found with id: {dto.PatientId}");

        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == dto.DoctorId)
            ?? throw new NotFoundException($"This Employee not found with id: {dto.DoctorId}");

        var existRoom = await this.roomRepository.GetAsync(r => r.Id == dto.RoomId)
            ?? throw new NotFoundException($"This room not found with id: {dto.RoomId}");

        var mappedTreatment = this.mapper.Map<Treatment>(dto);
        if (existRoom.Busy + 1 <= existRoom.Quantity)
        {
            if (existRoom.Gender != existPatient.Gender && existRoom.Busy != 0)
            {
                throw new CustomException(400, "The gender don't match in this room");
            }
            if (existRoom.Busy == 0)
            {
                existRoom.Gender = existPatient.Gender;
            }
            existRoom.Busy += 1;
            mappedTreatment.Room = existRoom;
        }
        else
            throw new CustomException(401, "There is no available in this room");

        mappedTreatment.Doctor = existDoctor;
        mappedTreatment.Patient = existPatient;

        this.roomRepository.Update(existRoom);
        await this.treatmentRepository.CreateAsync(mappedTreatment);
        await this.treatmentRepository.SaveChanges();
        await this.roomRepository.SaveChanges();

        return this.mapper.Map<TreatmentResultDto>(mappedTreatment);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existTreatment = await this.treatmentRepository.GetAsync(r => r.Id == id, includes: new[] { "Patient", "Room" })
            ?? throw new NotFoundException($"This Treatment not found with id: {id}");

        existTreatment.Room.Busy -= 1;
        this.roomRepository.Update(existTreatment.Room);
        this.treatmentRepository.Delete(existTreatment);

        await this.treatmentRepository.SaveChanges();
        await this.roomRepository.SaveChanges();

        return true;
    }
    public async Task<TreatmentResultDto> UpdateAsync(TreatmentUpdateDto dto)
    {
        var existTreatment = await this.treatmentRepository.GetAsync(r => r.Id == dto.Id, includes: new[] { "Employee", "Patient", "Room" })
            ?? throw new NotFoundException($"This Treatment not found with id: {dto.Id}");

        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(dto.PatientId))
            ?? throw new NotFoundException($"This Patient not found with id: {dto.PatientId}");

        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == dto.DoctorId)
            ?? throw new NotFoundException($"This Employee not found with id: {dto.DoctorId}");

        var existRoom = await this.roomRepository.GetAsync(r => r.Id == dto.RoomId)
           ?? throw new NotFoundException($"This room not found with id: {dto.RoomId}");

        var older = existTreatment;
        this.mapper.Map(dto, existTreatment);

        existTreatment.Room = existRoom;
        existTreatment.Doctor = existDoctor;
        existTreatment.Patient = existPatient;

        this.roomRepository.Update(existTreatment.Room);
        this.treatmentRepository.Update(existTreatment);
        await this.treatmentRepository.SaveChanges();
        await this.roomRepository.SaveChanges();

        return this.mapper.Map<TreatmentResultDto>(existTreatment);
    }

    public async Task<TreatmentResultDto> GetAsync(long id)
    {
        var existTreatment = await this.treatmentRepository.GetAsync(r => r.Id == id, includes: new[] { "Employee", "Patient", "Room" })
            ?? throw new NotFoundException($"This Treatment not found with id: {id}");

        return this.mapper.Map<TreatmentResultDto>(existTreatment);
    }

    public async Task<IEnumerable<TreatmentResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allTreatments = await this.treatmentRepository.GetAll(includes: new[] { "Employee", "Patient", "Room" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            search = search.Replace("+", "%2B");
            allTreatments = allTreatments
                .Where(d => d.Doctor.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Doctor.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Doctor.Phone.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Room.Number.Equals(search)
                || d.Room.Quantity.Equals(search)).ToList();
        }

        return this.mapper.Map<IEnumerable<TreatmentResultDto>>(allTreatments);
    }

    public async Task<IEnumerable<TreatmentResultDto>> GetAllByPatientIdAsync(long patientId)
    {
        var existPatient = await this.patientRepository.GetAsync(p => p.Id == patientId)
            ?? throw new NotFoundException($"This patient is not found with id: {patientId}");

        var patients = await this.treatmentRepository.GetAll(p => p.PatientId == patientId).ToListAsync();

        return this.mapper.Map<IEnumerable<TreatmentResultDto>>(patients);
    }

    public async Task<IEnumerable<TreatmentResultDto>> GetAllByDoctorIdAsync(long doctorId, PaginationParams @params, string search = null)
    {
        var existDoctor = await this.doctorRepository.GetAsync(p => p.Id == doctorId)
            ?? throw new NotFoundException($"This patient is not found with id: {doctorId}");

        var doctors = await this.treatmentRepository.GetAll(p => p.DoctorId == doctorId)
            .ToPaginate(@params)
            .ToListAsync();
        if (search is not null)
        {
            doctors = doctors
                .Where(d => d.Patient.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Room.Number.Equals(search)
                || d.Room.Quantity.Equals(search)).ToList();
        }
        return this.mapper.Map<IEnumerable<TreatmentResultDto>>(doctors);
    }

    public async Task<IEnumerable<TreatmentResultDto>> GetAllByRoomIdAsync(long roomId)
    {
        var existRoom = await this.roomRepository.GetAsync(r => r.Id == roomId)
            ?? throw new NotFoundException($"This room is not found with id = {roomId}");

        var rooms = await this.treatmentRepository.GetAll(r => r.RoomId == roomId).ToListAsync();

        return this.mapper.Map<IEnumerable<TreatmentResultDto>>(rooms);
    }
}