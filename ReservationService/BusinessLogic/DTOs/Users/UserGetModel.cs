using DataAccess.Data.Entities;

namespace BusinessLogic.DTOs.Users
{
    public class UserGetModel
    {
        public Guid Id { get; set; }
        public string Login {  set; get; }
        public string FullName { get; set; }
        public string Email {  set; get; }
        public DateTime DateTime { get; set; }
        public decimal Balance { get; set; }
        public string passwordHash { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }


    }
}
