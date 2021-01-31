## MusicLibrary

Used a docker container to run a local MS SQL server.
Change connection string in appsettings.json to refer to your local database.
Make sure to adjust file location inside FileHelper to the location of your json files.

Unittests have not been written yet, but a project has been prepared for them in advance.

### Supported HTTP methods:
#### Get:
- Get all songs/artists in DB (GetSongs, `api/Songs`/`api/Artists`)
- Get song/artist by ID (GetSong, `api/Songs/5`/`api/Artists/5`)
- Get songs by genre (GetGenre, `api/Songs/GetGenre/Metal`)
- Get songs released before year x (GetBefore, `api/Songs/GetBefore/2016`)
- Get songs by genre and released before year x (GetGenreBefore, `api/Songs/GetGenreBefore/Metal/2016`)
#### Put/Post:
- Update song/artist (PutSong/PutArtist, `api/Songs/5`/`api/Artists/5`, when using postman, insert changes into the body)
- Insert song/artist (PostSong/PostArtist, `api/Songs`/`api/Artists`, when using postman, insert changes into the body)
- Insert song/artists from file (LoadSongsFromFile/LoadArtistsFromFile, `api/Songs/LoadSongsFromFile`/`api/Artists/LoadArtistsFromFile`)
#### Delete:
- Delete song/artist by ID (DeleteSong/DeleteArtist, `api/Songs/5`/`api/Artists/5`)



### Known Issues:
- When running the post method LoadSongsFromFile, a possible error can occur when trying to insert an ID.
To fix this, remove the `IDENTITY (1,1)` from the Songs table create query.
- The post method LoadArtistsFromFile throws the following error: `InvalidOperationException: No route matches the supplied value.`. This error doesnt have a workaround or fix yet.


