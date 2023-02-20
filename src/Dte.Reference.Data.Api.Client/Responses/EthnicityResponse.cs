using System.Collections.Generic;

namespace Dte.Reference.Data.Api.Client.Responses
{
    public class EthnicityResponse
    {
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public List<string> Backgrounds { get; set; }
    }
}