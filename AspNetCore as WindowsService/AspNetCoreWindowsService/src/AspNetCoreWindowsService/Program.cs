using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using System.Diagnostics;
using System.Linq;

namespace AspNetCoreWindowsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
        }
    }
}
