using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public  class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Email { get; set; }

        public DateRange(DateTime startDate, DateTime endDate,string email)
        {
            StartDate = startDate;
            EndDate = endDate;
            Email = email;
        }
    }
}
