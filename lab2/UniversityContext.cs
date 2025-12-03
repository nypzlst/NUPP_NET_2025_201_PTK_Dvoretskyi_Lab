using lab2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace lab2
{
    public class UniversityContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCard> StudentCards { get; set; }

        public UniversityContext()
        {
        }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
       
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=university.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Person>().UseTptMappingStrategy();
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Professor>().ToTable("Professors");

            
            modelBuilder.Entity<Professor>()
                .HasMany(p => p.Courses)
                .WithOne(c => c.Professor)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<Student>()
                .HasOne(s => s.StudentCard)
                .WithOne(c => c.Student)
                .HasForeignKey<StudentCard>(c => c.StudentId);


        }
    }
}
