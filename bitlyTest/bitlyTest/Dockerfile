FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["bitlyTest/bitlyTest.csproj", "bitlyTest/"]
RUN dotnet restore "bitlyTest/bitlyTest.csproj"
COPY . .
WORKDIR "/src/bitlyTest"
RUN dotnet build "bitlyTest.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "bitlyTest.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "bitlyTest.dll"]