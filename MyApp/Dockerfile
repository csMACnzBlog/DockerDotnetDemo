FROM microsoft/dotnet:2.1-sdk
WORKDIR /src
COPY . .
RUN dotnet publish MyApp/MyApp.csproj -c Release -o /app
WORKDIR /app
COPY /app .
ENTRYPOINT ["dotnet", "MyApp.dll"]
