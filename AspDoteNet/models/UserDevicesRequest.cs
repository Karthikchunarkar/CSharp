namespace models ;
 using BaseUser = models.BaseUser; using string = System.string;  public class UserDevicesRequest { public BaseUser User { get; set; } 
 public string Token { get; set; } 
 public UserDevicesRequest (  BaseUser user, string token ) {
  User=user;
  Token=token;
 }
 }