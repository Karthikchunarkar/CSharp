namespace models ;
 using BaseUser = models.BaseUser; using string = System.string;  public class UserDevice { public BaseUser User { get; set; } 
 public string DeviceToken { get; set; } 
 public UserDevice (  BaseUser user, string devicetoken ) {
  User=user;
  DeviceToken=devicetoken;
 }
 }