// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.ServiceProcess;
using Microsoft.AspNetCore.Hosting.WindowsServices;


namespace SGSLogViewer
{
    public class Program
    {
        static string pathToExe;
        static string pathToContentRoot;

        public static void Main(string[] args)
        {
            pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            pathToContentRoot = Path.GetDirectoryName(pathToExe);
            //CreateWebHostBuilder(args).Build().Run();
            CreateWebHostBuilder(args).Build().RunAsService();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
                  .UseUrls("http://0.0.0.0:7575")
                  .UseContentRoot(pathToContentRoot)
                  .UseStartup<Startup>();
    }
}
