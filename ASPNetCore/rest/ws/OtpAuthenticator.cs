namespace rest.ws ;
 using classes; using java.util; using models; using repository.jpa; using security;  public class OtpAuthenticator :  Authenticator { private readonly JwtTokenUtil jwtTokenUtil ;
 
 private readonly OneTimePasswordRepository oneTimePasswordRepository ;
 
 public TaskLoginResult Login (  string email, string phone, string username, string password, string deviceToken, string token, string code, string clientAddress ) {
  OneTimePassword otp=oneTimePasswordRepository.GetByToken(token);
  LoginResult loginResult=new LoginResult();
 if ( otp == null ) {
 loginResult.Success = false ;
 loginResult.FailureMessage = "Invalid token." ;
 return loginResult ;
 }
 if ( otp.Expiry.<(DateTime.now()) ) {
 loginResult.Success = false ;
 loginResult.FailureMessage = "OTP validity has expired." ;
 return loginResult ;
 }
 if ( !(code.Equals(otp.Code)) ) {
 loginResult.Success = false ;
 loginResult.FailureMessage = "Invalid code." ;
 return loginResult ;
 }
  BaseUser user=otp.User();
 if ( user == null ) {
 loginResult.Success = false ;
 loginResult.FailureMessage = "Invalid user." ;
 return loginResult ;
 }
 RecordLoginHistory(user,true,"",clientAddress) ;
 loginResult.Success = true ;
 loginResult.UserObject = user ;
  string type=((string)user.getClass().GetSimpleName());
  string id=string.valueOf(user.getId());
  string finalToken=jwtTokenUtil.generateToken(id,new UserProxy(type,user.getId(),UUID.randomUUID().toString()));
 loginResult.Token = finalToken ;
 if ( deviceToken != null ) {
 user.DeviceToken(deviceToken) ;
 store.Database.get().save(user) ;
 }
 return loginResult ;
 }
 private void RecordLoginHistory (  BaseUser user, bool success, string failureReason, string clientAddress ) {
  UserLoginRecord loginRecord=new UserLoginRecord();
 loginRecord.TimeStamp = DateTime.UtcNow() ;
 loginRecord.Success = success ;
 loginRecord.FailureReason = failureReason ;
 loginRecord.User = user ;
 loginRecord.ClientAddress = clientAddress ;
 store.Database.get().save(loginRecord) ;
 }
 }