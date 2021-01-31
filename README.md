## MusicLibrary

Used a docker container to run a local MS SQL server with following command:
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<your_strong_password>" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```
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
- When running the post method LoadSongsFromFile/LoadArtistsFromFile, a error can occur when trying to insert an ID.
To fix this, remove the `IDENTITY (1,1)` from the table definition query.
This used to fix this issue, but as of 31-01-2021, opening the table definition within visual studio is no longer working.
The same error as described [here][1] occurs.
- The post method LoadArtistsFromFile throws the following error: `InvalidOperationException: No route matches the supplied value.`. This error doesnt have a workaround or fix yet. Not sure if this still happens, since the error on inserting ID gets thrown first now.

[1]: https://stackoverflow.com/questions/64824124/ssrs-member-not-found-exception-from-hresult-0x80020003-disp-e-membernotfound
