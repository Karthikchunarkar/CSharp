namespace classes ;
 using d3e.core; using lists; using models; using store;  public class LoginResult :  DBObject { public static int _SUCCESS = 0 ;
 
 public static int _USEROBJECT = 1 ;
 
 public static int _TOKEN = 2 ;
 
 public static int _FAILUREMESSAGE = 3 ;
 
 private long id 
 private bool success 
 private BaseUser userObject 
 private TypeAndId userObjectRef 
 private string token 
 private string failureMessage 
 public LoginResult (  ) {
 }
 public LoginResult (  string failureMessage, bool success, string token, BaseUser userObject ) {
 this.FailureMessage = failureMessage ;
 this.Success = success ;
 this.Token = token ;
 this.UserObject = userObject ;
 }
 public long getId (  ) {
 return id ;
 }
 public void setId (  long id ) {
 this.id = id ;
 }
 public bool getSuccess (  ) {
 return success ;
 }
 public void setSuccess (  bool success ) {
 fieldChanged(_SUCCESS,this.success,success) ;
 this.success = success ;
 }
 public BaseUser getUserObject (  ) {
 return userObject ;
 }
 public TypeAndId getUserObjectRef (  ) {
 return userObjectRef ;
 }
 public void setUserObject (  BaseUser userObject ) {
 fieldChanged(_USEROBJECT,this.userObject,userObject) ;
 this.userObject = userObject ;
 }
 public void setUserObjectRef (  TypeAndId userObjectRef ) {
 fieldChanged(_USEROBJECT,this.userObjectRef,userObjectRef) ;
 this.userObjectRef = userObjectRef ;
 }
 public string getToken (  ) {
 return token ;
 }
 public void setToken (  string token ) {
 fieldChanged(_TOKEN,this.token,token) ;
 this.token = token ;
 }
 public string getFailureMessage (  ) {
 return failureMessage ;
 }
 public void setFailureMessage (  string failureMessage ) {
 fieldChanged(_FAILUREMESSAGE,this.failureMessage,failureMessage) ;
 this.failureMessage = failureMessage ;
 }
 public int _typeIdx (  ) {
 return SchemaConstants.LoginResult ;
 }
 public String _type (  ) {
 return "LoginResult" ;
 }
 public int _fieldsCount (  ) {
 return 4 ;
 }
 public void _convertToObjectRef (  ) {
 this.userObjectRef = TypeAndId.from(this.userObject) ;
 this.userObject = null ;
 }
 }