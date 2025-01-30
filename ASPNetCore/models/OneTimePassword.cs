namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class OneTimePassword :  CreatableObject { public static int INPUT = 0 ;
 
 public static int INPUTTYPE = 1 ;
 
 public static int USERTYPE = 2 ;
 
 public static int SUCCESS = 3 ;
 
 public static int ERRORMSG = 4 ;
 
 public static int TOKEN = 5 ;
 
 public static int CODE = 6 ;
 
 public static int USER = 7 ;
 
 public static int EXPIRY = 8 ;
 
 private string input { get; set; } 
 private string inputType { get; set; } 
 private string userType { get; set; } 
 private bool success { get; set; } = false ;
 
 private string errorMsg { get; set; } 
 private string token { get; set; } 
 private string code { get; set; } 
 private BaseUser user { get; set; } 
 private DateTime expiry { get; set; } 
 public OneTimePassword (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.OneTimePassword ;
 }
 public string Type (  ) {
 return "OneTimePassword" ;
 }
 public int FieldsCount (  ) {
 return 9 ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public string Input (  ) {
 _CheckProxy() ;
 return this.input ;
 }
 public void Input (  string input ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.input,input) ) {
 return ;
 }
 fieldChanged(INPUT,this.input,input) ;
 this.input = input ;
 }
 public string InputType (  ) {
 _CheckProxy() ;
 return this.inputType ;
 }
 public void InputType (  string inputType ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.inputType,inputType) ) {
 return ;
 }
 fieldChanged(INPUTTYPE,this.inputType,inputType) ;
 this.inputType = inputType ;
 }
 public string UserType (  ) {
 _CheckProxy() ;
 return this.userType ;
 }
 public void UserType (  string userType ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.userType,userType) ) {
 return ;
 }
 fieldChanged(USERTYPE,this.userType,userType) ;
 this.userType = userType ;
 }
 public bool IsSuccess (  ) {
 _CheckProxy() ;
 return this.success ;
 }
 public void Success (  bool success ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.success,success) ) {
 return ;
 }
 fieldChanged(SUCCESS,this.success,success) ;
 this.success = success ;
 }
 public string ErrorMsg (  ) {
 _CheckProxy() ;
 return this.errorMsg ;
 }
 public void ErrorMsg (  string errorMsg ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.errorMsg,errorMsg) ) {
 return ;
 }
 fieldChanged(ERRORMSG,this.errorMsg,errorMsg) ;
 this.errorMsg = errorMsg ;
 }
 public string Token (  ) {
 _CheckProxy() ;
 return this.token ;
 }
 public void Token (  string token ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.token,token) ) {
 return ;
 }
 fieldChanged(TOKEN,this.token,token) ;
 this.token = token ;
 }
 public string Code (  ) {
 _CheckProxy() ;
 return this.code ;
 }
 public void Code (  string code ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.code,code) ) {
 return ;
 }
 fieldChanged(CODE,this.code,code) ;
 this.code = code ;
 }
 public BaseUser User (  ) {
 _CheckProxy() ;
 return this.user ;
 }
 public void User (  BaseUser user ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.user,user) ) {
 return ;
 }
 fieldChanged(USER,this.user,user) ;
 this.user = user ;
 }
 public DateTime Expiry (  ) {
 _CheckProxy() ;
 return this.expiry ;
 }
 public void Expiry (  DateTime expiry ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.expiry,expiry) ) {
 return ;
 }
 fieldChanged(EXPIRY,this.expiry,expiry) ;
 this.expiry = expiry ;
 }
 public string DisplayName (  ) {
 return "OneTimePassword" ;
 }
 public bool equals (  Object a ) {
 return a is OneTimePassword && base.Equals(a) ;
 }
 public OneTimePassword DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  OneTimePassword _obj=((OneTimePassword)dbObj);
 _obj.Input(input) ;
 _obj.InputType(inputType) ;
 _obj.UserType(userType) ;
 _obj.Success(success) ;
 _obj.ErrorMsg(errorMsg) ;
 _obj.Token(token) ;
 _obj.Code(code) ;
 _obj.User(user) ;
 _obj.Expiry(expiry) ;
 }
 public OneTimePassword CloneInstance (  OneTimePassword cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new OneTimePassword() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Input(this.Input()) ;
 cloneObj.InputType(this.InputType()) ;
 cloneObj.UserType(this.UserType()) ;
 cloneObj.Success(this.IsSuccess()) ;
 cloneObj.ErrorMsg(this.ErrorMsg()) ;
 cloneObj.Token(this.Token()) ;
 cloneObj.Code(this.Code()) ;
 cloneObj.User(this.User()) ;
 cloneObj.Expiry(this.Expiry()) ;
 return cloneObj ;
 }
 public OneTimePassword CreateNewInstance (  ) {
 return new OneTimePassword() ;
 }
 public void CollectCreatableReferences (  List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 _refs.Add(this.user) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }