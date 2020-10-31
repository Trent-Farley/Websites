using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HWSch.Models
{
    public partial class HWScheduleDBContext : DbContext
    {
        public HWScheduleDBContext()
        {
        }

        public HWScheduleDBContext(DbContextOptions<HWScheduleDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Homework> Homeworks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=HWSchedulerConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Homework>(entity =>
            {
                entity.Property(e => e.Fin).HasDefaultValueSql("((0))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}