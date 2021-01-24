using MongoDB.Driver;
using Portfolio.API.Model;
using System;
using System.Collections.Generic;

namespace Portfolio.API.DAO
{
    public class AcademyDAO
    {
        private MongoClient mongo;
        public AcademyDAO()
        {
            mongo = new MongoClient("mongodb://lsimoes:5826L0492*Cdb@cluster0-shard-00-00.jliuf.mongodb.net:27017,cluster0-shard-00-01.jliuf.mongodb.net:27017,cluster0-shard-00-02.jliuf.mongodb.net:27017/dbportfoliosite?ssl=true&replicaSet=atlas-edysej-shard-0&authSource=admin&retryWrites=true&w=majority");
        }

        public List<MAcademy> FindAllAcademyProjects()
        {
            try
            {
                IMongoDatabase database = mongo.GetDatabase("dbsite");
                IMongoCollection<MAcademy> academyDb = database.GetCollection<MAcademy>("academy");
                var responseMongo = academyDb.Find(Builders<MAcademy>.Filter.Empty).ToListAsync().Result;

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

        public object FindByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
