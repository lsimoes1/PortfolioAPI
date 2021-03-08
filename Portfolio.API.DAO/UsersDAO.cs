using MongoDB.Driver;
using Portfolio.API.Model.Security;
using System;

namespace Portfolio.API.DAO
{
    public class UsersDAO
    {
        private IMongoClient mongo;
        private IMongoDatabase _database;
        private IMongoCollection<User> _userdb;

        public UsersDAO()
        {
            mongo = new MongoClient(Environment.GetEnvironmentVariable("MongoDBConn"));
            _database = mongo.GetDatabase("dbsite");
            _userdb = _database.GetCollection<User>("security");
        }

        public User FindByUser(string userID)
        {
            try
            {
                var responseMongo = _userdb.Find(x => x.UserID.Equals(userID)).FirstOrDefault();

                if (responseMongo == null)
                {
                    return null;
                }

                return new User() { UserID = responseMongo.UserID, AccessKey = responseMongo.AccessKey };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
