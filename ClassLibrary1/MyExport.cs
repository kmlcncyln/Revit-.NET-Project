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
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;


            View activeView = doc.ActiveView;


            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            ExportAndViewToDWG(doc, activeView, desktopPath, "MyExportedView");

            return Result.Succeeded;
        }

        private void ExportAndViewToDWG(Document doc, View view, string desktopPath, string fileName)
        {

            DWGExportOptions dwgOptions = new DWGExportOptions();
            doc.Export(desktopPath, $"{fileName}.dwg", new List<ElementId> { view.Id }, dwgOptions);


            string savePath = Path.Combine(desktopPath, $"{fileName}.rvt");
            SaveAsOptions saveOptions = new SaveAsOptions();
            saveOptions.OverwriteExistingFile = true;
            doc.SaveAs(savePath, saveOptions);
        }
    }
}
