namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public abstract class BaseUserSession :  CreatableObject { public static int USERSESSIONID = 0 ;
 
 private string userSessionId { get; set; } 
 public BaseUserSession (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.BaseUserSession ;
 }
 public string Type (  ) {
 return "BaseUserSession" ;
 }
 public int FieldsCount (  ) {
 return 1 ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public string UserSessionId (  ) {
 _CheckProxy() ;
 return this.userSessionId ;
 }
 public void UserSessionId (  string userSessionId ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.userSessionId,userSessionId) ) {
 return ;
 }
 fieldChanged(USERSESSIONID,this.userSessionId,userSessionId) ;
 this.userSessionId = userSessionId ;
 }
 public string DisplayName (  ) {
 return "BaseUserSession" ;
 }
 public bool equals (  Object a ) {
 return a is BaseUserSession && base.Equals(a) ;
 }
 public BaseUserSession DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  BaseUserSession _obj=((BaseUserSession)dbObj);
 _obj.UserSessionId(userSessionId) ;
 }
 public BaseUserSession CloneInstance (  BaseUserSession cloneObj ) {
 base.CloneInstance(cloneObj) ;
 cloneObj.UserSessionId(this.UserSessionId()) ;
 return cloneObj ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }