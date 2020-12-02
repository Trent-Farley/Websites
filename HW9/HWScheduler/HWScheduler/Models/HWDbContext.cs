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
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Done).HasDefaultValueSql("((0))");

                entity.Property(e => e.Duedate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("HW_FK_COURSE");
            });

            modelBuilder.Entity<HomeworkTag>(entity =>
            {
                entity.HasKey(e => new { e.HomeworkId, e.TagId })
                    .HasName("PK__Homework__DA80AD0A60DDF5E5");

                entity.Property(e => e.HomeworkId).HasColumnName("Homework_Id");

                entity.Property(e => e.TagId).HasColumnName("Tag_Id");

                entity.HasOne(d => d.Homework)
                    .WithMany(p => p.HomeworkTags)
                    .HasForeignKey(d => d.HomeworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Homework");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.HomeworkTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Tag");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Tagname).HasMaxLength(512);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
