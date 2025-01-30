namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public abstract class BaseUser :  CreatableObject { public static int ISACTIVE = 0 ;
 
 public static int DEVICETOKEN = 1 ;
 
 public static int DEVICES = 2 ;
 
 private bool isActive { get; set; } = false ;
 
 private string deviceToken { get; set; } = null ;
 
 private UserDevice devices { get; set; } 
 public BaseUser (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.BaseUser ;
 }
 public string Type (  ) {
 return "BaseUser" ;
 }
 public int FieldsCount (  ) {
 return 3 ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public bool IsIsActive (  ) {
 _CheckProxy() ;
 return this.isActive ;
 }
 public void IsActive (  bool isActive ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.isActive,isActive) ) {
 return ;
 }
 fieldChanged(ISACTIVE,this.isActive,isActive) ;
 this.isActive = isActive ;
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
 public UserDevice Devices (  ) {
 _CheckProxy() ;
 return this.devices ;
 }
 public void Devices (  UserDevice devices ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.devices,devices) ) {
 return ;
 }
 fieldChanged(DEVICES,this.devices,devices) ;
 this.devices = devices ;
 }
 public string DisplayName (  ) {
 return "" ;
 }
 public bool equals (  Object a ) {
 return a is BaseUser && base.Equals(a) ;
 }
 public BaseUser DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  BaseUser _obj=((BaseUser)dbObj);
 _obj.IsActive(isActive) ;
 _obj.DeviceToken(deviceToken) ;
 _obj.Devices(devices) ;
 }
 public BaseUser CloneInstance (  BaseUser cloneObj ) {
 base.CloneInstance(cloneObj) ;
 cloneObj.IsActive(this.IsIsActive()) ;
 cloneObj.DeviceToken(this.DeviceToken()) ;
 cloneObj.Devices(this.Devices()) ;
 return cloneObj ;
 }
 public string ToString (  ) {
 return DisplayName() ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }