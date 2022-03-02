using Api.Domain;
using Api.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet(Name = "GetCompany")]
    public async Task<CompanyResponseDto> GetCompanyAsync([FromQuery] CompanyRequestDto request, CancellationToken cancellationToken = default)
    {
        var company = await _companyService.GetCompanyAsync(request.RegistrationNumber, cancellationToken);
        var companyDto = new CompanyResponseDto(company);
        return companyDto;
    }
}