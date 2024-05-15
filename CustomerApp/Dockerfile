FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 3000
EXPOSE 3001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src

RUN curl -o /usr/local/share/ca-certificates/verisign.crt -SsL https://crt.sh/?d=1039083 && update-ca-certificates

COPY ["CustomerApp/CustomerApp.csproj", "CustomerApp/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infra/Infra.csproj", "Infra/"]

WORKDIR /src/Domain
RUN dotnet restore
RUN dotnet pack -o /src/packages/
RUN dotnet nuget add source /src/packages -n local

WORKDIR /src/Infra
RUN dotnet restore
RUN dotnet pack -o /src/packages/

WORKDIR /src/CustomerApp
RUN dotnet restore

RUN dotnet add package Domain -s /src/packages
RUN dotnet add package Infra -s /src/packages

WORKDIR /src
COPY . .

WORKDIR "/src/CustomerApp"
RUN rm -fr obj bin
RUN dotnet clean
RUN dotnet build 

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "CustomerApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "CustomerApp.dll"]