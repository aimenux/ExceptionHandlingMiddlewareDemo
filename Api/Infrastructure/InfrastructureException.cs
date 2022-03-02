namespace Api.Infrastructure;

public class InfrastructureException : Exception
{
    protected InfrastructureException(string message) : base(message)
    {
    }

    public static InfrastructureException PartnerWebServiceIsDown()
    {
        throw new InfrastructureException("Partner WebService is down");
    }

    public static InfrastructureException PartnerWebServiceIsTakingTooLongToRespond()
    {
        throw new InfrastructureException("Partner WebService is taking too long to respond");
    }
}