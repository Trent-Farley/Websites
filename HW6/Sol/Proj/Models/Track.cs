﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj.Models
{
    [Table("Track")]
    public partial class Track
    {
        public Track()
        {
            InvoiceLines = new HashSet<InvoiceLine>();
            PlaylistTracks = new HashSet<PlaylistTrack>();
        }

        [Key]
        public int TrackId { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public int? AlbumId { get; set; }
        public int MediaTypeId { get; set; }
        public int? GenreId { get; set; }
        [StringLength(220)]
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        [Column(TypeName = "numeric(10, 2)")]
        public decimal UnitPrice { get; set; }

        [ForeignKey(nameof(AlbumId))]
        [InverseProperty("Tracks")]
        public virtual Album Album { get; set; }
        [ForeignKey(nameof(GenreId))]
        [InverseProperty("Tracks")]
        public virtual Genre Genre { get; set; }
        [ForeignKey(nameof(MediaTypeId))]
        [InverseProperty("Tracks")]
        public virtual MediaType MediaType { get; set; }
        [InverseProperty(nameof(InvoiceLine.Track))]
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
        [InverseProperty(nameof(PlaylistTrack.Track))]
        public virtual ICollection<PlaylistTrack> PlaylistTracks { get; set; }
    }
}
