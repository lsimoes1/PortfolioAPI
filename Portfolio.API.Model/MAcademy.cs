using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.API.Model
{
    public class MAcademy
    {
        public ObjectId _id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public int Tipo { get; set; }

        public BsonDateTime DataConclusao { get; set; }

        public string Link { get; set; }

        public string Imagem { get; set; }

        public Boolean Concluido { get; set; }

        public string Linguagem { get; set; }

    }
}
