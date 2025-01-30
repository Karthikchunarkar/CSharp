namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class UserDevicesRequest :  CreatableObject { public static int USER = 0 ;
 
 public static int TOKEN = 1 ;
 
 private BaseUser user { get; set; } 
 private string token { get; set; } 
 public UserDevicesRequest (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.UserDevicesRequest ;
 }
 public string Type (  ) {
 return "UserDevicesRequest" ;
 }
 public int FieldsCount (  ) {
 return 2 ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
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
 public string DisplayName (  ) {
 return "UserDevicesRequest" ;
 }
 public bool equals (  Object a ) {
 return a is UserDevicesRequest && base.Equals(a) ;
 }
 public UserDevicesRequest DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  UserDevicesRequest _obj=((UserDevicesRequest)dbObj);
 _obj.User(user) ;
 _obj.Token(token) ;
 }
 public UserDevicesRequest CloneInstance (  UserDevicesRequest cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new UserDevicesRequest() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.User(this.User()) ;
 cloneObj.Token(this.Token()) ;
 return cloneObj ;
 }
 public bool TransientModel (  ) {
 return true ;
 }
 public UserDevicesRequest CreateNewInstance (  ) {
 return new UserDevicesRequest() ;
 }
 public void CollectCreatableReferences (  List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 _refs.Add(this.user) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }