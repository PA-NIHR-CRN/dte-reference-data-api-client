using System.Collections.Generic;

namespace Dte.Reference.Data.Api.Client.Models
{
    public class HealthModel
    {
        public string Status { get; set; }
        public Dictionary<string, ResultsModel> Results { get; set; }
    }
    
    public class ResultsModel
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
    }
}