using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnsekapiTestSuite.ResponseModels
{
    public class EnergyData
    {
        [JsonPropertyName("electric")]
        public EnergyType Electric { get; set; }

        [JsonPropertyName("gas")]
        public EnergyType Gas { get; set; }

        [JsonPropertyName("nuclear")]
        public EnergyType Nuclear { get; set; }

        [JsonPropertyName("oil")]
        public EnergyType Oil { get; set; }
    }

    public class EnergyType
    {
        [JsonPropertyName("energy_id")]
        public int EnergyId { get; set; }

        [JsonPropertyName("price_per_unit")]
        public double PricePerUnit { get; set; }

        [JsonPropertyName("quantity_of_units")]
        public int QuantityOfUnits { get; set; }

        [JsonPropertyName("unit_type")]
        public string UnitType { get; set; }
    }

    public enum  EnergyTypeEnum 
    {
        Gas,
        Electric,
        Nuclear,
        Oil
    }
}
