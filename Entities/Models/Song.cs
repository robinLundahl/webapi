using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Song
{
    public Guid Id { get; set; }
    [Required]
    public string SongName { get; set; }
    [Required]
    public string ArtistName { get; set; }
    public string? Comment { get; set; }
    [Required]
    public string? Genre { get; set; }
}