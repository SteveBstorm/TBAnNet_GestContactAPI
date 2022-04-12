using AnNet_GestContact.Dal.Entities;
using AnNet_GestContact.Dal.Mappers;
using MT.Tools.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnNet_GestContact.Dal.Services
{
    public class UserService
    {
        private readonly Connection _connection;

        public UserService(Connection connection)
        {
            _connection = connection;
        }

        public AppUser Login(string email, string password)
        {
            Command cmd = new Command("LoginUser", true);
            cmd.AddParameter("email", email);
            cmd.AddParameter("passwrd", password);

            return _connection.ExecuteReader(cmd, DataReader.ToUser).FirstOrDefault();
        }

        public AppUser GetById(int Id)
        {
            Command cmd = new Command("Select * FROM AppUser WHERE Id = @Id");
            cmd.AddParameter("Id", Id);

            return _connection.ExecuteReader(cmd, DataReader.ToUser).FirstOrDefault();
        }

        public IEnumerable<AppUser> GetAll()
        {
            Command cmd = new Command("Select * FROM AppUser");

            return _connection.ExecuteReader(cmd, DataReader.ToUser);
        }
    }
}
