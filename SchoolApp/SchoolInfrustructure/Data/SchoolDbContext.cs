using Microsoft.EntityFrameworkCore;
using SchoolDomain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolInfrustructure.Data
{
    public class SchoolDbContext : DbContext
    {
        //DbContextOptions არის ობიექტი, რომელიც EF Core-ს ეუბნება, როგორ უნდა იმუშაოს მონაცემთა ბაზასთან.
        // DbContextOptions საშუალებას გვაძლევს სხვადასხვა გარემოში  გამოვიყენოთ სხვადასხვა მონაცემთა ბაზა.
        // Unit Testing-ის დროს InMemory Database-ს გამოვიყენებთ
        // პლიკაციის მუშაობისას SQL Server.
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        //ცხრილები
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentProfile>()
                .HasOne(sp => sp.Student) // ყავს 1 სტუდენტი
                .WithOne(s => s.StudentProfile) // არის სტუდენტის 1 პროფაილი(ანუ სტუდენტს აქვს 1 პროფაილი)
                .HasForeignKey<StudentProfile>(sp => sp.StudentId);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Teacher)
                .WithMany(t => t.Subjects)
                .HasForeignKey(s => s.TeacherId);

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Subjects)
                .WithOne(s => s.Teacher)
                .HasForeignKey(s => s.TeacherId);

            //junction - Enrolment
            modelBuilder.Entity<Enrolment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrolments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade); // წასშლის საშუალებას ვაძლევთ მარა მიკავშრებულ ტიპებსაც წაიღებს თუ წაიშალა
            modelBuilder.Entity<Enrolment>()
                .HasOne(e => e.Subject)
                .WithMany(s => s.Enrolments)
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
            //requirments
            // ეს ვალიდაციები მოქმედებს მონაცემთა ბაზებზე
            //სეტ-ში რა ვალიდაციასაც გავწერთ მაგას მიგრაცია არ წაიღებს
            //ამიტომ ეს "ვალიდაციები" დტო-ში უეჭ მჭრდება 
            modelBuilder.Entity<Student>(entity =>
            {
                //entity.HasKey(e => e.Id); not needed 
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DateOfBirth).IsRequired();
            });
        }
    }
}
