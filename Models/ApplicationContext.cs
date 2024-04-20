using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using WebServiceCurrency.Models;


namespace WebServiceCurrency.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Valute> Valutes { get; set; }
        public ApplicationContext()
        {
            _ = Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PathWatcher;Trusted_Connection=True;");
            Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
            string path = Environment.GetFolderPath(folder);
             
            _ = optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite($"Data Source={path}{Path.DirectorySeparatorChar}path-watcher.db");
        }
    }
}