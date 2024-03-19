using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Microsoft.Maui.ApplicationModel.Communication;
using Newtonsoft.Json.Linq;

namespace Cinepolis
{
    public class Firebaseconecxioncs
    {
        public static FirebaseAuthClient ConectaraFirebase()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyCdPLVaXYqtDSnnu3z3HjB7DL7Qj9G_rAM",
                AuthDomain = "cinepolis-119be.firebaseapp.com",
                Providers = new FirebaseAuthProvider[] {
                    new GoogleProvider().AddScopes("email"),
                    new EmailProvider()
                },
            };
            var client = new FirebaseAuthClient(config);
            return client;
        }

        public async Task<UserCredential> CargarUsuario(string Email, string Password)
        {
            var cliente = ConectaraFirebase();
            var userCredential = await cliente.SignInWithEmailAndPasswordAsync(Email, Password);
            return userCredential;
        }

       


        private const string FirebaseApiKey = "AIzaSyCdPLVaXYqtDSnnu3z3HjB7DL7Qj9G_rAM";
        private const string RequestUri = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + FirebaseApiKey;

        public static async Task SendVerificationEmailAsync(string idToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent("{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"" + idToken + "\"}");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(RequestUri, content);
                response.EnsureSuccessStatusCode();
            }
        }

        // Método para enviar un correo de recuperación de contraseña

        public async void enviar(string email)
        {
           await SendPasswordResetEmail(email);
        }

        public static async Task SendPasswordResetEmail(string email)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent("{\"email\":\"" + email + "\",\"requestType\":\"PASSWORD_RESET\"}");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + FirebaseApiKey, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí según tus necesidades
                Console.WriteLine("Error al enviar correo de recuperación de contraseña: " + ex.Message);
            }
        }
    }
}
