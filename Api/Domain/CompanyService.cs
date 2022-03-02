using Api.Infrastructure;

namespace Api.Domain
{
    public class CompanyService : ICompanyService
    {
        private readonly IProxy _proxy;

        public CompanyService(IProxy proxy)
        {
            _proxy = proxy;
        }

        public async Task<Company> GetCompanyAsync(string registrationNumber, CancellationToken cancellationToken = default)
        {
            var company = await _proxy.GetCompanyAsync(registrationNumber, cancellationToken);
            
            if (company.CompanyStatus != CompanyStatus.Active)
            {
                throw DomainException.InvalidCompanyStatus();
            }

            if (string.IsNullOrWhiteSpace(company.Address))
            {
                throw DomainException.InvalidCompanyAddress();
            }

            return company;
        }
    }
}
