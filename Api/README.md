# ExceptionHandlingMiddlewareDemo
```
Using exception middleware to global handle errors
```

In this demo, i m using an exception middleware in order to global handle errors in the whole application.

Exceptions are thrown by :
>
- Infrastructure : see `Proxy` (which simulate an external instable web service)
>
- Domain : see `CompanyService` (which makes some dummy validation logic)

Exceptions are catched by the exception middleware and formatted using [problem details specification](https://datatracker.ietf.org/doc/html/rfc7807)

**`Tools`** : vs22, net 6.0