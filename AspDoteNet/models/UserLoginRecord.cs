namespace models ;
 using BaseUser = models.BaseUser; using DateTime = System.DateTime; using bool = System.bool; using string = System.string;  public class UserLoginRecord { public BaseUser User { get; set; } 
 public DateTime TimeStamp { get; set; } 
 public string IPAddress { get; set; } 
 public string Browser { get; set; } 
 public string Device { get; set; } 
 public string Location { get; set; } 
 public bool Success { get; set; } 
 public string FailureReason { get; set; } 
 public UserLoginRecord (  BaseUser user, DateTime timestamp, string ipaddress, string browser, string device, string location, bool success, string failurereason ) {
  User=user;
  TimeStamp=timestamp;
  IPAddress=ipaddress;
  Browser=browser;
  Device=device;
  Location=location;
  Success=success;
  FailureReason=failurereason;
 }
 }