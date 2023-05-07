using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace terminarz_projekt.Models
{
    public class CalendarModel : Controller
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int DaysInMonth { get; set; }
    }
}
