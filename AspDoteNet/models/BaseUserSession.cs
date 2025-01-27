namespace models ;
 using string = System.string;  public class BaseUserSession { public string UserSessionId { get; set; } 
 public BaseUserSession (  string usersessionid ) {
  UserSessionId=usersessionid;
 }
 }