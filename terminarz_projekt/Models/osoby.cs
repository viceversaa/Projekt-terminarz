namespace terminarz_projekt.Models
{
    public class Osoby
    {
        public int ID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }    
        public string drugie_imie { get; set; }
        public int plec { get; set; }
        public DateOnly data_urodzenia { get; set; }
        public int nr_telefonu { get; set; }
        public string email { get; set; }
        public string Hasło { get; set; }
    }
}
