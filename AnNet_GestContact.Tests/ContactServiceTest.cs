using AnNet_GestContact.Dal.Entities;
using AnNet_GestContact.Dal.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MT.Tools.Database;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace AnNet_GestContact.Tests
{
    [TestClass]
    public class ContactServiceTest
    {
        private const string CONNECTION_STRING = @"Data Source=AW-BRIAREOS\SQL2019DEV;Initial Catalog=AnNet_GestContact;Integrated Security=True;";
        private ContactService _contactService;
        
        [TestInitialize]
        public void Initialize()
        {
            Connection connection = new Connection(CONNECTION_STRING, SqlClientFactory.Instance);
            _contactService = new ContactService(connection);
        }

        [TestMethod]
        public void TestGet()
        {
            IEnumerable<Contact> contacts = _contactService.Get();
            Assert.AreEqual(contacts.Count(), 2);
        }
    }
}