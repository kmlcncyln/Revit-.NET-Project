using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;

namespace ClassLibrary1
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class MyTest : IExternalCommand
    {
        // HttpClient instance for making HTTP requests to the weather API
        private static readonly HttpClient httpClient = new HttpClient() { BaseAddress = new Uri("https://api.openweathermap.org/") };

        // API key and coordinates for weather data retrieval
        private const string apikey = "1cbb1acf59e92203749bf86a549c97df";
        private const string lat = "41.01384";
        private const string lon = "28.94966";
        private const string lang = "tr";

        // Random instance for generating random wall locations
        private static readonly Random rand = new Random();

        // Execute method required by IExternalCommand interface
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Get the active Revit application and document
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            // Get the default level to place walls
            Level level = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().FirstOrDefault();

            // Fetch weather data from the API
            HttpResponseMessage response = httpClient.GetAsync($"data/2.5/weather?lat={lat}&lon={lon}&appid={apikey}").Result;
            string weatherStr = response.Content.ReadAsStringAsync().Result;

            // Start a transaction to create walls and set weather data
            using (Transaction tx = new Transaction(doc))
            {
                try
                {
                    tx.Start("CreateWall");

                    // Create two random walls and set weather data to their parameters
                    for (int i = 0; i < 2; i++)
                    {
                        Wall wall = CreateRandomWall(doc, level);
                        Parameter parameter = wall.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
                        parameter.Set(weatherStr);
                    }

                    tx.Commit();
                }
                catch (Exception ex)
                {
                    // Handle any exceptions and roll back the transaction
                    Debug.Print(ex.Message);
                    tx.RollBack();
                }

            }

            return Result.Succeeded;
        }

        // Method to create a random wall
        private static Wall CreateRandomWall(Document doc, Level level)
        {
            // Generate random start and end points for the wall
            XYZ startPoint = new XYZ(rand.Next(-100, 100), rand.Next(-100, 100), 0);
            XYZ endPoint = new XYZ(rand.Next(-100, 100), rand.Next(-100, 100), 0);
            Line line = Line.CreateBound(startPoint, endPoint);

            // Create the wall using the generated line and level
            Wall wall = Wall.Create(doc, line, level.Id, true);
            return wall;
        }
    }
}
