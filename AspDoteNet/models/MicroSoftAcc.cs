namespace models ;
 using OAuthCredentialsStatus = classes.OAuthCredentialsStatus; using string = System.string;  public class MicroSoftAcc { public string AccessToken { get; set; } 
 public string RefreshToken { get; set; } 
 public OAuthCredentialsStatus OauthStatus { get; set; } 
 public MicroSoftAcc (  string accesstoken, string refreshtoken, OAuthCredentialsStatus oauthstatus ) {
  AccessToken=accesstoken;
  RefreshToken=refreshtoken;
  OauthStatus=oauthstatus;
 }
 }