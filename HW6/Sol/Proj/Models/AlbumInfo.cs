using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Models
{
    public class AlbumInfo
    {
        public IEnumerable<Album> AlbumsTracks { get; set; }
        public string ArtistName { get; set; }
    }
}