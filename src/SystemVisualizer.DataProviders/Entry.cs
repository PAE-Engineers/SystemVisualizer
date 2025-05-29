using CsvHelper.Configuration.Attributes;
using Ganss.Excel;

namespace SystemVisualizer.DataProviders;

public class Entry
{
    [Column("PANEL NAME" )]
    [Name("PANEL NAME")]
    public string PanelName { get; set; }

    [Column("SUPPLY FROM")]
    [Name("SUPPLY FROM")]
    public string SupplyFrom { get; set; }
}