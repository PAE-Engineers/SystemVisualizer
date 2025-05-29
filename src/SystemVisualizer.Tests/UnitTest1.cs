using SystemVisualizer.Core.Enums;
using SystemVisualizer.DataProviders;

namespace SystemVisualizer.Tests;

public class ExcelDataProviderTests
{
    private ExcelDataProvider _dataProvider;
    private const string TestDataPath = "TestData";

    [SetUp]
    public void Setup()
    {
        _dataProvider = new ExcelDataProvider();
        Directory.CreateDirectory(TestDataPath);
    }

    [TearDown]
    public void Cleanup()
    {
        if (Directory.Exists(TestDataPath))
            Directory.Delete(TestDataPath, true);
    }

    [Test]
    public async Task GetNodes_WithValidExcelFile_ReturnsNodesAndConnections()
    {
        // Arrange
        var filePath = Path.Combine(TestDataPath, "test.xlsx");
        using var stream = CreateTestExcelFile(filePath);

        // Act
        var (nodes, connections) = await _dataProvider.GetNodes(stream, DataFormat.Excel);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(nodes, Is.Not.Empty);
            Assert.That(connections, Is.Not.Empty);
        });
    }

    [Test]
    public async Task GetNodes_WithValidCsvFile_ReturnsNodesAndConnections()
    {
        // Arrange
        var filePath = Path.Combine(TestDataPath, "test.csv");
        var fileInfo = new FileInfo(filePath);
        Assert.That(fileInfo.Exists, Is.True);
        
        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        // Act
        var (nodes, connections) = await _dataProvider.GetNodes(stream, DataFormat.CSV);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(nodes, Is.Not.Empty);
            Assert.That(connections, Is.Not.Empty);
        });
    }

    [Test]
    public async Task GetNodes_WithEmptyFile_ReturnsEmptyCollections()
    {
        // Arrange
        var filePath = Path.Combine(TestDataPath, "empty.xlsx");
        using var stream = File.Create(filePath);

        // Act
        var (nodes, connections) = await _dataProvider.GetNodes(stream, DataFormat.Excel);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(nodes, Is.Empty);
            Assert.That(connections, Is.Empty);
        });
    }

    private static Stream CreateTestExcelFile(string filePath)
    {
        // Create test Excel file implementation
        return File.Create(filePath);
    }

    private static Stream CreateTestCsvFile(string filePath)
    {
        // Create test CSV file implementation
        return File.Create(filePath);
    }
    
}