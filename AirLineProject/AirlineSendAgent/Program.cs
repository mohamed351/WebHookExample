using AirlineSendAgent.App;
using AirlineSendAgent.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace AirlineSendAgent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                  .ConfigureServices((context, service) =>
                  {
                      service.AddSingleton<IAppHost, AppHost>();
                     
                      Console.WriteLine(context.Configuration.GetConnectionString("DefaultConnection"));
                      service.AddDbContext<ApplicationDbContext>(a =>
                      {
                          a.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection"));
                         
                      });
                      service.AddSingleton<IWebHookClient, WebHookClient>();
                      service.AddHttpClient();
                  }).Build();

            host.Services.GetService<IAppHost>().Run();
        }
    }
}
