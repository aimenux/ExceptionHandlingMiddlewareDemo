namespace Api.Domain;

public class DomainException : Exception
{
    protected DomainException(string message) : base(message)
    {
    }

    public static DomainException InvalidCompanyAddress()
    {
        throw new DomainException("Invalid company address");
    }

    public static DomainException InvalidCompanyStatus()
    {
        throw new DomainException("Invalid company status");
    }
}