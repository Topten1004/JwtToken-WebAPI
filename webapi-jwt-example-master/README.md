.NET Core Web API (JWT Auth) Example
========
## Get started
### Build & Run
```sh
git clone https://github.com/GLEBR1K/webapi-jwt-example jwt
cd jwt\WebAPI_Example
dotnet run
```
### Use
Try to register with `POST /api/register` and get JWT token:
```sh
> curl http://localhost:8080/api/register -H 'Content-type: application/json' -d '{ Email: "$EMAIL", Password: "$PASSWORD" }'

{"succeeded":true,"token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlbWFpbEBleGFtcGxlLmxvY2FsIiwianRpIjoiNGE0NzQzYWMtZDU5Yi00NjI3LThhMmEtN2JkZjAxMTQyODk4IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJkYjVmNWEwMC0xYzNiLTQ1MDYtOTE3YS02NWJlODJmYTg2MmQiLCJleHAiOjE1Mjk2OTgyNjIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTkxMjQvIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1OTEyNC8ifQ.ZZreutgH3FgKy173cwiBj9jRVZPD9_gdoYLwgpDkST4"}
```

Try to login with `POST /api/login` and get JWT token.
```sh
> curl http://localhost:8080/api/login -H 'Content-type: application/json' -d '{ Email: "$EMAIL", Password: "$PASSWORD" }'

{"succeeded":true,"token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlbWFpbEBleGFtcGxlLmxvY2FsIiwianRpIjoiNGE0NzQzYWMtZDU5Yi00NjI3LThhMmEtN2JkZjAxMTQyODk4IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJkYjVmNWEwMC0xYzNiLTQ1MDYtOTE3YS02NWJlODJmYTg2MmQiLCJleHAiOjE1Mjk2OTgyNjIsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTkxMjQvIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1OTEyNC8ifQ.ZZreutgH3FgKy173cwiBj9jRVZPD9_gdoYLwgpDkST4"}
```

Get access to public page with `GET /api/public`, which returns random value.
```sh
> curl http://localhost:8080/api/public

{"value":737}
```

Get access to protected page with `GET /api/protected`, which needs JWT token.
```sh
> curl http://localhost:8080/api/protected -H 'Authorization: Bearer $TOKEN'

{"value":769}
```
## Tests
You can test app using command line (like `curl`) or other apps (like `Postman`).

Import collection (from `Tests\PostmanCollection.json`) and global variables (from `Tests\PostmanVariables.json`) into Postman.

Edit `Email`, `Password` and `Token` in global variables panel.

Now you can send example requests from Postman.
## Technologies
* .NET Core 2.1.200 (dotnet)
* SQLite 3
