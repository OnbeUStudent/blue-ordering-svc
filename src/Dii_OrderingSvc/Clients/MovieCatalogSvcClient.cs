using Dii_OrderingSvc.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dii_OrderingSvc.Clients
{
    public class MovieCatalogSvcClient
    {
        private readonly HttpClient httpClient;

        public MovieCatalogSvcClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Movie> GetMovie(string id)
        {
            if (Guid.TryParse(id, out Guid movieIdAsGuid))
            {

                var movie = await httpClient.GetStringAsync($"api/movies/{movieIdAsGuid.ToString()}");
                if (!string.IsNullOrWhiteSpace(movie))
                {
                    return JsonConvert.DeserializeObject<Movie>(movie);
                }
            }
            return null;
        }
    }
}
