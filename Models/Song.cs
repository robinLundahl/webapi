using System.ComponentModel.DataAnnotations;

namespace webapi.models;

public class Song
{
    public int Id { get; set; }
    [Required]
    public string SongName { get; set; }
    [Required]
    public string ArtistName { get; set; }
    public string? Comment { get; set; }
    [Required]
    public string Genre { get; set; }
}