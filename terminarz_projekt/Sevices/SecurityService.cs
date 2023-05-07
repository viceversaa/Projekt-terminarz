using terminarz_projekt.Models;

namespace terminarz_projekt.Sevices
{
    public class SecurityService
    {
        List<UserModel> knownUsers = new List<UserModel>();

        public SecurityService() {

           knownUsers.Add(new UserModel { NazwaUżytkownika = "Bill", Hasło = "bill"});
           

        }

        public bool IsValid(UserModel user)
        {
            return knownUsers.Any(x => x.NazwaUżytkownika == user.NazwaUżytkownika && x.Hasło == user.Hasło);
        }
    }
}
