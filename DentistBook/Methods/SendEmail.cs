using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DentistBook.Methods
{
    public class SendEmail
    {
        public  void SendEmailMet(string date , string doctor , string procedure , string hour , string name , string email, out bool result)
        {
            try
            {
              

                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;
                string emailFrom = "alfaomegatest2@gmail.com";
                string password = "Qwerty$1";
                string emailTo = email;
                string subject = "Reservation Doctor !";
                string body = "Hello, Mr."+ name +". "+ " Your recipise for reservation : <br />  " + " Date : "+date +" hour: "+hour+ "<br />  Doctor: " + doctor + " <br /> Procedure : " + procedure +" " ;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }



                result = true;
            }
            catch (Exception e)
            {
                result = false;
            }
          
        }
    }
}