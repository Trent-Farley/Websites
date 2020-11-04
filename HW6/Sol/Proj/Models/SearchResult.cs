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
        public string Search { get; set; } = null;

        public IEnumerable<Album> AlbumResult { get; set; } = null;
    }
}