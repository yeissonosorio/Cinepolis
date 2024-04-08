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
        public async static Task<List<models.acientos>> GetPosts(int id,string ciudad,string fecha,string hora)
        {
            List<models.acientos>? posts = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync("https://cinepolis.ngrok.dev/api/acientos?id_pelicula=" + id+"&ciudad="+ciudad+"&fecha="+fecha+"&hora="+hora);

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
        public async static Task<models.Msg> Createacien(models.acientore acientore)
        {
            var msg = new models.Msg();

            var JsonObject = JsonConvert.SerializeObject(acientore);

            System.Net.Http.StringContent stringContent = new StringContent(JsonObject, Encoding.UTF8,
                "application/json");
            Console.WriteLine(JsonObject);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = null;
                    responseMessage = await client.PostAsync("https://cinepolis.ngrok.dev/api/acientos/store", stringContent);

                    if (responseMessage != null)
                    {
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            var resultado = responseMessage.Content.ReadAsStringAsync().Result;
                            msg = JsonConvert.DeserializeObject<models.Msg>(resultado);
                        }
                    }
                    else
                    {
                        msg.Code = "01";
                        msg.message = "No hubo respuesta del servidor";
                        return msg;
                    }
                }

                return msg;
            }
            catch (HttpRequestException ex)
            {
                msg.Code = "01";
                msg.message = "Error al enviar la solicitud HTTP: " + ex.Message;
                return msg;
            }
            catch (JsonException ex)
            {
                msg.Code = "01";
                msg.message = "Error al deserializar la respuesta JSON: " + ex.Message;
                return msg;
            }
            catch (TaskCanceledException ex)
            {
                msg.Code = "01";
                msg.message = "La tarea se canceló: " + ex.Message;
                return msg;
            }
            catch (InvalidOperationException ex)
            {
                msg.Code = "01";
                msg.message = "Operación no válida: " + ex.Message;
                return msg;
            }
            catch (Exception e)
            {
                msg.Code = "01";
                msg.message = "Error desconocido: " + e.Message;
                return msg;
            }

        }
    }
}
