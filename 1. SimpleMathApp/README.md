# SimpleMathApp

SimpleMathApp is a minimal ASP.NET Core app that performs basic math operations through a query-string endpoint.

## Usage

Send a request to `/` with these query parameters:

- `operation`: `add`, `subtract`, `multiply`, or `divide`
- `firstNumber`: first integer
- `secondNumber`: second integer

Example:

```text
http://localhost:5156/?operation=add&firstNumber=2&secondNumber=3
```

Response:

```text
5
```

If the input is invalid, the app returns `400 Bad Request`.