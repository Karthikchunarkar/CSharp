namespace models ;
 using ResourceData = models.ResourceData; using string = System.string;  public class Notification { public string SubscriptionId { get; set; } 
 public string ChangeType { get; set; } 
 public string Resource { get; set; } 
 public ResourceData ResourceData { get; set; } 
 public Notification (  string subscriptionid, string changetype, string resource, ResourceData resourcedata ) {
  SubscriptionId=subscriptionid;
  ChangeType=changetype;
  Resource=resource;
  ResourceData=resourcedata;
 }
 }