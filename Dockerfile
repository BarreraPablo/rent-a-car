FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app 
#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY RentACar.Api/*.csproj ./RentACar.Api/
COPY RentACar.Core/*.csproj ./RentACar.Core/
COPY RentACar.Infrastructure/*.csproj ./RentACar.Infrastructure/
COPY RentACar.UnitTest/*.csproj ./RentACar.UnitTest/
#
RUN dotnet restore 
#
# copy everything else and build app
COPY RentACar.Api/. ./RentACar.Api/
COPY RentACar.Core/. ./RentACar.Core/
COPY RentACar.Infrastructure/. ./RentACar.Infrastructure/ 
#
WORKDIR /app/RentACar.Api
RUN dotnet publish -c Release -o out 
#
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS runtime
WORKDIR /app 
ENV ASPNETCORE_URLS http://+:5000;https://+:5001
COPY --from=build /app/RentACar.Api/out ./
ENTRYPOINT ["dotnet", "RentACar.Api.dll"]

