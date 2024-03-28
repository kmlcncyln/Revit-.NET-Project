# Project Name: Revit .NET Project

## Project Purpose and Scope:

This project is an application developed on the .NET platform using Autodesk Revit. The main purpose of the project is to create different types of walls, connect to a public web API such as weather conditions, retrieve data from the API, assign this data to wall parameters, then export a specific view as a .dwg file, and finally save the document to the user's desktop.

## Technologies and Tools Used:

Autodesk Revit API
- C# Programming Language 
- .NET Framework 
- Visual Studio IDE 

## Project Structure and Components:

The project consists of a series of C# classes. The main classes are as follows:

- MyTest: This is the main class of the project and implements the IExternalCommand interface. Autodesk Revit commands are defined here.
- WeatherAPI: This is a helper class used to connect to the weather API.
- MyExport: This class is used to export a specific view as a .dwg file and save the document to the user's desktop.

## Key Functions and Functionality:

```bash
CreateRandomWall(): Used to create random walls.
ConnectToWeatherAPI(): Connects to the weather API and retrieves data.
SetWeatherDataToWall(): Assigns data retrieved from the API to wall parameters.
ExportAndViewToDWG(): Exports a specific view as a .dwg file.
SaveDocumentToDesktop(): Saves the document to the user's desktop.

```

## Installation Instructions:

- Open Visual Studio IDE and load the project.
- Add references to RevitAPI and RevitAPIUI in the project.
- Build the project to ensure that there are no compilation errors.
- Install the compiled project in Revit by following these steps:
a. In Revit, create new project and go to the "Add-Ins" tab.
b. Click on "External Tools" and then select "Add-Ins Manager."
c. In the Add-Ins Manager dialog, click on the "Add" button.
d. Browse to the location where the compiled project file (.dll) is located.
e. Select the .dll file and click "Open" to add the add-in to Revit.
f. Close the Add-Ins Manager dialog.
- The project should now be installed in Revit and ready to use.


## Resources and References:

Autodesk Revit API Documentation
https://www.revitapidocs.com/2024/


OpenWeatherMap API Documentation
https://openweathermap.org/api


C# Programming Language Resources
https://learn.microsoft.com/en-us/dotnet/
