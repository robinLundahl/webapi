### Skapa ny migration

1. Radera Migration mappen och databasfilerna
2. Kör kommandot "dotnet ef migrations add InitialCreate"
3. Kör kommandot "dotnet ef database update"

<!-- För att returnera CreatedAtAction så måste man sätta   [HttpGet("song/{id:int}")]
                                                            [ActionName("GetSongByIdAsync")]
på den metoden som är inuti (nameof)-->

return CreatedAtAction(
nameof(GetSongByIdAsync),
new { id = createdSong.Id },
createdSong
);
