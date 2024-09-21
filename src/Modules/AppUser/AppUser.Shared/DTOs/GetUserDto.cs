using UserAccess.Domain.ValueObjects;

namespace AppUser.Shared.DTOs
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
