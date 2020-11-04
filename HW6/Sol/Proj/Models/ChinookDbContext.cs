using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Proj.Models
{
    public partial class ChinookDbContext : DbContext
    {
        public ChinookDbContext()
        {
        }

        public ChinookDbContext(DbContextOptions<ChinookDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<PlaylistTrack> PlaylistTracks { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ChinookConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasIndex(e => e.ArtistId)
                    .HasName("IFK_AlbumArtistId");

                entity.Property(e => e.AlbumId).ValueGeneratedNever();

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlbumArtistId");
            });

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(e => e.ArtistId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.SupportRepId)
                    .HasName("IFK_CustomerSupportRepId");

                entity.Property(e => e.CustomerId).ValueGeneratedNever();

                entity.HasOne(d => d.SupportRep)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.SupportRepId)
                    .HasConstraintName("FK_CustomerSupportRepId");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.ReportsTo)
                    .HasName("IFK_EmployeeReportsTo");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo)
                    .HasConstraintName("FK_EmployeeReportsTo");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasIndex(e => e.CustomerId)
                    .HasName("IFK_InvoiceCustomerId");

                entity.Property(e => e.InvoiceId).ValueGeneratedNever();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceCustomerId");
            });

            modelBuilder.Entity<InvoiceLine>(entity =>
            {
                entity.HasIndex(e => e.InvoiceId)
                    .HasName("IFK_InvoiceLineInvoiceId");

                entity.HasIndex(e => e.TrackId)
                    .HasName("IFK_InvoiceLineTrackId");

                entity.Property(e => e.InvoiceLineId).ValueGeneratedNever();

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceLines)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceLineInvoiceId");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.InvoiceLines)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceLineTrackId");
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.Property(e => e.MediaTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.Property(e => e.PlaylistId).ValueGeneratedNever();
            });

            modelBuilder.Entity<PlaylistTrack>(entity =>
            {
                entity.HasKey(e => new { e.PlaylistId, e.TrackId })
                    .IsClustered(false);

                entity.HasIndex(e => e.TrackId)
                    .HasName("IFK_PlaylistTrackTrackId");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.PlaylistTracks)
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaylistTrackPlaylistId");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.PlaylistTracks)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaylistTrackTrackId");
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.HasIndex(e => e.AlbumId)
                    .HasName("IFK_TrackAlbumId");

                entity.HasIndex(e => e.GenreId)
                    .HasName("IFK_TrackGenreId");

                entity.HasIndex(e => e.MediaTypeId)
                    .HasName("IFK_TrackMediaTypeId");

                entity.Property(e => e.TrackId).ValueGeneratedNever();

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.AlbumId)
                    .HasConstraintName("FK_TrackAlbumId");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_TrackGenreId");

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Tracks)
                    .HasForeignKey(d => d.MediaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrackMediaTypeId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
