using Microsoft.AspNetCore.Identity;

namespace DataAccess.Data.Entities
{
    public class User : IdentityUser
    {
        public string Login { get; set; } = string.Empty;
        public string? FullName { get; set; } = "";
        public DateTime? Birthdate { get; set; }
        public decimal Balance { get; set; } = 0;
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        //public ICollection<Order>? Orders { get; set; }
    }
}
