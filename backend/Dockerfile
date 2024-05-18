FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY *.sln .
COPY DoDo.Models/*.csproj DoDo.Models/
COPY DoDo.Business/*.csproj DoDo.Business/
COPY DoDo.Data/*.csproj DoDo.Data/
COPY DoDo.Api/*.csproj DoDo.Api/
RUN dotnet restore DoDo.Api/DoDo.Api.csproj

COPY . .
RUN dotnet publish DoDoAPI.sln -c Release -o API/out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/API/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "DoDo.Api.dll"]