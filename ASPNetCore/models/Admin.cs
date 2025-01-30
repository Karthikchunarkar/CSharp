namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class Admin :  BaseUser { public static int FULLNAME = 3 ;
 
 public static int EMAIL = 4 ;
 
 public static int PASSWORD = 5 ;
 
 public static int ROLE = 6 ;
 
 private string fullName { get; set; } 
 private string email { get; set; } 
 private string password { get; set; } 
 private UserRole role { get; set; } = UserRole.Admin ;
 
 private Admin Old { get; set; } 
 public Admin (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.Admin ;
 }
 public string Type (  ) {
 return "Admin" ;
 }
 public int FieldsCount (  ) {
 return 7 ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public string FullName (  ) {
 _CheckProxy() ;
 return this.fullName ;
 }
 public void FullName (  string fullName ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.fullName,fullName) ) {
 return ;
 }
 fieldChanged(FULLNAME,this.fullName,fullName) ;
 this.fullName = fullName ;
 }
 public string Email (  ) {
 _CheckProxy() ;
 return this.email ;
 }
 public void Email (  string email ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.email,email) ) {
 return ;
 }
 fieldChanged(EMAIL,this.email,email) ;
 this.email = email ;
 }
 public string Password (  ) {
 _CheckProxy() ;
 return this.password ;
 }
 public void Password (  string password ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.password,password) ) {
 return ;
 }
 fieldChanged(PASSWORD,this.password,password) ;
 this.password = password ;
 }
 public UserRole Role (  ) {
 _CheckProxy() ;
 return this.role ;
 }
 public void Role (  UserRole role ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.role,role) ) {
 return ;
 }
 fieldChanged(ROLE,this.role,role) ;
 this.role = role ;
 }
 public Admin getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((Admin)old) ;
 }
 public bool equals (  Object a ) {
 return a is Admin && base.Equals(a) ;
 }
 public Admin DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  Admin _obj=((Admin)dbObj);
 _obj.FullName(fullName) ;
 _obj.Email(email) ;
 _obj.Password(password) ;
 _obj.Role(role) ;
 }
 public Admin CloneInstance (  Admin cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new Admin() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.FullName(this.FullName()) ;
 cloneObj.Email(this.Email()) ;
 cloneObj.Password(this.Password()) ;
 cloneObj.Role(this.Role()) ;
 return cloneObj ;
 }
 public Admin CreateNewInstance (  ) {
 return new Admin() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 base.HandleChildChange(childIdx,set) ;
 }
 }