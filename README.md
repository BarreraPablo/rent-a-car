# Rent A Car

A web application for car rental management 

Live demo: https://barrerapablo.github.io/rent-a-car/

:white_check_mark: Back-end hosted on Azure

:white_check_mark: Front-end hosted on Github Pages

## How to run with Docker

On repository root directory run on bash o powershell
```bash
docker-compose up -d
```
Then navigate to `http://localhost:8000/`

The database will be generated automatically on the api first run.

## How to install
To start-up the backend create a json file with the name `appsettings.Development.json` on the folder RentACar.Api with the values requested on `appsettings.json`

After that, open a powershell or bash on repository root directory and run
```bash
dotnet restore
dotnet run --project RentACar.Api
```
Then the api will be up on `https://localhost:5001/`

You can find the swagger docs on `https://localhost:5001/swagger/index.html`

To start-up the frontend create a .env file on `RentACar.Presentation` with the following variable and api url as value
```bash
REACT_APP_API_URL=https://localhos....
```
And run  ```npm run start``` on RentACar.Presentation folder to start-up the application

The database will be autogenerated on the first run of the API.


## Tech Stack
##### Main Technologies
- .NET 5.0
- React.js
- SQL Server

##### Other technologies and tools
- XUnit
- Entity Framework Core
- Serilog
- Docker
- Swagger
- AutoMapper
- JSON Web Token (JWT)
- Moq
- Axios
- Ant Design
