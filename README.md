## MusicLibrary

Used a docker container to run a local MS SQL server with following command:
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<your_strong_password>" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```
Change connection string in appsettings.json to refer to your local database.
Connectionstring should similair to this:
```
"ConnectionStrings": {
  "Data Source=localhost,<your_port>;Initial Catalog=<your_db_name>;User ID=sa;Password=<your_strong_password>"
}
```
Make sure to adjust file location inside FileHelper to the location of your json files.
Json files are located in the root of this repo.

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
- The post method LoadArtistsFromFile throws the following error: `InvalidOperationException: No route matches the supplied value.`. This error doesnt have a workaround or fix yet. The data is still inserted into the database however.
