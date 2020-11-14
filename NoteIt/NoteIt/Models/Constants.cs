using System;
using System.Collections.Generic;
using System.Text;

namespace NoteIt.Models
{
    public static class Constants
    {
        // API key can be a shared, multi-resource key or an individual service key
        // and can be found and regenerated in the Azure portal
        public static string CognitiveServicesApiKey = "48d47c48e6ba4e0299ddf958f3581a8b";

        // Endpoint is based on your configured region, for example "westus"
        public static string CognitiveServicesRegion = "westus";
    }
}
