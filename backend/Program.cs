using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
         {
        //      Console.WriteLine("hellodata");
        //      string cs = @"server=localhost;userid=root;password=fathimaadmin;database=DOCUMENT_MANAGEMENT";
        //     Console.WriteLine(cs);

        //     using var con = new MySqlConnection(cs);
        //     con.Open();

        //     Console.WriteLine($"MySQL version : {con.ServerVersion}");
            CreateHostBuilder(args).Build().Run();
           
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
