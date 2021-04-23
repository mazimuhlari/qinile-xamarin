using System;
using Newtonsoft.Json;

namespace Qinile.Core.Models
{
    public class BaseModel<T>
    {
        [JsonProperty("_id")]
        public virtual T Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}