namespace models ;
 using string = System.string;  public class VerificationDataByTokenRequest { public string Token { get; set; } 
 public VerificationDataByTokenRequest (  string token ) {
  Token=token;
 }
 }