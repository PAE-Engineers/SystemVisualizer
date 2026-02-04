# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

SystemVisualizer is a .NET 9 cross-platform desktop application built with Avalonia UI for visualizing system graphs. It reads electrical panel data from Excel/CSV files and displays them as interactive node graphs with automatic layout algorithms.

## Build Commands

```bash
# Build solution
dotnet build src/SystemVisualizer.sln

# Run desktop application
dotnet run --project src/SystemVisualizer.Desktop/SystemVisualizer.Desktop.csproj

# Run all tests
dotnet test src/SystemVisualizer.sln

# Run specific test project
dotnet test src/SystemVisualizer.Tests/SystemVisualizer.Tests.csproj
```

## Architecture

### Project Structure

- **SystemVisualizer** - Main Avalonia UI application (Views, ViewModels, Controls, Themes)
- **SystemVisualizer.Core** - Domain models and interfaces (Node, Edge, Connector, Graph, IDataProvider, ILayoutProvider)
- **SystemVisualizer.DataProviders** - Excel/CSV file parsing using ExcelMapper and CsvHelper
- **SystemVisualizer.Layout** - Graph layout computation using Microsoft MSAGL
- **SystemVisualizer.Desktop** - Entry point executable
- **SystemVisualizer.Tests** - NUnit test project

### Key Patterns

**MVVM Architecture:**
- ViewModels use CommunityToolkit.MVVM with `[ObservableProperty]` and `[RelayCommand]` attributes
- Views are XAML-based (`.axaml` files) with compiled bindings (`x:DataType`)
- WeakReferenceMessenger for decoupled communication between components

**Data Flow:**
1. User opens file → `MainViewModel.OpenDataFileAsync()`
2. `ExcelDataProvider.GetNodes()` parses data into nodes and connections
3. `EditorViewModel.LoadData()` populates observable collections
4. `MsaglLayoutProvider.ApplyLayout()` positions nodes
5. Views bind to collections and render the graph

### Key Interfaces

- **IGraphItem** - Node representation with Name, Location, Bounds, Cluster
- **IDataProvider** - Loads graph data from files (streams)
- **ILayoutProvider** - Computes node positions for given nodes/connections
- **ILayoutCalculator** - Layout algorithm abstraction (Sugiyama, MDS, Ranking, etc.)

## Technology Stack

- .NET 9.0 with nullable reference types
- Avalonia UI 11.3.0 (cross-platform)
- Nodify/NodifyAvalonia for node editor control
- Microsoft.Msagl for hierarchical graph layout
- ExcelMapper/CsvHelper for data import
- CommunityToolkit.Mvvm for MVVM infrastructure
- NUnit 4.3 for testing

## Configuration

- Central package management via `Directory.Packages.props`
- .NET SDK 9.0 with pre-release allowance (`global.json`)
- Debug builds include Avalonia.Diagnostics
