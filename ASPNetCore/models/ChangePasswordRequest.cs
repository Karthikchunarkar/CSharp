namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class ChangePasswordRequest :  CreatableObject { public static int NEWPASSWORD = 0 ;
 
 private string newPassword { get; set; } 
 public ChangePasswordRequest (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ChangePasswordRequest ;
 }
 public string Type (  ) {
 return "ChangePasswordRequest" ;
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
 public string NewPassword (  ) {
 _CheckProxy() ;
 return this.newPassword ;
 }
 public void NewPassword (  string newPassword ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.newPassword,newPassword) ) {
 return ;
 }
 fieldChanged(NEWPASSWORD,this.newPassword,newPassword) ;
 this.newPassword = newPassword ;
 }
 public string DisplayName (  ) {
 return "ChangePasswordRequest" ;
 }
 public bool equals (  Object a ) {
 return a is ChangePasswordRequest && base.Equals(a) ;
 }
 public ChangePasswordRequest DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ChangePasswordRequest _obj=((ChangePasswordRequest)dbObj);
 _obj.NewPassword(newPassword) ;
 }
 public ChangePasswordRequest CloneInstance (  ChangePasswordRequest cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ChangePasswordRequest() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.NewPassword(this.NewPassword()) ;
 return cloneObj ;
 }
 public bool TransientModel (  ) {
 return true ;
 }
 public ChangePasswordRequest CreateNewInstance (  ) {
 return new ChangePasswordRequest() ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }