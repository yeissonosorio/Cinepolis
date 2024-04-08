using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.controllers
{
    public class ControllerPelicula
    {
        public async static Task<List<models.Peliculas>> Historial()
        {
            List<models.Peliculas>? posts = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync("https://cinepolis.ngrok.dev/api/peliculas/estado/1");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = await responseMessage.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<List<models.Peliculas>>(result);
                    }
                    else
                    {

                    }
                }
            }
            catch (HttpRequestException ex)
            {

            }
            catch (JsonException ex)
            {

            }
            catch (Exception ex)
            {

            }

            return posts;
        }
    }
}
