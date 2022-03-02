using Api.Domain;

namespace Api.Infrastructure;

public interface IProxy
{
    Task<Company> GetCompanyAsync(string registrationNumber, CancellationToken cancellationToken = default);
}