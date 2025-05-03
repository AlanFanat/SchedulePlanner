using Microsoft.EntityFrameworkCore;
using SchedulePlanner.Db.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulePlanner.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<LessonTask> LessonTasks { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Lesson -> Period
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Period)
                .WithMany(p => p.Lessons)
                .HasForeignKey(l => l.PeriodId)
                .OnDelete(DeleteBehavior.Cascade);

            // Subject -> Period
            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Period)
                .WithMany(p => p.Subjects)
                .HasForeignKey(s => s.PeriodId)
                .OnDelete(DeleteBehavior.Cascade);

            // Teacher -> Period
            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Period)
                .WithMany(p => p.Teachers)
                .HasForeignKey(t => t.PeriodId)
                .OnDelete(DeleteBehavior.Cascade);

            //Lesson - Subject
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Subject)         // Урок имеет один предмет
                .WithMany(s => s.Lessons)        // Предмет имеет много уроков
                .HasForeignKey(l => l.SubjectId) // FK в таблице Lesson
                .OnDelete(DeleteBehavior.Restrict); // Или Cascade, если нужно

            //LessonTask - Subject
            modelBuilder.Entity<LessonTask>()
                .HasOne(l => l.Subject)         // Урок имеет один предмет
                .WithMany(s => s.LessonTasks)        // Предмет имеет много уроков
                .HasForeignKey(l => l.SubjectId) // FK в таблице Lesson
                .OnDelete(DeleteBehavior.Restrict); // Или Cascade, если нужно

            //Lesson - Teacher
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Teacher)         // Урок имеет один предмет
                .WithMany(s => s.Lessons)        // Предмет имеет много уроков
                .HasForeignKey(l => l.TeacherId) // FK в таблице Lesson
                .OnDelete(DeleteBehavior.Restrict); // Или Cascade, если нужно

            
        }
    }
}
