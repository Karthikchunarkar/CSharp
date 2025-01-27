namespace models ;
 using DFile = d3e.core.DFile; using DateTime = System.DateTime; using bool = System.bool; using string = System.string;  public class EmailMessage { public string From { get; set; } 
 public List<string> To { get; set; } 
 public string Body { get; set; } 
 public DateTime CreatedOn { get; set; } = DateTime.now() ;
 
 public List<string> Bcc { get; set; } 
 public List<string> Cc { get; set; } 
 public string Subject { get; set; } 
 public bool Html { get; set; } 
 public List<DFile> InlineAttachments { get; set; } 
 public List<DFile> Attachments { get; set; } 
 public EmailMessage (  string from, List<string> to, string body, DateTime createdon, List<string> bcc, List<string> cc, string subject, bool html, List<DFile> inlineattachments, List<DFile> attachments ) {
  From=from;
  To=to;
  Body=body;
  CreatedOn=createdon;
  Bcc=bcc;
  Cc=cc;
  Subject=subject;
  Html=html;
  InlineAttachments=inlineattachments;
  Attachments=attachments;
 }
 }