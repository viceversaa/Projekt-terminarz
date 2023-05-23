namespace terminarz_projekt.Models
{
    /// <summary>
    /// Model opisujacy osoby. Zawiera podstawowe informacje o uzytkownikach.
    /// </summary>
    public class Osoby
    {
        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }    
        public string drugie_imie { get; set; }
        public string plec { get; set; }
        public string data_urodzenia { get; set; }
        public int nr_telefonu { get; set; }
        public string email { get; set; }
        public string Hasło { get; set; }
    }
}
