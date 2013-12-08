using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Action.Model
{
    public class BsonClass
    {
        [BsonId]
        public ObjectId OID { get; private set; }

        public int Version { get; set; }
    }
}
