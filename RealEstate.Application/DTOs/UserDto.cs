using Microsoft.AspNetCore.Http;
namespace RealEstate.Application.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsVerified { get; set; }
        public string PhoneNumber { get; set; }
        public string? ProfilePicturePath { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
}