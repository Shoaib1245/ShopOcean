namespace Service.UI.EmailSender
{
    public class EmailTemplate
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmailTemplate(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public bool CommonTemplate(string subject, string toEmail, string pageHeader,string Code)
        {
            string body = string.Empty;

            // Get the path to the email template
            string templatePath = Path.Combine(_webHostEnvironment.WebRootPath, "EmailTemplate", "CommonTemplate.html");

            // Read the content of the email template
            if (File.Exists(templatePath))
            {
                body = File.ReadAllText(templatePath);
            }
            else
            {
                throw new FileNotFoundException("The email template file was not found.", templatePath);
            }

            // Replace placeholders with actual values
            body = body.Replace("@PageHeader", pageHeader);
            //body = body.Replace("@PageBody", pageBody);
            body = body.Replace("@Code", Code);
            //body = body.Replace("@LogoUrl", "https://eticmain.azurewebsites.net/assets/images/whiteLogo.png");
            //body = body.Replace("@ProjectUrl", "https://eticmain.azurewebsites.net/");
            //body = body.Replace("@ClickHereUrl", clickHereUrl);
            //body = body.Replace("@PageFooter", pageFooter);
            //body = body.Replace("@ClickHereLinkText", clickHereLinkText);
            body = body.Replace("@Date", DateTime.Now.ToString());

            // Send the email
            return EmailSender.EmailSend.SendEmail(toEmail, subject, body);
        }
    }
}
