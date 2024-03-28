using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;

namespace ClassLibrary1
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class MyExport : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Get the active Revit application and document
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            // Get the active view
            View activeView = doc.ActiveView;

            // Get the desktop path
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Export the active view to DWG and save the document
            ExportAndViewToDWG(doc, activeView, desktopPath, "MyExportedView");

            return Result.Succeeded;
        }

        private void ExportAndViewToDWG(Document doc, View view, string desktopPath, string fileName)
        {
            // Set up DWG export options
            DWGExportOptions dwgOptions = new DWGExportOptions();

            // Export the view to DWG format
            doc.Export(desktopPath, $"{fileName}.dwg", new List<ElementId> { view.Id }, dwgOptions);

            // Save the document with the specified file name on the desktop
            string savePath = Path.Combine(desktopPath, $"{fileName}.rvt");
            SaveAsOptions saveOptions = new SaveAsOptions();
            saveOptions.OverwriteExistingFile = true;
            doc.SaveAs(savePath, saveOptions);
        }
    }
}
