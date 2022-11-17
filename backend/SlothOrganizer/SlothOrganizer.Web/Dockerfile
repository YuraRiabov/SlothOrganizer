#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SlothOrganizer.Contracts/SlothOrganizer.Contracts.csproj", "SlothOrganizer.Contracts/"]
COPY ["SlothOrganizer.Domain/SlothOrganizer.Domain.csproj", "SlothOrganizer.Domain/"]
COPY ["SlothOrganizer.Persistence/SlothOrganizer.Persistence.csproj", "SlothOrganizer.Persistence/"]
COPY ["SlothOrganizer.Presentation/SlothOrganizer.Presentation.csproj", "SlothOrganizer.Presentation/"]
COPY ["SlothOrganizer.Services/SlothOrganizer.Services.csproj", "SlothOrganizer.Services/"]
COPY ["SlothOrganizer.Services.Abstractions/SlothOrganizer.Services.Abstractions.csproj", "SlothOrganizer.Services.Abstractions/"]
COPY ["SlothOrganizer.Web/SlothOrganizer.Web.csproj", "SlothOrganizer.Web/"]
RUN dotnet restore "SlothOrganizer.Web/SlothOrganizer.Web.csproj"
COPY . .
WORKDIR "/src/SlothOrganizer.Web"
RUN dotnet build "SlothOrganizer.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SlothOrganizer.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SlothOrganizer.Web.dll"]