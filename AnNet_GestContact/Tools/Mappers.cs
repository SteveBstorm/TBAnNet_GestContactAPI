using DAL = AnNet_GestContact.Dal.Entities;
using API = AnNet_GestContact.Models;

namespace AnNet_GestContact.Tools
{
    public static class Mappers
    {
        public static API.AppUser ToApi(this DAL.AppUser user)
        {
            if(user is null) { throw new ArgumentNullException("Utilisateur inéxistant"); }
            return new API.AppUser
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Nickname = user.Nickname
            };
        }
    }
}
