using AnNet_GestContact.Dal;
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
    public class ContactService
    {
        private readonly Connection _connection;

        public ContactService(Connection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Contact> Get()
        {
            Command command = new Command("Select Id, LastName, FirstName, Email, BirthDate FROM Contact;");
            return _connection.ExecuteReader(command, dr => dr.ToContact());
        }

        public Contact? Get(int id)
        {
            Command command = new Command("Select Id, LastName, FirstName, Email, BirthDate FROM Contact WHERE Id = @Id;");
            command.AddParameter("id", id);

            return _connection.ExecuteReader(command, dr => dr.ToContact()).SingleOrDefault();
        }

        public int Insert(Contact entity)
        {
            Command command = new Command("Insert Into Contact (LastName, FirstName, Email, BirthDate) OUTPUT Inserted.Id Values (@LastName, @FirstName, @Email, @BirthDate);");
            command.AddParameter("LastName", entity.LastName);
            command.AddParameter("FirstName", entity.FirstName);
            command.AddParameter("Email", entity.Email);
            command.AddParameter("BirthDate", entity.BirthDate);

            int? id = (int?)_connection.ExecuteScalar(command);

            if(!id.HasValue)
                throw new OperationCanceledException("Something wrong with database...");

            return id.Value;
        }

        public bool Update(Contact entity)
        {
            Command command = new Command("Update Contact Set LastName = @LastName, FirstName = @FirstName, Email = @Email, BirthDate = @BirthDate WHERE Id = @Id");
            command.AddParameter("Id", entity.Id);
            command.AddParameter("LastName", entity.LastName);
            command.AddParameter("FirstName", entity.FirstName);
            command.AddParameter("Email", entity.Email);
            command.AddParameter("BirthDate", entity.BirthDate);

            return _connection.ExecuteNonQuery(command) == 1;
        }

        public Contact? Delete(int id)
        {
            Command command = new Command("Delete From Contact OUTPUT Deleted.* WHERE Id = @Id");
            command.AddParameter("Id", id);

            return _connection.ExecuteReader(command, dr => dr.ToContact()).SingleOrDefault();
        }
    }
}
