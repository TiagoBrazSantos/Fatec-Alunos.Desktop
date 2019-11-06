using Fatec.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fatec.Core.Connection
{
    public class FatecContext : DbContext
    {
        public FatecContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            var caminhoBd = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Fatec");

            if (!Directory.Exists(caminhoBd))
            {
                Directory.CreateDirectory(caminhoBd);
            }

            optionbuilder.UseSqlite(@$"Data Source={caminhoBd}\Fatec.db");
        }

        public void CreateDatabase()
        {
            Database.EnsureCreated();
        }

        public DbSet<Aluno> Aluno { get; set; }
    }
}
