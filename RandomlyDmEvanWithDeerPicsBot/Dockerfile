FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RandomlyDmEvanWithDeerPicsBot.csproj", "./"]
RUN dotnet restore "RandomlyDmEvanWithDeerPicsBot.csproj"
COPY . .
RUN dotnet build "RandomlyDmEvanWithDeerPicsBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RandomlyDmEvanWithDeerPicsBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["/app/RandomlyDmEvanWithDeerPicsBot"]
