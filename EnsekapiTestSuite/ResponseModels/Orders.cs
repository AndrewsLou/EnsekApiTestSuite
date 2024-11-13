using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnsekBddTests.ResponseModels
{
    public class Orders
    {
        [JsonPropertyName("fuel")]
        public string Fuel { get; set; }
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
    }
}
