using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PhonebookApp.Service;
using PhonebookApp.Models;


namespace PhonebookApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //It is not tested!
            //Supposed to check at 9:30 every 24 hours if some deleted contacts are passing the 30 days
            MyScheduler.IntervalInHours(9, 30, 24,
            () => {
                ContactModel model = new ContactModel();

                model.RemoveDeletedContacts();
            });

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
