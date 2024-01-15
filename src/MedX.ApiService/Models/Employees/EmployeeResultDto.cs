using MedX.Domain.Enums;
using System.ComponentModel;

namespace MedX.ApiService.Models.Employees;

public class EmployeeResultDto
{
    public long Id { get; set; }
    [DisplayName("Фамилия")]
    public string FirstName { get; set; } = string.Empty;
    [DisplayName("Имя")]
    public string LastName { get; set; } = string.Empty;
    [DisplayName("Отчество")]
    public string Patronymic { get; set; } = string.Empty;
    [DisplayName("Профессия")]
    public string Professional { get; set; } = string.Empty;
    [DisplayName("Телефон")]
    public string Phone { get; set; } = string.Empty;
    [DisplayName("Email")]
    public string Email { get; set; } = string.Empty;
    [DisplayName("Баланс")]
    public decimal Balance { get; set; }
    [DisplayName("Номер счёта")]
    public string AccountNumber { get; set; } = string.Empty;
    [DisplayName("Изображение")]
    public AssetResultDto Image { get; set; } = default!;
    [DisplayName("Зарплата")]
    public decimal? Salary { get; set; }
    [DisplayName("Процент")]
    public int? Percentage { get; set; }
    [DisplayName("Степень")]
    public Degree Degree { get; set; }
}