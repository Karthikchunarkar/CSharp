namespace models ;
 using BaseUser = models.BaseUser; using DateTime = System.DateTime; using bool = System.bool; using string = System.string;  public class OneTimePassword { public string Input { get; set; } 
 public string InputType { get; set; } 
 public string UserType { get; set; } 
 public bool Success { get; set; } 
 public string ErrorMsg { get; set; } 
 public string Token { get; set; } 
 public string Code { get; set; } 
 public BaseUser User { get; set; } 
 public DateTime Expiry { get; set; } 
 public OneTimePassword (  string input, string inputtype, string usertype, bool success, string errormsg, string token, string code, BaseUser user, DateTime expiry ) {
  Input=input;
  InputType=inputtype;
  UserType=usertype;
  Success=success;
  ErrorMsg=errormsg;
  Token=token;
  Code=code;
  User=user;
  Expiry=expiry;
 }
 }