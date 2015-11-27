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
            //this.Database.Connection.ConnectionString = @"DataSource = C:\Users\Asus\Documents\GitHubVisualStudio\DBPAngular\DBPAngular\App_Data\DBPSqlCompact.mdf";
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
            //List<PortfolioBook> portfolios = new List<PortfolioBook>() {
            //    new PortfolioBook()
            //    {
            //        Title = "Rebecca",
            //        Description = "I had a blast shooting these",
            //        ImageThumbnailURL = "http://www.davebrownphotography.com/Images/Fashion/_DSC1141.jpg",
            //        Order = 2

            //    },
            //    new PortfolioBook()
            //    {
            //        Title = "Liv",
            //        Description = "Liv was super nice",
            //        ImageThumbnailURL = "http://www.davebrownphotography.com/Images/Fashion/01ABDSC9151.jpg",
            //        Order = 3
            //    }
            //};

            //foreach (var book in portfolios)
            //{
            //    context.PortfolioBooks.Add(book);
            //    context.SaveChanges();
            //}
        }
    }
}