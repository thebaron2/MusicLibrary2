using MusicLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibrary.Helpers
{
    public class FileHelper
    {
        public static List<Song> ReadSongsFromJson()
        {
            var jsongString = File.ReadAllText(@"c:\temp\songs.json");
            List<Song> songs = JsonConvert.DeserializeObject<List<Song>>(jsongString);
            return songs;
        }

        public static List<Artist> ReadArtistsFromJson()
        {
            var jsongString = File.ReadAllText(@"c:\temp\artists.json");
            List<Artist> artists = JsonConvert.DeserializeObject<List<Artist>>(jsongString);
            return artists;
        }


    }
}
