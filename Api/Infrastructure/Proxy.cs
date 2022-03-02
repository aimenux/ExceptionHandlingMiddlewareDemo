using Api.Domain;

namespace Api.Infrastructure
{
    public class Proxy : IProxy
    {
        public Task<Company> GetCompanyAsync(string registrationNumber, CancellationToken cancellationToken = default)
        {
            var nextValue = Randomize.Next();

            return nextValue switch
            {
                < 200 => Task.FromResult(new Company
                {
                    Name = Randomize.RandomString(10),
                    RegistrationNumber = registrationNumber,
                    Address = Randomize.RandomString(20),
                    CompanyStatus = CompanyStatus.Active
                }),
                < 400 => Task.FromResult(new Company
                {
                    Name = Randomize.RandomString(10),
                    RegistrationNumber = registrationNumber,
                    Address = Randomize.RandomString(20),
                    CompanyStatus = CompanyStatus.Delisted
                }),
                < 600 => Task.FromResult(new Company
                {
                    Name = Randomize.RandomString(10),
                    RegistrationNumber = registrationNumber,
                    CompanyStatus = CompanyStatus.Active
                }),
                < 800 => throw InfrastructureException.PartnerWebServiceIsDown(),
                _ => throw InfrastructureException.PartnerWebServiceIsTakingTooLongToRespond()
            };
        }

        public class Randomize
        {
            private static readonly Random Random = new(Guid.NewGuid().GetHashCode());

            public static int Next() => Random.Next(1, 1000);
            
            public static string RandomString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[Random.Next(s.Length)]).ToArray());
            }
        }
    }
}
