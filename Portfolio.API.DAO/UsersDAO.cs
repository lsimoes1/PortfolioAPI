using MongoDB.Driver;
using Portfolio.API.Model.Security;
using System;

namespace Portfolio.API.DAO
{
    public class UsersDAO
    {
        private MongoClient mongo;

        public UsersDAO()
        {
            mongo = new MongoClient(Environment.GetEnvironmentVariable("MongoDBConn"));
        }

        public User FindByUser(string userID)
        {
            try
            {
                IMongoDatabase database = mongo.GetDatabase("dbsite");
                IMongoCollection<User> userdb = database.GetCollection<User>("security");
                var responseMongo = userdb.Find(x => x.UserID.Equals(userID)).FirstOrDefault();

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
