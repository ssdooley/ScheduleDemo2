using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Schedule.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Commitment> Commitments { get; set; }
        public DbSet<CommitmentPerson> CommitmentPeople { get; set; }
        public DbSet<Person> People { get; set; }
    }
}
