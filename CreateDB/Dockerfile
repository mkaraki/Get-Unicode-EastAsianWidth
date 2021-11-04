FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

COPY . /app/

RUN dotnet publish /app/CreateDB/CreateDB.csproj -c Release -o /article

FROM mcr.microsoft.com/dotnet/runtime:5.0

COPY --from=build /article /app

WORKDIR /app

ENTRYPOINT [ "dotnet", "CreateDB.dll" ]
