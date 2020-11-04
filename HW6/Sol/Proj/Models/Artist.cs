using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proj.Models
{
    [Table("Artist")]
    public partial class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        [Key]
        public int ArtistId { get; set; }
        [StringLength(120)]
        public string Name { get; set; }

        [InverseProperty(nameof(Album.Artist))]
        public virtual ICollection<Album> Albums { get; set; }
    }
}
