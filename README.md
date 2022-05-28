# noisee

Reads the seed of your current Noita run from memory.

# How to run

Start noisee.exe before or after you start Noita.

# Build from source

To publish as a single-file binary (requires [.NET 6 Runtime](<https://dotnet.microsoft.com/en-us/download/dotnet/6.0/runtime>)), run

```
dotnet publish --output "C:\temp\" --runtime win-x64 --configuration Release -p:PublishSingleFile=true --self-contained false
```

To publish as a single-file and self-contained binary, run

```
dotnet publish --output "C:\temp\" --runtime win-x64 --configuration Release -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
```
