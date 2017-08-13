using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Differences.Interaction.Models
{
    public abstract class AggregateRoot : Entity
    {
        [BsonId]
        public string Id { get; set; }
    }
}