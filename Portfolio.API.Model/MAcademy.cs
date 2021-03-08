using MongoDB.Bson;
using System;

namespace Portfolio.API.Model
{
    public class MAcademy
    {
        public ObjectId _id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public int Tipo { get; set; }

        public dynamic DataConclusao { get; set; }

        public string Link { get; set; }

        public string Imagem { get; set; }

        public Boolean Concluido { get; set; }

        public string Linguagem { get; set; }

    }
}
