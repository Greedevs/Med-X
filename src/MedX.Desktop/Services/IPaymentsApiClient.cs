using MedX.Service.DTOs.Payments;

namespace MedX.Desktop.Services;

public interface IPaymentsApiClient
{
    [Post("create")]
    Task<PaymentResultDto> AddAsync(PaymentCreationDto dto);

    [Put("update")]
    Task<PaymentResultDto> UpdateAsync(PaymentUpdateDto dto);

    [Delete("delete/{id:long}")]
    Task<bool> DeleteAsync(long id);

    [Get("get/{id:long}")]
    Task<PaymentResultDto> GetAsync([AliasAs("id")] long id);

    [Get("get-all")]
    Task<IEnumerable<PaymentResultDto>> GetAllAsync([Query] PaginationParams @params, [Query] string search);
}