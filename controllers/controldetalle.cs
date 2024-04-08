using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.controllers
{
    public class controldetalle
    {
        public async static Task<models.acent> GetPosts(int id)
        {
            models.acent post = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync("https://cinepolis.ngrok.dev/api/acientos/" + id);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = await responseMessage.Content.ReadAsStringAsync();
                        post = JsonConvert.DeserializeObject<models.acent>(result);
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

            return post;
        }

        public async static Task<List<models.versanck>> Historialsnack(int id)
        {
            List<models.versanck>? posts = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync("https://cinepolis.ngrok.dev/api/snack/" + id);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = await responseMessage.Content.ReadAsStringAsync();
                        posts = JsonConvert.DeserializeObject<List<models.versanck>>(result);
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
