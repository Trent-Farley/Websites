using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HWScheduler.Models
{
    public partial class HWDbContext : DbContext
    {
        public HWDbContext()
        {
        }

        public HWDbContext(DbContextOptions<HWDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Homework> Homework { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=HWDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Homework>(entity =>
            {
                entity.Property(e => e.Done).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("HW_FK_COURSE");

                entity.HasOne(d => d.Line)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.LineId)
                    .HasConstraintName("HW_FK_TAG");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
