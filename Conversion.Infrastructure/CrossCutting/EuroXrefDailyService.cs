using Conversion.Infrastructure.CrossCutting.Model;
using Microsoft.AspNetCore.Http;
using System.Xml.Serialization;

namespace Conversion.Infrastructure.CrossCutting
{
    public class EuroXrefDailyService
    {
#pragma warning disable S1075 // URIs should not be hardcoded
        private const string _uri = "https://www.ecb.europa.eu";
#pragma warning restore S1075 // URIs should not be hardcoded

        public async Task<Envelope?> Get()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_uri);

                var response = await httpClient.GetAsync("/stats/eurofxref/eurofxref-daily.xml");

                if (response.IsSuccessStatusCode)
                {
                    var xmlContent = await response.Content.ReadAsStringAsync();

                    Envelope? envelope;

                    using (StringReader stringReader = new StringReader(xmlContent))
                    {
                        var serializer = new XmlSerializer(typeof(Envelope));
                        envelope = serializer.Deserialize(stringReader) as Envelope;
                    }

                    return envelope;
                }
                else
                {
                    throw new BadHttpRequestException($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
        }
    }
}
