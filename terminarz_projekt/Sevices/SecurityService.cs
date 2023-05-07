using terminarz_projekt.Models;

namespace terminarz_projekt.Sevices
{
    public class SecurityService
    {
        List<Osoby> knownUsers = new List<Osoby>();

        public SecurityService() {

          /// knownUsers.Add(new UserModel { NazwaUżytkownika = "Bill", Hasło = "bill"});
           

        }

        public bool IsValid(Osoby osoby)
        {
            return knownUsers.Any(x => x.email == osoby.email && x.Hasło == osoby.Hasło);
        }
    }
}
