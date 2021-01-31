using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibrary.Models
{
    public class Song
    {
        [Column("Id")]
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Year")]
        public int Year { get; set; }

        [Column("Artist")]
        public string Artist { get; set; }

        [Column("ShortName")]
        public string ShortName { get; set; }

        [Column("Bpm")]
        public int? Bpm { get; set; }

        [Column("Duration")]
        public int? Duration { get; set; } // milliseconds

        [Column("Genre")]
        public string Genre { get; set; }

        [Column("SpotifyId")]
        public string SpotifyId { get; set; }

        [Column("Album")]
        public string Album { get; set; }
    }
}
