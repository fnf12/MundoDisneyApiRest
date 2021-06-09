using EASendMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MundoDisneyApiRest.services
{
    public class Logic
    {
        public string SendEmail(string to, string subject, string message)
        {
            string mensaje = "Error al enviar correo";

            try
            {

                SmtpMail correo = new("Tryit")
                {
                    From = "fnf12soft@gmail.com",
                    To = to,
                    Subject = subject,
                    TextBody = message
                };


                SmtpServer server = new("smtp.gmail.com")
                {
                    User = "fnf12soft@gmail.com",
                    Password = "spiderman915",
                    Port = 587,
                    ConnectType = SmtpConnectType.ConnectSSLAuto
                };

                SmtpClient client = new();

                client.SendMail(server, correo);

                mensaje = "Correo enviado Correctamente";
            }
            catch(Exception ex)
            {
                mensaje = "Eroor al enviar correo " + ex.Message;
            }

            return mensaje;
        }
    }
}
