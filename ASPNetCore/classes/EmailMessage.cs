using d3e.core;

namespace classes
{
    //Future Will Remove this Class
    public class EmailMessage
    {
        public string From { get; set; }
        public List<string> To { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public List<string> Bcc { get; set; }
        public List<string> Cc { get; set; }
        public string Subject { get; set; }
        public bool Html { get; set; }
        public List<DFile> InlineAttachments { get; set; }
        public List<DFile> Attachments { get; set; }

        public EmailMessage()
        {
            
        }

        public EmailMessage(string from, List<string> to, string body, DateTime createdon, List<string> bcc, List<string> cc, string subject, bool html, List<DFile> inlineattachments, List<DFile> attachments)
        {
            From = from;
            To = to;
            Body = body;
            CreatedOn = createdon;
            Bcc = bcc;
            Cc = cc;
            Subject = subject;
            Html = html;
            InlineAttachments = inlineattachments;
            Attachments = attachments;
        }
    }
}
