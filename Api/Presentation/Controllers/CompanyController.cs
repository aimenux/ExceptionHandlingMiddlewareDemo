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
    [Consumes("application/json")]
    [Produces("application/json", "application/problem+json")]
    [ProducesResponseType(typeof(CompanyRequestDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status501NotImplemented)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<CompanyResponseDto> GetCompanyAsync([FromQuery] CompanyRequestDto request, CancellationToken cancellationToken = default)
    {
        var company = await _companyService.GetCompanyAsync(request.RegistrationNumber, cancellationToken);
        var companyDto = new CompanyResponseDto(company);
        return companyDto;
    }
}