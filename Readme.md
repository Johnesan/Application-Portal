### Description
This is a sample application portal with fluentValidation and localization support.
It is built with ASP.NET Core 5.0 (API) and Aurelia (SPA).

### Build (Development)
To run, you need nodejs (> v10) and dotnet core runtime(.net5.0) installed on your machine.
In the .Web project root folder, run the command
`dotnet run`

This should restore all packages and install the necessary nuget packages.
Browse the application via `https://localhost:5001/`


##### IIS Express via Visual Studio
If you are opening the project on visual studio and your launch profile is IIS Express, 
you would need to make a few modifications to the baseUrl specified in Aurelia's 
environment.json file. You would find the relevant dotnet api applicationUrl & ssl port in 
the  **iisSettings -> iisExpress** section of your launchSettings.json file. It is currently
set to `http://localhost:49128` and port `44355`

Open the environment.json file in the folder `~/ClientApp/config/environment.json` and replace
the baseUrl with `https://localhost:44355/` then run the application( with IIS Express option on visual studio).

### Deploy (Production)
To publish and run this application in production, run the following command in your .Web project root folder
`dotnet publish -c Release `

Your deployed application(.exe) is available in the location: `~/bin/release/net5.0/publish` folder. 
Run it and navigate to `https://localhost:5001/`



