using MedX.ApiService.Models.Employees;

namespace MedX.ApiService.Services;

public class EmployeeService(HttpClient client) : IEmployeeService
{
    public async Task<Response<EmployeeResultDto>> AddAsync(EmployeeCreationDto dto)
    {
        using var multipartFormContent = ConvertHelper.ConvertToMultipartFormContent(dto);
        using var response = await client.PostAsync("create", multipartFormContent);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<Response<EmployeeResultDto>>())!;
    }

    public async Task<Response<EmployeeResultDto>> UpdateAsync(EmployeeUpdateDto dto)
    {
        using var multipartFormContent = ConvertHelper.ConvertToMultipartFormContent(dto);
        using var response = await client.PutAsync("update", multipartFormContent);
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<Response<EmployeeResultDto>>())!;
    }

    public async Task<Response<bool>> DeleteAsync(long id)
    {
        using var response = await client.DeleteAsync($"delete/{id}");
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<Response<bool>>())!;
    }

    public async Task<Response<EmployeeResultDto>> GetAsync(long id)
    {
        using var response = await client.GetAsync($"get/{id}");
        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<Response<EmployeeResultDto>>())!;
    }

    public async Task<Response<IEnumerable<EmployeeResultDto>>> GetAllAsync(PaginationParams @params, string search = null!)
    {
        var queryParams = new Dictionary<string, string>
        {
            { nameof(@params.PageIndex), @params.PageIndex.ToString() },
            { nameof(@params.PageSize), @params.PageSize.ToString() },
            { nameof(search), search }
        };

        var queryString = string.Join("&", queryParams.Where(p => !string.IsNullOrEmpty(p.Value)).Select(p => $"{p.Key}={p.Value}"));
        using var response = await client.GetAsync($"get-all?{queryString}");

        if (!response.IsSuccessStatusCode)
            return default!;

        return (await response.Content.ReadFromJsonAsync<Response<IEnumerable<EmployeeResultDto>>>())!;
    }
}