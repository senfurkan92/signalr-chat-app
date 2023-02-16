using CORE.Models;
using System.Net;
using System.Net.Mail;

namespace WEB.Models
{
    public interface IEmailWork
    {
        List<string> BccAddresses { get; set; }
        string CredentialAddress { get; set; }
        string CredentialPassword { get; set; }
        string DisplayName { get; set; }
        string FromAddress { get; set; }
        string Host { get; set; }
        string MailBody { get; set; }
        string MailSubject { get; set; }
        int Port { get; set; }
        List<string> ToAddresses { get; set; }

        void Dispose();
        ResultModel SendMail();
    }

    public class EmailWork : IDisposable, IEmailWork
    {
        private bool disposedValue;

        public string FromAddress { get; set; } = "info@eminbuy.com";
        public List<string> ToAddresses { get; set; } = new List<string>();
        public List<string> BccAddresses { get; set; } = new List<string>();
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
        public string CredentialAddress { get; set; } = "info@eminbuy.com";
        public string CredentialPassword { get; set; } = "Emintek2021!!";
        public int Port { get; set; } = 587; //587
        public string Host { get; set; } = "mail.eminbuy.com";
        public string DisplayName { get; set; } = "Emintek E-mail Service";


        public ResultModel SendMail()
        {
            ResultModel result = null;
            try
            {
                SmtpClient client = new SmtpClient()
                {
                    Credentials = new NetworkCredential(CredentialAddress, CredentialPassword),
                    EnableSsl = true,
                    Timeout = 5000,
                    Port = Port,
                    Host = Host,
                };

                MailMessage message = new MailMessage()
                {
                    From = new MailAddress(FromAddress, DisplayName, System.Text.Encoding.UTF8),
                    Subject = MailSubject,
                    Body = MailBody,
                    IsBodyHtml = true,
                    SubjectEncoding = System.Text.Encoding.UTF8,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    Priority = MailPriority.Normal,
                };

                ToAddresses.ForEach(x =>
                {
                    message.To.Add(x);
                });

                BccAddresses.ForEach(x =>
                {
                    message.Bcc.Add(x);
                });

                client.Send(message);
                result = new ResultModel
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                result = new ResultModel
                {
                    IsSuccess = false,
                    Exception = ex
                };
            }

            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~EmailWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
