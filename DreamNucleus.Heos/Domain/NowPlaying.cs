using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamNucleus.Heos.Domain
{
    public class NowPlaying
    {
        public string Type { get; set; }
        public string Song { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string ImageUrl { get; set; }
        public string Mid { get; set; }
        public string Qid { get; set; }
        public int? Sid { get; set; }
        public string AlbumId { get; set; }
    }
}
