using dbpRestAPI.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;

namespace dbpRestAPI.DataLayer
{

    public class dbpDatabaseContext : DbContext
    {
        //Add Any lists of model objects you create here.
        public DbSet<PortfolioBook> PortfolioBooks { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }

        public dbpDatabaseContext() : base("dbpDatabaseConnectionString")
        {
            //DataSource = C:\Users\dbrown\Documents\GitHubVisualStudio\DBPAngular\DBPAngular\App_Data\DBPSqlCompact.mdf
            //The line that's commented out will most likely be used for migrations.
            //That's because the migrations won't have a HttpContext.Current, so you get a null ref exception.  I still want to keep it relative pathed though
            //so it's easy when I deploy the database
            this.Database.Connection.ConnectionString = "DataSource = " + HttpContext.Current.Server.MapPath("~/App_Data/DBPSqlCompact.sdf");
            //This is the location of the database on my work laptop, but will probably be different on different machines.
            //C:\Users\Asus\Documents\Visual Studio 2015\Projects\dbpRestAPI\dbpRestAPI\App_Data
            //C:\dbpRestAPI\dbpRestAPI\App_Data
            //this.Database.Connection.ConnectionString = @"DataSource = C:\dbpRestAPI\dbpRestAPI\App_Data\DBPSqlCompact.sdf";
        }

        public static dbpDatabaseContext Create()
        {
            return new dbpDatabaseContext();
        }
    }

    //This function will ensure the database is created and seeded with any default data.
    public class DBInitializer : CreateDatabaseIfNotExists<dbpDatabaseContext>
    {
        public object db { get; private set; }

        protected override void Seed(dbpDatabaseContext context)
        {
            
        }
    }
}