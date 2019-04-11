namespace SNIESWebApplication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SNIESWebApplication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SNIESWebApplication.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Periodos.AddOrUpdate(
               x => x.Id,
               new Periodo { Id = 1, FechaPeriodo = "2014-01" },
               new Periodo { Id = 2, FechaPeriodo = "2014-02" },
               new Periodo { Id = 3, FechaPeriodo = "2015-01" },
               new Periodo { Id = 4, FechaPeriodo = "2015-02" },
               new Periodo { Id = 5, FechaPeriodo = "2016-01" },
               new Periodo { Id = 6, FechaPeriodo = "2016-02" },
               new Periodo { Id = 7, FechaPeriodo = "2017-01" },
               new Periodo { Id = 8, FechaPeriodo = "2017-02" },
               new Periodo { Id = 9, FechaPeriodo = "2018-01" },
               new Periodo { Id = 10, FechaPeriodo = "2018-02" },
               new Periodo { Id = 11, FechaPeriodo = "2019-01" }
               );

        }
    }
}
