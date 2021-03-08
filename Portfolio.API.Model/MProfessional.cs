using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Portfolio.API.Model
{
    public class MProfessional
    {
        public ObjectId _id { get; set; }

        public string Company { get; set; }

        public string Link { get; set; }

        public string Image { get; set; }

        public List<OfficeViewModel> Offices { get; set; }
    }

    public class OfficeViewModel 
    {

        public string Office { get; set; }

        public dynamic DataIn { get; set; }

        public dynamic DateOut { get; set; }

        public string Description { get; set; }
    }

}
