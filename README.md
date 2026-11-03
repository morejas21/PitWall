# PitWall

Android aplikacija za igru prognoza Formule 1 s prijateljima. Korisnici se okupljaju u privatnim ligama i prije svakog trkaćeg vikenda prognoziraju pole position, prva tri mjesta i najbrži krug. Backend nakon utrke preuzima rezultate i boduje prognoze.

Projekt kolegija na FOI-ju.

## Backend

ASP.NET Core Web API na .NET 10, u mapi `backend/`:

- `PitWall.Api`: endpointi, DI, OpenAPI
- `PitWall.Business`: poslovna pravila
- `PitWall.Data`: baza i vanjski izvori podataka
- `PitWall.Tests`: xUnit testovi

Preduvjet je .NET SDK 10 (na Fedori `sudo dnf install dotnet-sdk-10.0`).

Naredbe se pokreću iz mape `backend/`, jer `global.json` u njoj određuje SDK i test runner:

```
cd backend
dotnet build PitWall.sln
dotnet test --solution PitWall.sln
dotnet format PitWall.sln --verify-no-changes
dotnet run --project PitWall.Api
```

API sluša na `http://localhost:5000`. U razvojnom okruženju Swagger UI je na `http://localhost:5000/swagger`, a OpenAPI dokument na `http://localhost:5000/openapi/v1.json`.
