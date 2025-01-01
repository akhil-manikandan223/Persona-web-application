using EduBrain.Models.Blocks;
using EduBrain.Models.ClubRepresentatives;
using EduBrain.Models.Clubs;
using EduBrain.Models.Departments;
using EduBrain.Models.Drivers;
using EduBrain.Models.EmployeeCategories;
using EduBrain.Models.Employees;
using EduBrain.Models.Helpers;
using EduBrain.Models.HODs;
using EduBrain.Models.Janitors;
using EduBrain.Models.Locations;
using EduBrain.Models.States;
using EduBrain.Models.Subjects;
using EduBrain.Models.Teachers;
using EduBrain.Models.Vehicles;
using EduBrain.Models.Watchmen;
using Microsoft.EntityFrameworkCore;

namespace EduBrain.Data
{
    public class EduBrainContext : DbContext
    {
        public EduBrainContext(DbContextOptions<EduBrainContext> options)
            : base(options)
        {
        }

        public DbSet<Watchman> Watchmen { get; set; }
        public DbSet<Janitor> Janitors { get; set; }
        public DbSet<Helper> Helpers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<HOD> HODs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<EmployeeCategory> EmployeeCategories { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ClubRep> ClubReps { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Watchman Configuration
            modelBuilder.Entity<Watchman>()
                .HasOne(w => w.CategoryName)
                .WithMany() // Adjust if EmployeeCategory has a collection of Watchmen
                .HasForeignKey(w => w.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Watchman>()
                .HasOne(w => w.BlockName)
                .WithMany() // Adjust if Block has a collection of Watchmen
                .HasForeignKey(w => w.BlockId)
                .OnDelete(DeleteBehavior.Restrict);

            // Janitor Configuration
            modelBuilder.Entity<Janitor>()
                .HasOne(j => j.CategoryName)
                .WithMany()
                .HasForeignKey(j => j.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Janitor>()
                .HasOne(j => j.BlockName)
                .WithMany()
                .HasForeignKey(j => j.BlockId)
                .OnDelete(DeleteBehavior.Restrict);

            // Helper Configuration
            modelBuilder.Entity<Helper>()
                .HasOne(h => h.CategoryName)
                .WithMany()
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Helper>()
                .HasOne(h => h.VehicleNumber)
                .WithMany() // Adjust if Vehicle has a collection of Helpers
                .HasForeignKey(h => h.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Driver Configuration
            modelBuilder.Entity<Driver>()
                .HasOne(d => d.CategoryName)
                .WithMany()
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.VehicleNumber)
                .WithMany() // Adjust if Vehicle has a collection of Drivers
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            // HOD Configuration
            modelBuilder.Entity<HOD>()
                .HasOne(h => h.TeacherName)
                .WithMany() // Adjust if Teacher has a collection of HODs
                .HasForeignKey(h => h.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HOD>()
                .HasOne(h => h.DepartmentName)
                .WithMany() // Adjust if Department has a collection of HODs
                .HasForeignKey(h => h.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student Configuration
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Club)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClubId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict to prevent cascade deletes

            // Teacher Configuration
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Category)
                .WithMany() // Assuming one category can have many teachers
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Adjust according to your needs

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Department)
                .WithMany()
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict); // Adjust according to your needs

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Subject)
                .WithMany()
                .HasForeignKey(t => t.SubjectId)
                .OnDelete(DeleteBehavior.Restrict); // Adjust according to your needs

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Club)
                .WithMany()
                .HasForeignKey(t => t.ClubId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust according to your needs

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Location)
                .WithMany() // Assuming one location can have many teachers
                .HasForeignKey(t => t.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.State)
                .WithMany() // Assuming one state can have many teachers
                .HasForeignKey(t => t.StateId)
                .OnDelete(DeleteBehavior.Cascade);

            // Employee Configuration
            modelBuilder.Entity<Employee>()
                .HasOne(t => t.Category)
                .WithMany() // Assuming one category can have many teachers
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Adjust according to your needs

            modelBuilder.Entity<Employee>()
                .HasOne(t => t.Department)
                .WithMany()
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict); // Adjust according to your needs

            modelBuilder.Entity<Employee>()
                .HasOne(t => t.Subject)
                .WithMany()
                .HasForeignKey(t => t.SubjectId)
                .OnDelete(DeleteBehavior.Restrict); // Adjust according to your needs

            modelBuilder.Entity<Employee>()
                .HasOne(t => t.Club)
                .WithMany()
                .HasForeignKey(t => t.ClubId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust according to your needs

            modelBuilder.Entity<Employee>()
                .HasOne(t => t.Location)
                .WithMany() // Assuming one location can have many teachers
                .HasForeignKey(t => t.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employee>()
                .HasOne(t => t.State)
                .WithMany() // Assuming one state can have many teachers
                .HasForeignKey(t => t.StateId)
                .OnDelete(DeleteBehavior.Cascade);

            // ClubRep Configuration
            modelBuilder.Entity<ClubRep>()
                .HasOne(cr => cr.ClubName)
                .WithMany()
                .HasForeignKey(cr => cr.ClubId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as necessary

            modelBuilder.Entity<ClubRep>()
                .HasOne(cr => cr.StudentName)
                .WithMany()
                .HasForeignKey(cr => cr.StudentId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as necessary

            modelBuilder.Entity<ClubRep>()
                .HasOne(cr => cr.TeacherName)
                .WithMany()
                .HasForeignKey(cr => cr.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as necessary

            // Configure the Location-State relationship
            modelBuilder.Entity<Location>()
                .HasOne(l => l.State)
                .WithMany() // Assuming one state can have many locations
                .HasForeignKey(l => l.StateId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
