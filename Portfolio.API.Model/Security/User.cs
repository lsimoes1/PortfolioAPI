using MongoDB.Bson;

namespace Portfolio.API.Model.Security
{
    public class User
    {
        public string UserID { get; set; }
        public string AccessKey { get; set; }
    }

    public class UserMongoDB
    {
        public ObjectId _id { get; set; }
        public string UserID { get; set; }
        public string AccessKey { get; set; }
    }
}
