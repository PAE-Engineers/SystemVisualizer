namespace SystemVisualizer.Tests;

[TestFixture]
public class DataMappingTest
{

    [Test]
    public void ConvertData()
    {
                   // 1) Adjust these paths as needed
            var inputPath  = @"C:\Users\sam.najjar\source\repos\narrsam\SystemVisualizer\src\SystemVisualizer.Tests\TestData\test2.csv";
            var outputPath = @"C:\Users\sam.najjar\source\repos\narrsam\SystemVisualizer\src\SystemVisualizer.Tests\TestData\test2_renamed.csv";
            
            // 2) Read all lines
            var lines = File.ReadAllLines(inputPath);
            if (lines.Length < 2)
            {
                Console.WriteLine("Input CSV must have at least header + one data row.");
                return;
            }

            // 3) Parse header & data
            var header = lines[0];
            var records = lines
                .Skip(1)
                .Select(line => line.Split(',', 2))    // split into exactly 2 columns
                .Where(cols => cols.Length == 2)
                .ToList();

            // 4) Gather all unique panel names (both columns)
            var allNames = records
                .SelectMany(cols => new[] { cols[0].Trim(), cols[1].Trim() })
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct()
                .ToList();

            // 5) Build mapping to new, simplified names
            var mapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            int switchgearCounter  = 1;
            int substationCounter  = 1;
            int genericCounter     = 1;

            foreach (var original in allNames)
            {
                // TODO: Tweak this logic to suit your naming convention.
                // Example:
                //   - If original contains "SWG" → Elec-Switchgear-PanelX
                //   - Else if it contains "SUB" or "FN4" → Substation-PanelY
                //   - Otherwise → Generic-PanelZ

                string newName;
                if (original.IndexOf("SWG", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    newName = $"Elec-Switchgear-Panel{switchgearCounter++}";
                }
                else if (original.IndexOf("SUB", StringComparison.OrdinalIgnoreCase) >= 0
                      || original.IndexOf("FN4", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    newName = $"Substation-Panel{substationCounter++}";
                }
                else
                {
                    newName = $"Generic-Panel{genericCounter++}";
                }

                mapping[original] = newName;
            }

            // 6) Write out new CSV
            using (var writer = new StreamWriter(outputPath))
            {
                writer.WriteLine(header); 
                
                foreach (var cols in records)
                {
                    var origPanel   = cols[0].Trim();
                    var origSupply = cols[1].Trim();

                    // Look up mapped names (if any name is empty, just leave it blank)
                    var newPanel   = mapping.ContainsKey(origPanel)   ? mapping[origPanel]   : "";
                    var newSupply  = mapping.ContainsKey(origSupply)  ? mapping[origSupply]  : origSupply;

                    writer.WriteLine($"{newPanel},{newSupply}");
                }
            }

            Console.WriteLine($"Finished! Renamed CSV written to: {outputPath}");

 
    }
        
}