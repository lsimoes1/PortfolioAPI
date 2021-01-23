using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Portfolio.API.Model.Security;
using System;

namespace Portfolio.API.DAO
{
    public class UsersDAO
    {
        private IConfiguration _configuration;
        private MongoClient mongo;

        public UsersDAO(IConfiguration configuration)
        {
            _configuration = configuration;
            mongo = new MongoClient(_configuration.GetConnectionString("mongodb"));
        }

        public User FindByUser(string userID)
        {
            try
            {
                IMongoDatabase database = mongo.GetDatabase("dbsite");
                IMongoCollection<User> userdb = database.GetCollection<User>("security");
                var responseMongo = userdb.Find(x => x.UserID.Equals(userID)).FirstOrDefault();
                User user = new User() { UserID = responseMongo.UserID, AccessKey = responseMongo.AccessKey };
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
