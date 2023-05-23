namespace terminarz_projekt.Data
{
    public class ConnectDb
    {
        private string _connString;

        public ConnectDb(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("connString");
        }

      /*  public List<uzytkownicy> GetUsers()
        {
            var users = new List<uzytkownicy>();
        }*/
    }
}
