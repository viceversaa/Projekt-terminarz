namespace terminarz_projekt.Models
{
    /// <summary>
    /// Model zawierajacy informacje o dostepnych koniach w stajni.
    /// </summary>
    public class Konie
    {
        public int ID { get; set; }
        public string imie { get; set; }
        public string typ_konia { get; set; }
        public string wytrzymalosc { get; set; }
        public string stopien_zaawansowania { get; set; }

    }
}
