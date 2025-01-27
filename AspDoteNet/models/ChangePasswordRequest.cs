namespace models ;
 using string = System.string;  public class ChangePasswordRequest { public string NewPassword { get; set; } 
 public ChangePasswordRequest (  string newpassword ) {
  NewPassword=newpassword;
 }
 }