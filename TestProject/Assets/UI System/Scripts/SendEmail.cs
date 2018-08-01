using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SendEmail : MonoBehaviour {



    public void Send()
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("ahmed.gamal.fouad@gmail.com");
        mail.To.Add("a.fouad@saedubai.com");

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        mail.Subject = "WHATEVER_YOU_WANT_TEXT";
        mail.Body = "WHATEVER_YOU_WANT_TEXT";
        smtpServer.Credentials = new System.Net.NetworkCredential("info@dawgzstore.com", "bubbles@2008") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        smtpServer.Send(mail);
        //smtpServer.SendAsync(mail);
        Debug.Log("success");
    }
}
