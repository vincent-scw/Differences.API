using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Differences.Interaction.Models
{
    public enum DataStatus
    {
        New,
        Normal,
        Deleted
    }

    public abstract class Entity
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdateTime { get; set; }
    }

    public abstract class TraceableEntity : Entity
    {
        public DataStatus Status { get; set; }
    }
}
