using UnityEngine;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class Sender : MonoBehaviour
{
    public static void SendMessage()
    {
        MailMessage message = new MailMessage();
        message.Body = "Hi";
        message.From = new MailAddress("От кого");
        message.To.Add("Кому");
        message.BodyEncoding = System.Text.Encoding.UTF8;
        
        SmtpClient client = new SmtpClient();
        client.Host = "smtp.gmail.com";
        client.Port = 587;
        client.Credentials = new NetworkCredential(message.From.Address, "Password");
        client.EnableSsl = true;
        
        ServicePointManager.ServerCertificateValidationCallback =
            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true;};
        
        client.Send(message);
    }
    
}