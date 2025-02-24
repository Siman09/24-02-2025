FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/SchoolManagement.Web.Host/SchoolManagement.Web.Host.csproj", "src/SchoolManagement.Web.Host/"]
COPY ["src/SchoolManagement.Web.Core/SchoolManagement.Web.Core.csproj", "src/SchoolManagement.Web.Core/"]
COPY ["src/SchoolManagement.Application/SchoolManagement.Application.csproj", "src/SchoolManagement.Application/"]
COPY ["src/SchoolManagement.Core/SchoolManagement.Core.csproj", "src/SchoolManagement.Core/"]
COPY ["src/SchoolManagement.EntityFrameworkCore/SchoolManagement.EntityFrameworkCore.csproj", "src/SchoolManagement.EntityFrameworkCore/"]
WORKDIR "/src/src/SchoolManagement.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/SchoolManagement.Web.Host", "src/SchoolManagement.Web.Host"]
COPY ["src/SchoolManagement.Web.Core", "src/SchoolManagement.Web.Core"]
COPY ["src/SchoolManagement.Application", "src/SchoolManagement.Application"]
COPY ["src/SchoolManagement.Core", "src/SchoolManagement.Core"]
COPY ["src/SchoolManagement.EntityFrameworkCore", "src/SchoolManagement.EntityFrameworkCore"]
WORKDIR "/src/src/SchoolManagement.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "SchoolManagement.Web.Host.dll"]
