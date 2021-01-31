using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibrary.Helpers;
using MusicLibrary.Models;

namespace MusicLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly MusicContext _context;

        public ArtistsController(MusicContext context)
        {
            _context = context;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            return await _context.Artists.ToListAsync();
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        // PUT: api/Artists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, Artist artist)
        {
            if (id != artist.Id)
            {
                return BadRequest();
            }

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/Artists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
        }

        // POST: api/artists/LoadArtistsFromFile
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult<Artist>> LoadArtistsFromFile()
        {
            List<string> artistNames = GetExistingArtists();

            ArtistRoot artists = FileHelper.ReadArtistsFromJson();
            List<int> ids = await AddArtistsToDb(artistNames, artists);

            return CreatedAtAction(nameof(GetArtist), new
            {
                msg = $"Added {ids.Count} artists. " +
                $"{artistNames.Count} artists already in DB."
            });
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }

        private async Task<List<int>> AddArtistsToDb(List<string> artistsInDb, ArtistRoot artists)
        {
            List<int> ids = new List<int>();

            foreach (Artist artist in artists.Artists)
            {
                if (!artistsInDb.Contains(artist.Name))
                {
                    if (artist.Id == 0)
                    {
                        continue;
                    }
                    _context.Artists.Add(artist);
                    ids.Add(artist.Id);
                    artistsInDb.Add(artist.Name);
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
        private List<string> GetExistingArtists()
        {
            List<string> artistNames = new List<string>();
            List<Artist> existingArtists = _context.Artists.ToList();
            foreach (var artist in existingArtists)
            {
                artistNames.Add(artist.Name);
            }

            return artistNames;
        }
    }
}
