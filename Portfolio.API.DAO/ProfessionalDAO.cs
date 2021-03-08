using MongoDB.Driver;
using Portfolio.API.DAO.Interface;
using Portfolio.API.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.API.DAO
{
    public class ProfessionalDAO : IProfessionalDAO
    {
        private MongoClient mongo;

        public ProfessionalDAO()
        {
            mongo = new MongoClient(Environment.GetEnvironmentVariable("MongoDBConn"));
        }

        public List<MProfessional> FindProfessionalInfo()
        {
            try
            {
                IMongoDatabase database = mongo.GetDatabase("dbsite");
                IMongoCollection<MProfessional> academyDb = database.GetCollection<MProfessional>("professional");
                var responseMongo = academyDb.Find(Builders<MProfessional>.Filter.Empty).ToListAsync().Result;

                if (responseMongo == null)
                {
                    return null;
                }

                return responseMongo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
