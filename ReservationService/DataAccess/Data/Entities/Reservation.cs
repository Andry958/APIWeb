using DataAccess.Enum;

namespace DataAccess.Data.Entities
{
    public class Reservation : BaseEntity
    {
        public Guid Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int ResourceId { get; set; }
        public Resource? Resource { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public decimal TotalPrice { get; set; }
        public string? Notes { get; set; }


    }
}
