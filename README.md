# pluto-rover

This project emulate the navigation of a land-rover on the surface of a planet, handling only simple commands such as:
 - Move forwards (F)
 - Move backwards (B)
 - Pivot left (L)
 - Pivot right (R)

The rover is also able to detect obstacles if any encoutered

## Getting startd

Clone the repository

```bash
dotnet restore
dotnet build
```

## Runing tests

```bash
cd Pluto.Rover.Tests
dotnet restore
dotnet build
dotnet test
```


## TODO

 - Implement initial dropzone for rover
 - Refactor switch cases by % of the map size
