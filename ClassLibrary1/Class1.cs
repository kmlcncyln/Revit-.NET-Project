using Autodesk.Revit.UI;
using System.Reflection;

namespace ClassLibrary1
{
    // Class implementing the IExternalApplication interface
    public class Class1 : IExternalApplication
    {
        // Method called when Revit shuts down
        public Result OnShutdown(UIControlledApplication application)
        {
            // Return success
            return Result.Succeeded;
        }

        // Method called when Revit starts up
        public Result OnStartup(UIControlledApplication application)
        {
            // Create a new ribbon panel named "RibbonPanel"
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("RibbonPanel");

            // Get the path of the current assembly
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            // Define PushButtonData for the "My Test" button
            PushButtonData buttonData = new PushButtonData("cmdMyTest", "My Test", thisAssemblyPath, "ClassLibrary1.MyTest");

            // Define PushButtonData for the "My Export" button
            PushButtonData buttonData2 = new PushButtonData("cmdMyExport", "My Export", thisAssemblyPath, "ClassLibrary1.MyExport");

            // Add the "My Test" button to the ribbon panel
            _ = ribbonPanel.AddItem(buttonData) as PushButton;

            // Add the "My Export" button to the ribbon panel
            _ = ribbonPanel.AddItem(buttonData2) as PushButton;

            // Return success
            return Result.Succeeded;
        }
    }
}
