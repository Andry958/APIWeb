using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Enum
{
    public enum BookingStatus
    {
        Pending,     // Очікує підтвердження
        Confirmed,   // Підтверджено
        Cancelled,   // Скасовано
        Completed    // Завершено
    }
}
