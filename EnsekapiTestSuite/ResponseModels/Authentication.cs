using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnsekBddTests.ResponseModels
{
    public class Authentication
    {
        [JsonPropertyName("access_token")]
        public string AuthToken { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
