using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Models;
using MusicLibrary.Helpers;
using Microsoft.Extensions.Configuration;

namespace MusicLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly MusicContext _context;

        public SongsController(MusicContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // GET: api/Songs/GetGenre/Metal
        [Route("[action]/{genre}")]
        [HttpGet]
        public async Task<IEnumerable<Song>> GetGenre(string genre)
        {
            var songs = _context.Songs.FromSqlInterpolated(
                $"SELECT * FROM dbo.Songs WHERE Genre = ({genre})").ToList();

            return songs;


            //return _context.Songs.FromSqlInterpolated(
            //    $"SELECT * FROM dbo.Songs WHERE Genre = ({genre})");
        }

        // GET: api/Songs/GetBefore/2016
        [Route("[action]/{year}")]
        [HttpGet]
        public async Task<IEnumerable<Song>> GetBefore(int year)
        {
            var songs = _context.Songs.FromSqlInterpolated(
                $"SELECT * FROM dbo.Songs WHERE Year < ({year})").ToList();
            return songs;
        }

        // GET: api/Songs/GetGenreBefore/Metal/2016
        [Route("[action]/{genre}/{year}")]
        [HttpGet]
        public async Task<IEnumerable<Song>> GetGenreBefore(string genre, int year)
        {
            var songs = _context.Songs.FromSqlInterpolated(
                @$"SELECT * FROM dbo.Songs 
                   WHERE genre = ({genre}) AND Year < ({year})"
                ).ToList();
            return songs;
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song song)
        {
            if (id != song.Id)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSong), new { id = song.Id }, song);
        }

        // POST: api/Songs/LoadSongsFromFile
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Song>> LoadSongsFromFile()
        {
            List<string> songNames = GetExistingSongs();

            SongRoot songs = FileHelper.ReadSongsFromJson();
            List<int> ids = await AddSongsToDb(songNames, songs);

            return CreatedAtAction(nameof(GetSongs), new 
            { 
                msg = $"Added {ids.Count} songs. " +
                $"{songNames.Count} songs already in DB." 
            });
        }


        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.Id == id);
        }

        private async Task<List<int>> AddSongsToDb(List<string> songsInDb, SongRoot songs)
        {
            List<int> ids = new List<int>();

            foreach (Song song in songs.Songs)
            {
                if (!songsInDb.Contains(song.Name))
                {
                    _context.Songs.Add(song);
                    ids.Add(song.Id);
                    songsInDb.Add(song.Name);
                }
            }
            await _context.SaveChangesAsync();
            return ids;
        }

        /// <summary>
        /// Retrieve songs that are already in the database.
        /// Workaround, couldnt call get all songs because of loop issue.
        /// </summary>
        /// <returns></returns>
        private List<string> GetExistingSongs()
        {
            List<string> songNames = new List<string>();
            List<Song> existingSongs = _context.Songs.ToList();
            foreach (Song song in existingSongs)
            {
                songNames.Add(song.Name);
            }

            return songNames;
        }
    }
}
