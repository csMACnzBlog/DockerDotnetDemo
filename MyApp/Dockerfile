ARG DOCKER_SKIP_TESTS=

FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY MyApp/MyApp.csproj MyApp/
COPY MyApp.Tests/MyApp.Tests.csproj MyApp.Tests/
RUN dotnet restore ./MyApp/MyApp.csproj /p:Configuration=Release
RUN dotnet restore ./MyApp.Tests/MyApp.Tests.csproj /p:Configuration=Release
COPY . .
RUN dotnet build ./MyApp/MyApp.csproj --no-restore -c Release -o /app

FROM build as test
ARG DOCKER_SKIP_TESTS

WORKDIR /src
RUN [ ! -z "$DOCKER_SKIP_TESTS" ] && : || dotnet build ./MyApp.Tests/MyApp.Tests.csproj --no-restore -c Release
RUN [ ! -z "$DOCKER_SKIP_TESTS" ] && : || dotnet test ./MyApp.Tests/MyApp.Tests.csproj --no-restore --no-build -v normal -c Release

FROM build AS publish
WORKDIR /src/MyApp
RUN dotnet publish MyApp.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MyApp.dll"]
