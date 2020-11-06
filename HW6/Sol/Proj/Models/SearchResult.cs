using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Proj.Models
{
    public class SearchResult
    {
        [Required]
        [MinLength(2)]
        public string Search { get; set; }

        public int ArtistId { get; set; }
        public IEnumerable<Artist> ArtistResult { get; set; } = null;
    }
}