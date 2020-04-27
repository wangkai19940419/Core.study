using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wk.Study.Service.Services;

namespace Wk.Study.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureLogging(loggingBuilder =>
                {
                    //loggingBuilder.AddFilter("System", LogLevel.Warning);   //����ϵͳ��־
                    //loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
                    loggingBuilder.AddLog4Net();//��Ҫ�����ļ�
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
