# Installation

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later

## Install from NuGet

```bash
dotnet tool install --global JFM.RoslynNavigator
```

## Install from source

```bash
git clone https://github.com/jfmeyers/jfm-roslyn-navigator.git
cd jfm-roslyn-navigator
dotnet pack -c Release -o ./nupkgs
dotnet tool install --global --add-source ./nupkgs JFM.RoslynNavigator
```

## Verify installation

```bash
jfm-roslyn-navigator --help
```

## Update

```bash
dotnet tool update --global JFM.RoslynNavigator
```

## Uninstall

```bash
dotnet tool uninstall --global JFM.RoslynNavigator
```
