using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Collections.Generic;

namespace terminarz_projekt.Models
{
    public class CalendarModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int DaysInMonth { get; set; }

        public int ID { get; set; }
        public string typ_zajec { get; set; }
        public string pracownik { get; set; }
        public string dzien_data { get; set; }
        public string godzina_od { get; set; }
        public string godzina_do { get; set; }
        public string status { get; set; }

    }
}
