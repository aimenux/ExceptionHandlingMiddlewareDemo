[![.NET](https://github.com/aimenux/ExceptionHandlingMiddlewareDemo/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/aimenux/ExceptionHandlingMiddlewareDemo/actions/workflows/ci.yml)

# ExceptionHandlingMiddlewareDemo
```
Using exception middleware to global handle errors
```

In this demo, i m using an exception middleware in order to handle errors in the whole application.

Exceptions are thrown by :
>
- Infrastructure : see `Proxy` (which simulate an external instable web service)
>
- Domain : see `CompanyService` (which simulate some dummy eligibility rules)

Exceptions are catched by the exception middleware and formatted using [problem details specification](https://datatracker.ietf.org/doc/html/rfc7807)

**`Tools`** : net 8.0, xunit