namespace rest.ws ;
 using classes; using java.util; using models; using org.springframework.security.crypto.password; using repository.jpa; using security;  public class AdminUserEmailAuthenticator :  Authenticator { private readonly JwtTokenUtil jwtTokenUtil ;
 
 private readonly PasswordEncoder PasswordEncoder ;
 
 private readonly AdminRepository adminRepository ;
 
 public AdminUserEmailAuthenticator (  AdminRepository adminRepository, PasswordEncoder passwordEncoder, JwtTokenUtil jwtTokenUtil ) {
  adminRepository=adminRepository;
  PasswordEncoder=passwordEncoder;
  jwtTokenUtil=jwtTokenUtil;
 }
 public LoginResult Login (  string email, string phone, string username, string password, string deviceToken, string token, string code, string clientAddress ) {
  var admin=adminRepository.GetByEmail(email.ToLower());
  var loginResult=new LoginResult();
 if ( admin == null ) {
 loginResult.Success = false ;
 loginResult.FailureMessage = "Invalid authentication details." ;
 return loginResult ;
 }
  var passwordVerificationResult=VerifyHashedPassword(admin,admin.getPassword(),password);
 if ( passwordVerificationResult != PasswordVerificationResult.Success ) {
 RecordLoginHistory(admin,false,"Invalid authentication details.",clientAddress) ;
 loginResult.Success = false ;
 loginResult.FailureMessage = "Invalid authentication details." ;
 return loginResult ;
 }
 RecordLoginHistory(admin,true,"",clientAddress) ;
 loginResult.Success = true ;
 loginResult.UserObject = admin ;
  var jwtToken=jwtTokenUtil.GenerateToken(email.ToLower(),new UserProxy("Admin",admin.getId(),UUID.randomUUID().toString()));
 loginResult.Token = jwtToken ;
 if ( !(string.IsNullOrEmpty(deviceToken)) ) {
 admin.DeviceToken = deviceToken ;
 store.Database.get().save(admin) ;
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