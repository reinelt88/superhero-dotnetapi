# superhero-dotnetapi
Dotnet 6 example API using MySql 

## Prerquisites
1. Visual Studio Code
2. .NET 6.0 SDK

## How to run
`dotnet run`<br>
`dotnet watch run`to rebuild the changes automatically <br>
The API will be running on 7054 port

## Configuration
You should create a configuration file (appsettings.json) with the following info

````
{
  "ConnectionStrings": {
    "WebApiDatabase": "server=localhost; database={your-database-name}; user={your-user}; password={your-password}"
},
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
````
* In some cases you will need to config you database connection data in `appsettings.Development.json` file

## Exporting Global Variables
`export PATH=$PATH:/home/{system-username}/.dotnet` <br>
`export PATH=$PATH:/home/{system-username}/.dotnet/tools`

## Running migrations
`dotnet tool install --global dotnet-ef` <br>
`dotnet ef database update`

## Swagger URL
You can check the Swagger API documentation on URL https://localhost:7054/swagger/index.html

