namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class UserLoginRecord :  CreatableObject { public static int USER = 0 ;
 
 public static int TIMESTAMP = 1 ;
 
 public static int IPADDRESS = 2 ;
 
 public static int BROWSER = 3 ;
 
 public static int DEVICE = 4 ;
 
 public static int LOCATION = 5 ;
 
 public static int SUCCESS = 6 ;
 
 public static int FAILUREREASON = 7 ;
 
 private BaseUser user { get; set; } 
 private DateTime timeStamp { get; set; } 
 private string iPAddress { get; set; } 
 private string browser { get; set; } 
 private string device { get; set; } 
 private string location { get; set; } 
 private bool success { get; set; } = false ;
 
 private string failureReason { get; set; } 
 public UserLoginRecord (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.UserLoginRecord ;
 }
 public string Type (  ) {
 return "UserLoginRecord" ;
 }
 public int FieldsCount (  ) {
 return 8 ;
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
 public DateTime TimeStamp (  ) {
 _CheckProxy() ;
 return this.timeStamp ;
 }
 public void TimeStamp (  DateTime timeStamp ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.timeStamp,timeStamp) ) {
 return ;
 }
 fieldChanged(TIMESTAMP,this.timeStamp,timeStamp) ;
 this.timeStamp = timeStamp ;
 }
 public string IPAddress (  ) {
 _CheckProxy() ;
 return this.iPAddress ;
 }
 public void IPAddress (  string iPAddress ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.iPAddress,iPAddress) ) {
 return ;
 }
 fieldChanged(IPADDRESS,this.iPAddress,iPAddress) ;
 this.iPAddress = iPAddress ;
 }
 public string Browser (  ) {
 _CheckProxy() ;
 return this.browser ;
 }
 public void Browser (  string browser ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.browser,browser) ) {
 return ;
 }
 fieldChanged(BROWSER,this.browser,browser) ;
 this.browser = browser ;
 }
 public string Device (  ) {
 _CheckProxy() ;
 return this.device ;
 }
 public void Device (  string device ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.device,device) ) {
 return ;
 }
 fieldChanged(DEVICE,this.device,device) ;
 this.device = device ;
 }
 public string Location (  ) {
 _CheckProxy() ;
 return this.location ;
 }
 public void Location (  string location ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.location,location) ) {
 return ;
 }
 fieldChanged(LOCATION,this.location,location) ;
 this.location = location ;
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
 public string FailureReason (  ) {
 _CheckProxy() ;
 return this.failureReason ;
 }
 public void FailureReason (  string failureReason ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.failureReason,failureReason) ) {
 return ;
 }
 fieldChanged(FAILUREREASON,this.failureReason,failureReason) ;
 this.failureReason = failureReason ;
 }
 public string DisplayName (  ) {
 return "UserLoginRecord" ;
 }
 public bool equals (  Object a ) {
 return a is UserLoginRecord && base.Equals(a) ;
 }
 public UserLoginRecord DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  UserLoginRecord _obj=((UserLoginRecord)dbObj);
 _obj.User(user) ;
 _obj.TimeStamp(timeStamp) ;
 _obj.IPAddress(iPAddress) ;
 _obj.Browser(browser) ;
 _obj.Device(device) ;
 _obj.Location(location) ;
 _obj.Success(success) ;
 _obj.FailureReason(failureReason) ;
 }
 public UserLoginRecord CloneInstance (  UserLoginRecord cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new UserLoginRecord() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.User(this.User()) ;
 cloneObj.TimeStamp(this.TimeStamp()) ;
 cloneObj.IPAddress(this.IPAddress()) ;
 cloneObj.Browser(this.Browser()) ;
 cloneObj.Device(this.Device()) ;
 cloneObj.Location(this.Location()) ;
 cloneObj.Success(this.IsSuccess()) ;
 cloneObj.FailureReason(this.FailureReason()) ;
 return cloneObj ;
 }
 public UserLoginRecord CreateNewInstance (  ) {
 return new UserLoginRecord() ;
 }
 public void CollectCreatableReferences (  List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 _refs.Add(this.user) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }