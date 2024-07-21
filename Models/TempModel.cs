using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Crucigrama.Db
{
    public class CrucigramaContext : DbContext
    {
        public DbSet<Answer> Answers { get; set; }

        public string DbPath { get; }

        public CrucigramaContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>().ToTable("Answers");
        }
    }

    public class Answer
    {
        public int Id { get; set; }
        public string Word { get; set; }
    }
}
