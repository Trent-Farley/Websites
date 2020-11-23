﻿using System;
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
        public virtual DbSet<Detail> Details { get; set; }
        public virtual DbSet<Homework> Homework { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:HWDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(6);
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.ToTable("Detail");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Duedate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.Property(e => e.Done).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("HW_FK_COURSE");

                entity.HasOne(d => d.Info)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.InfoId)
                    .HasConstraintName("HW_FK_DETAIL");

                entity.HasOne(d => d.Line)
                    .WithMany(p => p.Homework)
                    .HasForeignKey(d => d.LineId)
                    .HasConstraintName("HW_FK_TAG");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Tagname).HasMaxLength(40);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
