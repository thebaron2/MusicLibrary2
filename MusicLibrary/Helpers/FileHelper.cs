using MusicLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusicLibrary.Helpers
{
    public class FileHelper
    {
        public static SongRoot ReadSongsFromJson()
        {
            var jsongString = File.ReadAllText(@"c:\temp\songs.json");
            SongRoot songs = JsonSerializer.Deserialize<SongRoot>(jsongString);
            return songs;
        }

        public static ArtistRoot ReadArtistsFromJson()
        {
            var jsongString = File.ReadAllText(@"c:\temp\artists.json");
            ArtistRoot artists = JsonSerializer.Deserialize<ArtistRoot>(jsongString);
            return artists;
        }


    }
}
