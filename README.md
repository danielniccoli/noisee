# noisee

Reads and displays the seed of your current Noita run from memory.

![](https://user-images.githubusercontent.com/2971735/170843170-f4d830f6-0bb0-401d-b927-a186f22f8407.png)

## How to run

Run `noisee.exe` before or after you start Noita.

## Build from source

To build from source and publish a single-file binary (requires [.NET 6 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/runtime)), run

```powershell-interactive
.\Publish-Project.ps1 -Configuration Release
```

To build form source and publish as a single-file **and self-contained** binary ([.NET 6 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/runtime) included, but larger file size), run

```powershell-interactive
.\Publish-Project.ps1 -Configuration Release -SelfContained
```

## About Publish-Project.ps1

Use `Get-Help .\Publish-Project.ps1` for more information.
