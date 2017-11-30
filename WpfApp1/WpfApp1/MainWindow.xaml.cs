using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string[] Scopes = { GmailService.Scope.MailGoogleCom };
        static string ApplicationName = "Gmail API .NET Quickstart";
        public MainWindow()
        {
            InitializeComponent();
            UserCredential credential;

            using (var stream =
                new FileStream(@"C:\Users\Kaivaan\source\repos\WpfApp1\WpfApp1\client_id.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = System.IO.Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");





            // List labels.
            var labels = request.Execute().Labels;
            Console.WriteLine("Labels:");
            if (labels != null && labels.Count > 0)
            {
                foreach (var labelItem in labels)
                {
                    if (labelItem.Name == "Work")
                    {
                        Console.WriteLine("{0}", labelItem.Name);
                    }
                   
                }
            }
            else
            {
                Console.WriteLine("No labels found.");
            }
            //Console.Read();


            ///--- retrive first email body
            Message messageFeed = service.Users.Messages.List("me").Execute().Messages.First();//get first latest message
            UsersResource.MessagesResource.GetRequest getReq = new UsersResource.MessagesResource.GetRequest(service, "me", messageFeed.Id);
            getReq.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Full;
            Message message = getReq.Execute();
            var BodyOftheFirstMessage = GetMimeString(message.Payload);
         //   Console.WriteLine(BodyOftheFirstMessage);


            //--------headers of first email

            foreach(var i in message.Payload.Headers)
            {
                 if (i.Name == "From")
                {
                    var x = i.Value;
                    Console.WriteLine("From : " + i.Value);
                }
                if (i.Name == "To")
                {
                    var x = i.Value;
                    Console.WriteLine("To : " + i.Value);
                }
                else if (i.Name == "Subject")
                {
                    var x = i.Value;
                    Console.WriteLine("Subject : " + i.Value);
                }
        }   
        }

        public static byte[] FromBase64ForUrlString(string base64ForUrlInput)
        {
            int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
            StringBuilder result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
            result.Append(String.Empty.PadRight(padChars, '='));
            result.Replace('-', '+');
            result.Replace('_', '/');
            return Convert.FromBase64String(result.ToString());
        }
        public static String GetMimeString(MessagePart Parts)
        {
            String Content = "";

            if (Parts.Parts != null)
            {
                foreach (MessagePart part in Parts.Parts)
                {
                    Content = String.Format("{0}\n{1}", Content, GetMimeString(part));
                    
                }
            }
            else if (Parts.Body.Data != null && Parts.Body.AttachmentId == null && Parts.MimeType == "text/plain")
            {
                String codedContent = Parts.Body.Data.Replace("-", "+");
                codedContent = codedContent.Replace("_", "/");
                byte[] data = Convert.FromBase64String(codedContent);
                Content = Encoding.UTF8.GetString(data);
               
            }
            return Content;
        }
        }
}
