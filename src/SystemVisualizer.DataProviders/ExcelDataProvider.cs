using Ganss.Excel;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using SystemVisualizer.Core.Compareres;
using SystemVisualizer.Core.Enums;
using SystemVisualizer.Core.Interfaces;
using SystemVisualizer.Core.Models;

namespace SystemVisualizer.DataProviders
{
    public class ExcelDataProvider : IDataProvider
    {
        private List<Entry> GetEntries()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var filePath =
                "C:\\Users\\sam.najjar\\source\\repos\\Revit-System-Visualizer\\src\\SystemVisualizer\\Data\\onelinebackcheck.xlsx";
            var mapper = new ExcelMapper(filePath)
            {
                HeaderRow = true,
            };
            var entries = mapper.Fetch<Entry>();
            return entries.ToList();
        }

        public object GetRawData()
        {
            var entries = GetEntries();
            return entries.Select(e => (e.PanelName, e.SupplyFrom)).ToList();
        }

        private async Task<List<Entry>> ProcessCsvFileAsync(Stream fileStream)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    PrepareHeaderForMatch = args => args.Header.ToLower() 
                };
                
                using var reader = new StreamReader(fileStream);
                using var csv = new CsvReader(reader, config);
                
                var records = new List<Entry>();
                var recordsAsync = csv.GetRecordsAsync<Entry>();
                await foreach(var r in recordsAsync)
                {
                    records.Add(r);
                }
                return records.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private (IEnumerable<IGraphItem> Nodes, IEnumerable<Edge> Connections) ProcessNodes(List<Entry> entries)
        {
            var nodes = entries.Select(e => new Node { Name = e.PanelName }).ToList();
            List<Node> supplyNodes = new();
            var connections = new List<Edge>();

            foreach (var node in nodes)
            {
                var entry = entries.FirstOrDefault(e => e.PanelName == node.Name);
                if (entry == null || string.IsNullOrEmpty(entry.SupplyFrom)) continue;

                var sourceNode =  nodes.FirstOrDefault(n => n.Name == entry.SupplyFrom) ??
                                  supplyNodes.FirstOrDefault(n => n.Name == entry.SupplyFrom);
                if (sourceNode == null)
                {
                    sourceNode = new Node { Name = entry.SupplyFrom };
                        supplyNodes.Add(sourceNode);
                }

                var inConnector = new Connector{Name = "Input"};
                node.Inputs.Add(inConnector);
                var outConnector = new Connector{Name = "Output"};
                sourceNode.Outputs.Add(outConnector);

                var connection = new Edge
                {
                    Source = outConnector,
                    Target = inConnector
                };
                connections.Add(connection);
            }

            nodes.AddRange(supplyNodes);
            return (nodes, connections);
        }

        public async Task<(IEnumerable<IGraphItem> Nodes, IEnumerable<Edge> Connections)> GetNodes(Stream fileStream,
            DataFormat format)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            List<Entry> entries;
            try
            {
                switch (format)
                {
                    case DataFormat.CSV:
                        entries =  await ProcessCsvFileAsync(fileStream);
                        break;
                    case DataFormat.Excel:
                        var mapper = new ExcelMapper(fileStream) { HeaderRow = true };
                        entries = mapper.Fetch<Entry>().ToList();
                        break;
                    default:
                        throw new NotSupportedException($"The format {format} is not supported.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return ProcessNodes(entries);
        }

        public (IEnumerable<IGraphItem> Nodes, IEnumerable<Edge> Connections) GetNodes()
        {
            var entries = GetEntries();

            var nodes = entries.Select(e => new Node { Name = e.PanelName }).ToList();

            List<Node> supplyNodes = new();

            var connections = new List<Edge>();
            foreach (var node in nodes)
            {
                var entry = entries.FirstOrDefault(e => e.PanelName == node.Name);

                if (entry == null || string.IsNullOrEmpty(entry.SupplyFrom)) continue;

                var sourceNode = nodes.FirstOrDefault(n => n.Name == entry.SupplyFrom);

                if (sourceNode == null)
                {
                    sourceNode = new Node { Name = entry.SupplyFrom };
                    if (!supplyNodes.Contains(sourceNode, new NodeEqualityComparerByName()))
                    {
                        supplyNodes.Add(sourceNode);
                    }
                }

                node.Inputs.Add(new Connector { Name = "Inputs" });

                sourceNode.Outputs.Add(new Connector { Name = "Outputs" });

                var connection = new Edge
                {
                    Source = sourceNode.Outputs.Last(),
                    Target = node.Inputs.Last()
                };
                connections.Add(connection);
            }

            nodes.AddRange(supplyNodes);

            return (nodes, connections);
        }
    }
}