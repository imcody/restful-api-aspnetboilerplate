namespace ResponsibleSystem.Configuration
{
    public class EmailSendingSettings
    {
        public string SendGridApiKey { get; set; }

        public string MailFromAddress { get; set; }

        public string MailFromDisplayName { get; set; }
    }
}