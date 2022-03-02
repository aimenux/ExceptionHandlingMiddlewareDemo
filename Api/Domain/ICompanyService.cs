namespace Api.Domain;

public interface ICompanyService
{
    Task<Company> GetCompanyAsync(string registrationNumber, CancellationToken cancellationToken = default);
}