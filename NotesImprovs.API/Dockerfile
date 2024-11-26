FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NotesImprovs.API/NotesImprovs.API.csproj", "NotesImprovs.API/"]
COPY ["NotesImprovs.BLL/NotesImprovs.BLL.csproj", "NotesImprovs.BLL/"]
COPY ["NotesImprovs.DAL/NotesImprovs.DAL.csproj", "NotesImprovs.DAL/"]
COPY ["NotesImprovs.Models/NotesImprovs.Models.csproj", "NotesImprovs.Models/"]
RUN dotnet restore "NotesImprovs.API/NotesImprovs.API.csproj"
COPY . .
WORKDIR "/src/NotesImprovs.API"
RUN dotnet build "NotesImprovs.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NotesImprovs.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotesImprovs.API.dll"]
