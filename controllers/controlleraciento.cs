using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Cinepolis.controllers
{
    public class controlleraciento
    {
        public async static Task<List<models.acientos>> GetPosts()
        {
            List<models.acientos>? posts = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync("https://8696-170-83-119-111.ngrok-free.app/api/acientos?id_pelicula=1&ciudad=sps&fecha=2024-03-01&hora=7");

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = await responseMessage.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<List<models.acientos>>(result);
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
