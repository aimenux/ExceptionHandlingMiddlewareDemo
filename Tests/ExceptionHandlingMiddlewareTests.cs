using System.IO;
using System.Threading.Tasks;
using Api.Domain;
using Api.Infrastructure;
using Api.Presentation.Middlewares;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Tests;

public class ExceptionHandlingMiddlewareTests
{
    [Theory]
    [ClassData(typeof(TestCases))]
    public async Task Should_Return_Valid_StatusCode(RequestDelegate next, int expectedStatusCode)
    {
        // arrange
        using var stream = new MemoryStream();
        var logger = NullLogger<ExceptionHandlingMiddleware>.Instance;
        var context = new DefaultHttpContext
        {
            Response =
            {
                Body = stream
            }
        };
        var middleware = new ExceptionHandlingMiddleware(next, logger);

        // act
        await middleware.Invoke(context);

        // assert
        context.Response.StatusCode.Should().Be(expectedStatusCode);
    }

    private class TestCases : TheoryData<RequestDelegate, int>
    {
        public TestCases()
        {
            Add(_ => Task.CompletedTask, 200);
            Add(_ => throw DomainException.InvalidCompanyStatus(), 501);
            Add(_ => throw DomainException.InvalidCompanyAddress(), 501);
            Add(_ => throw InfrastructureException.PartnerWebServiceIsDown(), 503);
            Add(_ => throw InfrastructureException.PartnerWebServiceIsTakingTooLongToRespond(), 503);
            Add(_ => throw InfrastructureException.PartnerWebServiceReceivingTooManyRequests(), 503);
        }
    }
}