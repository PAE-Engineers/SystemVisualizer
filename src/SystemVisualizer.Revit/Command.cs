#region Namespaces

using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using SystemVisualizer.App.Forms;

#endregion

namespace SystemVisualizer.App
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
                 ExternalCommandData commandData,
                 ref string message,
                 ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            //var pattern = @"^\d{2}.*$";
            //pattern = @"^.*\.rvt$"; // Updated pattern to match any *.rvt name
            //var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            //List<Document> docs = (from Document d in app.Documents let match = regex.IsMatch(d.PathName) where match select d)
            //    .OrderBy(d => d.PathName)
            //    .ToList();

            //var sb = new StringBuilder();

            //foreach (var d in docs)
            //{
            //    sb.AppendLine(d.PathName + ":");
            //    sb.AppendLine("__________________________________________________");
            //    var draftingViews = new FilteredElementCollector(d)
            //       .OfClass(typeof(ViewDrafting))
            //       .Cast<ViewDrafting>()
            //       .Where(v => !v.IsTemplate)
            //       .OrderBy(v => v.Name)
            //       .ToList();

            //    sb.AppendLine(string.Join("\n", draftingViews.Select(v => v.Name)));

            //    sb.AppendLine("");
            //    sb.AppendLine("__________________________________________________");
            //}

            //// Create and write to a text file
            //string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "DocumentsList.txt");
            //File.WriteAllText(filePath, sb.ToString());

            //// Open the file in Notepad
            //Process.Start("notepad.exe", filePath);

           
            var w = new SystemVisualizerView();
            w.Show();

            return Result.Succeeded;
        }
    }
}
