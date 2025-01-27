namespace models ;
 using string = System.string;  public class ResourceData { public string ResourceDataID { get; set; } 
 public string ChangeType { get; set; } 
 public string Subject { get; set; } 
 public ResourceData (  string resourcedataid, string changetype, string subject ) {
  ResourceDataID=resourcedataid;
  ChangeType=changetype;
  Subject=subject;
 }
 }