using System.Data;
using AnNet_GestContact.Dal.Entities;

namespace AnNet_GestContact.Dal.Mappers
{
    internal static class DataReader
    {
        internal static Contact ToContact(this IDataReader reader)
        {
            return new Contact()
            {
                Id = (int)reader["Id"],
                LastName = (string)reader["LastName"],
                FirstName = (string)reader["FirstName"],
                Email = (string)reader["Email"],
                BirthDate = (DateTime)reader["BirthDate"]
            };
        }

        internal static AppUser ToUser(this IDataReader reader)
        {
            return new AppUser
            {
                Id = (int)reader["Id"],
                Email = (string)reader["email"],
                Nickname = (string)reader["nickname"],
                IsAdmin = (bool)reader["isAdmin"]
            };
        }
    }
}
