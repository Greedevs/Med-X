using AutoMapper;
using MedX.Data.IRepositories;
using MedX.Domain.Configurations;
using MedX.Domain.Entities;
using MedX.Domain.Enums;
using MedX.Service.DTOs.Treatments;
using MedX.Service.Exceptions;
using MedX.Service.Extensions;
using MedX.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedX.Service.Services;

public class TreatmentService : ITreatmentService
{
    private readonly IRepository<Room> roomRepository;
    private readonly IRepository<Doctor> doctorRepository;
    private readonly IRepository<Patient> patientRepository;
    private readonly IRepository<Treatment> treatmentRepository;
    private readonly IMapper mapper;
    public TreatmentService(IMapper mapper,
        IRepository<Room> roomRepository,
        IRepository<Doctor> doctorRepository,
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
            ?? throw new NotFoundException($"This Doctor not found with id: {dto.DoctorId}");

        var existRoom = await this.roomRepository.GetAsync(r => r.Id == dto.RoomId)
            ?? throw new NotFoundException($"This room not found with id: {dto.RoomId}");

        var mappedTreatment = this.mapper.Map<Treatment>(dto);
        if (existRoom.IsBusy == false && existRoom.Place - 1 >= 0)
        {
            mappedTreatment.Room = existRoom;
            existRoom.Place -= 1;
            if (existPatient.Gender == Gender.Male)
            {
                existRoom.MaleCount = existRoom.MaleCount.HasValue ? existRoom.MaleCount + 1 : 1;
                existRoom.FemaleCount = existRoom.FemaleCount.HasValue ? existRoom.FemaleCount : 0;
            }
            else
            {
                existRoom.MaleCount = existRoom.MaleCount.HasValue ? existRoom.MaleCount : 0;
                existRoom.FemaleCount = existRoom.FemaleCount.HasValue ? existRoom.FemaleCount + 1 : 1;
            }
        }
        else
            throw new CustomException(401, "There is no room in this room");

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
        var existTreatment = await this.treatmentRepository.GetAsync(r => r.Id == id, includes: new[] {"Patient", "Room"})
            ?? throw new NotFoundException($"This Treatment not found with id: {id}");

        if (existTreatment.Patient.Gender == Gender.Male)
            existTreatment.Room.MaleCount -= 1;
        else
            existTreatment.Room.FemaleCount -= 1;

        existTreatment.Room.Place += 1;
        this.roomRepository.Update(existTreatment.Room);
        this.treatmentRepository.Delete(existTreatment);

        await this.treatmentRepository.SaveChanges();
        await this.roomRepository.SaveChanges();

        return true;
    }
    public async Task<TreatmentResultDto> UpdateAsync(TreatmentUpdateDto dto)
    {
        var existTreatment = await this.treatmentRepository.GetAsync(r => r.Id == dto.Id, includes: new[] { "Patient", "Room" })
            ?? throw new NotFoundException($"This Treatment not found with id: {dto.Id}");

        var existPatient = await this.patientRepository.GetAsync(d => d.Id.Equals(dto.PatientId))
            ?? throw new NotFoundException($"This Patient not found with id: {dto.PatientId}");

        var existDoctor = await this.doctorRepository.GetAsync(r => r.Id == dto.DoctorId)
            ?? throw new NotFoundException($"This Doctor not found with id: {dto.DoctorId}");

        var existRoom = await this.roomRepository.GetAsync(r => r.Id == dto.RoomId)
           ?? throw new NotFoundException($"This room not found with id: {dto.RoomId}");

        var older = existTreatment;
        this.mapper.Map(dto, existTreatment);

        if (older.Patient.Gender == Gender.Male && existTreatment.Patient.Gender == Gender.Female)
        {
            existRoom.MaleCount -= 1;
            existRoom.FemaleCount += 1;
        }
        else if (older.Patient.Gender == Gender.Female && existTreatment.Patient.Gender == Gender.Male)
        {
            existRoom.MaleCount += 1;
            existRoom.FemaleCount -= 1;
        }

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
        var existTreatment = await this.treatmentRepository.GetAsync(r => r.Id == id, includes: new[] { "Doctor", "Patient", "Room" })
            ?? throw new NotFoundException($"This Treatment not found with id: {id}");

        return this.mapper.Map<TreatmentResultDto>(existTreatment);
    }

    public async Task<IEnumerable<TreatmentResultDto>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var allTreatments = await this.treatmentRepository.GetAll(includes: new[] { "Doctor", "Patient", "Room" })
            .ToPaginate(@params)
            .ToListAsync();

        if (search is not null)
        {
            allTreatments = allTreatments
                .Where(d => d.Doctor.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Patient.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Doctor.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || d.Room.RoomNumber.Equals(search)
                || d.Room.Quantity.Equals(search)).ToList();
        }

        return this.mapper.Map<IEnumerable<TreatmentResultDto>>(allTreatments);
    }
}