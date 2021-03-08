using Microsoft.VisualBasic.CompilerServices;
using MongoDB.Driver;
using Portfolio.API.DAO.Interface;
using Portfolio.API.Model;
using System;
using System.Collections.Generic;

namespace Portfolio.API.DAO
{
    public class AcademyDAO : IAcademyDAO
    {
        private MongoClient mongo;
        public AcademyDAO()
        {
            mongo = new MongoClient(Environment.GetEnvironmentVariable("MongoDBConn"));
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
    }
}
