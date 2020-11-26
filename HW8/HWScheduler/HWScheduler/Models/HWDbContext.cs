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
        public virtual DbSet<HomeworkTag> HomeworkTags { get; set; }
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

            modelBuilder.Entity<HomeworkTag>(entity =>
            {
                entity.HasOne(d => d.Hw)
                    .WithMany()
                    .HasForeignKey(d => d.HwId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HomeworkTa__HwId__3493CFA7");

                entity.HasOne(d => d.Label)
                    .WithMany()
                    .HasForeignKey(d => d.LabelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HomeworkT__Label__3587F3E0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
