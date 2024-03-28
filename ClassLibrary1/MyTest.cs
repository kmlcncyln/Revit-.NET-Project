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
        private static readonly HttpClient httpClient = new HttpClient() { BaseAddress = new Uri("https://api.openweathermap.org/") };
        private const string apikey = "1cbb1acf59e92203749bf86a549c97df";
        private const string lat = "41.01384";
        private const string lon = "28.94966";
        private const string lang = "tr";
        private static readonly Random rand = new Random();

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            Level level = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().FirstOrDefault();

            HttpResponseMessage response = httpClient.GetAsync($"data/2.5/weather?lat={lat}&lon={lon}&appid={apikey}").Result;
            string weatherStr = response.Content.ReadAsStringAsync().Result;

            using (Transaction tx = new Transaction(doc))
            {
                try
                {
                    tx.Start("CreateWall");

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
                    Debug.Print(ex.Message);
                    tx.RollBack();
                }

            }

            return Result.Succeeded;
        }

        private static Wall CreateRandomWall(Document doc, Level level)
        {
            XYZ startPoint = new XYZ(rand.Next(-100, 100), rand.Next(-100, 100), 0);
            XYZ endPoint = new XYZ(rand.Next(-100, 100), rand.Next(-100, 100), 0);
            Line line = Line.CreateBound(startPoint, endPoint);
            Wall wall = Wall.Create(doc, line, level.Id, true);
            return wall;
        }



    }
}
