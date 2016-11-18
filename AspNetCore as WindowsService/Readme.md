# AspNetCore as a WindowsService

This is a simple sample repository to show how simple it is to run ASP.NET Core as a Windows Service.

Highlights:

1) You have to target .NET 4.6.1 framework. Since this application is intented to run as a Windows Service it makes sense since Linux or Mac don't have a Windows service so it can't be part of the .NET Core framework.

    [project.json]
    ```
    "frameworks": {
        "net461": { }
    }
    ```
2) Add this Microsoft.AspNetCore.Hosting.WindowsServices in the dependencies list

    [project.json]
    ```
    "dependencies": {
        "Microsoft.AspNetCore.Mvc": "1.0.1",
        "Microsoft.AspNetCore.Hosting.WindowsServices": "1.0.0",
        "Microsoft.AspNetCore.Server.Kestrel": "1.0.0",
        "Microsoft.Extensions.Options.ConfigurationExtensions": "1.0.0",
        "Microsoft.AspNetCore.StaticFiles": "1.0.0"
    },
    ```
    
     
3) You have to use Kestrel web server when running as a Windows Service.

    [Program.cs]
    ```
    string contentPath;
    if (Debugger.IsAttached || args.Contains("--console"))
        contentPath = Directory.GetCurrentDirectory();
    else
        contentPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

    var host = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(contentPath)
        .UseStartup<Startup>()
        .Build();

    if (Debugger.IsAttached || args.Contains("--console"))
        host.Run();
    else
        host.RunAsService();
    ```
4) Publish release build to file system.

5) To install as a Windows Service you need to run below statement in the command prompt (with Administrator privileges):
    ``` 
    sc create [service name] [binPath= ]   (Change ServiceName and binPath to match yours)
    ```
    Example:
    ```
    sc create AspNetCoreWindowsService binPath= "(FullPathToOutputPath)\AspNetCoreWindowsService.exe"
    ```