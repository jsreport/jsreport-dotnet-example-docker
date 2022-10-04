FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt update && apt install -y gconf-service libgbm-dev libasound2 libatk1.0-0 libc6 libcairo2 libcups2 libdbus-1-3 libexpat1 libfontconfig1 libgcc1 libgconf-2-4 libgdk-pixbuf2.0-0 libglib2.0-0 libgtk-3-0 libnspr4 libpango-1.0-0 libpangocairo-1.0-0 libstdc++6 libx11-6 libx11-xcb1 libxcb1 libxcomposite1 libxcursor1 libxdamage1 libxext6 libxfixes3 libxi6 libxrandr2 libxrender1 libxss1 libxtst6 ca-certificates fonts-liberation libappindicator1 libnss3 lsb-release xdg-utils
ENV chrome_launchOptions_args --no-sandbox,--disable-dev-shm-usage
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "WebApp/WebApp.csproj" "/src/WebApp/WebApp.csproj"
RUN dotnet restore "WebApp/WebApp.csproj"
COPY . .
WORKDIR "/src/WebApp"
RUN dotnet build "WebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApp.dll"]