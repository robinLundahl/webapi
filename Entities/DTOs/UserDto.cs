using System.ComponentModel.DataAnnotations;

namespace Entitites.DTOs.User;

public class User
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string? QRCode { get; set; }

}