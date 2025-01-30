namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class UserDevice :  CreatableObject { public static int USER = 0 ;
 
 public static int DEVICETOKEN = 1 ;
 
 private BaseUser user { get; set; } 
 private string deviceToken { get; set; } 
 public UserDevice (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.UserDevice ;
 }
 public string Type (  ) {
 return "UserDevice" ;
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
 if ( this.user != null ) {
 this.user.Devices(null) ;
 }
 this.user = user ;
 if ( this.user != null ) {
 this.user.Devices(this) ;
 }
 }
 public string DeviceToken (  ) {
 _CheckProxy() ;
 return this.deviceToken ;
 }
 public void DeviceToken (  string deviceToken ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.deviceToken,deviceToken) ) {
 return ;
 }
 fieldChanged(DEVICETOKEN,this.deviceToken,deviceToken) ;
 this.deviceToken = deviceToken ;
 }
 public string DisplayName (  ) {
 return "" ;
 }
 public bool equals (  Object a ) {
 return a is UserDevice && base.Equals(a) ;
 }
 public UserDevice DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  UserDevice _obj=((UserDevice)dbObj);
 _obj.User(user) ;
 _obj.DeviceToken(deviceToken) ;
 }
 public UserDevice CloneInstance (  UserDevice cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new UserDevice() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.User(this.User()) ;
 cloneObj.DeviceToken(this.DeviceToken()) ;
 return cloneObj ;
 }
 public string ToString (  ) {
 return DisplayName() ;
 }
 public UserDevice CreateNewInstance (  ) {
 return new UserDevice() ;
 }
 public void CollectCreatableReferences (  List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 _refs.Add(this.user) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }