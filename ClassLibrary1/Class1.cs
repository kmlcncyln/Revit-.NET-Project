using Autodesk.Revit.UI;
using System.Reflection;

namespace ClassLibrary1
{

    public class Class1 : IExternalApplication
    {

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("RibbonPanel");

            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData("cmdMyTest", "My Test", thisAssemblyPath, "ClassLibrary1.MyTest");
            PushButtonData buttonData2 = new PushButtonData("cmdMyExport", "My Export", thisAssemblyPath, "ClassLibrary1.MyExport");
            _ = ribbonPanel.AddItem(buttonData) as PushButton;
            _ = ribbonPanel.AddItem(buttonData2) as PushButton;

            return Result.Succeeded;
        }
    }
}