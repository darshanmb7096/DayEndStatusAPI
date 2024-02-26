using System.Text.Json.Serialization;

namespace DayEndStatusAPI.Dtos
{
    public class StatusMessageDTO
    {

        [JsonPropertyName("MiningStatus")]
        public string MiningStatus { get; set; }


        [JsonPropertyName("ReportingStatus")]
        public string ReportingStatus { get; set; }
    }
}
