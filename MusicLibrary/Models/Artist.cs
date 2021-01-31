using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibrary.Models
{
    public class Artist
    {
        [Column("DbId")]
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DbId { get; set; }

        [Column("Id")]        
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
