namespace models ;
 using DateTime = System.DateTime; using string = System.string;  public class SMSMessage { public string From { get; set; } 
 public List<string> To { get; set; } 
 public string Body { get; set; } 
 public DateTime CreatedOn { get; set; } = DateTime.now() ;
 
 public string DltTemplateId { get; set; } 
 public SMSMessage (  string from, List<string> to, string body, DateTime createdon, string dlttemplateid ) {
  From=from;
  To=to;
  Body=body;
  CreatedOn=createdon;
  DltTemplateId=dlttemplateid;
 }
 }