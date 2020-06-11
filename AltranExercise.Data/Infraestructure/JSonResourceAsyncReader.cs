using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AltranExercise.Data.Infraestructure
{
    internal static class JSonResourceAsyncReader
    {
        public static async Task<T> GetResourceAsync<T>(string resourceUrl, HttpClient httpClient)
        {
            if (string.IsNullOrEmpty(resourceUrl))
            {
                throw new ArgumentException("Missing resource url");
            }

            if (httpClient == null)
            {
                throw new ArgumentException("Missing httClient");
            }

            var result = default(T);
            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(resourceUrl))
                {
                    using (HttpContent content = response.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        var resource = JObject.Parse(data);


                        if (data != null)
                        {
                            result = JsonConvert.DeserializeObject<T>(resource.ToString());
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("JSonResourceAsyncReader: Error reading resource", exception);
            }

            return result;
        }
    }
}
