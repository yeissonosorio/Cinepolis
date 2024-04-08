using Newtonsoft.Json.Linq;
using System.Net;
using Cinepolis.models;
using QRCoder;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
namespace Cinepolis.Views;

public partial class PaymenState : ContentPage
{
    public string payerlink;
    public string Status { get; private set; }
    public PaymenState(string SelfLink, string Texto, string PayerLink)
    {
        InitializeComponent();
        PayPalView.Source = PayerLink;
        VerificarPago(SelfLink, Texto);
        payerlink = PayerLink;

    }


    public async void VerificarPago(string selfLink, string Texto)
    {
        try
        {
            bool completed = false;
            while (!completed)
            {
                selfLink = selfLink;
                HttpWebRequest selfRequest = (HttpWebRequest)WebRequest.Create(selfLink);
                selfRequest.Method = "GET";
                selfRequest.Headers.Add("Authorization", "Basic QVVuTmV2OWYxc1pUc2hNNDcyNkhRRzREQ05Jems0S0N1WWhtUGpiOEJPQkh2U0FPd09GdEhyV0wtbG5QZUxyTUZOR3pnWmY3V29xdlVZRTg6RUU2b1ZoQzRzSHlGR2plZU04YjRIS1VFRXVPSUFBYTVRbnFxbjhWOGVZb0xDczFYbF84TjMzWGFoRks3MXJmTmZQZ1c2dERXWkdLRWczNHY=");
                selfRequest.ContentType = "application/json";

                HttpWebResponse selfResponse = (HttpWebResponse)await selfRequest.GetResponseAsync();
                using (StreamReader reader = new StreamReader(selfResponse.GetResponseStream()))
                {
                    string selfResult = await reader.ReadToEndAsync();
                    JObject selfJsonResult = JObject.Parse(selfResult);
                    string status = selfJsonResult["status"].ToString();
                    if (status == "APPROVED")
                    {
                        Status = status;
                        completed = true;
                        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                        QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(Texto, QRCodeGenerator.ECCLevel.L);
                        PngByteQRCode qRCode = new PngByteQRCode(qRCodeData);
                        byte[] qrCodeBytes = qRCode.GetGraphic(20);

                        MemoryStream qrStream = new MemoryStream(qrCodeBytes);

                        usuario_cliente user = new();

                        string servidor = "smtp.gmail.com";
                        int Puerto = 587;
                        string GmailUser = "emailcinepolis@gmail.com";
                        string GmailPass = "wdxirgcurnjfmxva";

                        MimeMessage mensaje = new MimeMessage();
                        mensaje.From.Add(new MailboxAddress("Cinepolis Reserva", GmailUser));
                        mensaje.To.Add(new MailboxAddress("", Preferences.Get("correo", "")));
                        mensaje.Subject = "";

                        BodyBuilder CuerpoMensaje = new BodyBuilder();
                        CuerpoMensaje.TextBody = "Utiliza este código QR para completar el pago en tu Cinepolis más cercano";

                        CuerpoMensaje.Attachments.Add("codigo_qr.png", qrStream);

                        mensaje.Body = CuerpoMensaje.ToMessageBody();

                        using (SmtpClient ClienteSmtp = new SmtpClient())
                        {
                            ClienteSmtp.CheckCertificateRevocation = false;
                            await ClienteSmtp.ConnectAsync(servidor, Puerto, SecureSocketOptions.StartTls);
                            await ClienteSmtp.AuthenticateAsync(GmailUser, GmailPass);
                            await ClienteSmtp.SendAsync(mensaje);
                            await ClienteSmtp.DisconnectAsync(true);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Esperando hasta que el estado sea COMPLETED...");
                        await Task.Delay(TimeSpan.FromSeconds(5));
                    }
                }
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");

        }
        finally
        {
            reserva_acept res = new();
            selfLink = selfLink+ "/capture";
            HttpWebRequest captureRequest = (HttpWebRequest)WebRequest.Create(selfLink);
            captureRequest.Method = "POST";
            captureRequest.Headers.Add("Authorization", "Basic QVVuTmV2OWYxc1pUc2hNNDcyNkhRRzREQ05Jems0S0N1WWhtUGpiOEJPQkh2U0FPd09GdEhyV0wtbG5QZUxyTUZOR3pnWmY3V29xdlVZRTg6RUU2b1ZoQzRzSHlGR2plZU04YjRIS1VFRXVPSUFBYTVRbnFxbjhWOGVZb0xDczFYbF84TjMzWGFoRks3MXJmTmZQZ1c2dERXWkdLRWczNHY=");
            captureRequest.ContentType = "application/json";
            string jsonData = "{}";
            using (var streamWriter = new StreamWriter(captureRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            HttpWebResponse httpResponse = (HttpWebResponse)captureRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string selfResult = await streamReader.ReadToEndAsync();
                JObject selfJsonResult = JObject.Parse(selfResult);
                string status = selfJsonResult["status"].ToString();
                if (status == "COMPLETED")
                {
                    Status = status;
                    Content = null;
                    await Task.Delay(TimeSpan.FromSeconds(10));
                    await Navigation.PopModalAsync();
                }
            }
        }

    }

}
