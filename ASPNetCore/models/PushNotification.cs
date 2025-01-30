namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class PushNotification :  CreatableObject { public static int USERS = 0 ;
 
 public static int SKIPTHISDEVICE = 1 ;
 
 public static int DEVICETOKEN = 2 ;
 
 public static int TITLE = 3 ;
 
 public static int BODY = 4 ;
 
 public static int PATH = 5 ;
 
 public static int DATA = 6 ;
 
 public static int FAILED = 7 ;
 
 public static int FAILEDDEVICES = 8 ;
 
 private List<BaseUser> users { get; set; } = D3EPersistanceList.reference(USERS) ;
 
 private bool skipThisDevice { get; set; } = false ;
 
 private string deviceToken { get; set; } 
 private string title { get; set; } 
 private string body { get; set; } 
 private string path { get; set; } 
 private List<string> data { get; set; } = D3EPersistanceList.primitive(DATA) ;
 
 private bool failed { get; set; } = false ;
 
 private List<UserDevice> failedDevices { get; set; } = D3EPersistanceList.reference(FAILEDDEVICES) ;
 
 public PushNotification (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.PushNotification ;
 }
 public string Type (  ) {
 return "PushNotification" ;
 }
 public int FieldsCount (  ) {
 return 9 ;
 }
 public void AddToUsers (  BaseUser val, long index ) {
 if ( index == -1 ) {
 users.Add(val) ;
 }
 else {
 users.Add(((int)index),val) ;
 }
 }
 public void RemoveFromUsers (  BaseUser val ) {
 users.Remove(val) ;
 }
 public void AddToData (  string val, long index ) {
 if ( index == -1 ) {
 data.Add(val) ;
 }
 else {
 data.Add(((int)index),val) ;
 }
 }
 public void RemoveFromData (  string val ) {
 data.Remove(val) ;
 }
 public void AddToFailedDevices (  UserDevice val, long index ) {
 if ( index == -1 ) {
 failedDevices.Add(val) ;
 }
 else {
 failedDevices.Add(((int)index),val) ;
 }
 }
 public void RemoveFromFailedDevices (  UserDevice val ) {
 failedDevices.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public List<BaseUser> Users (  ) {
 return this.users ;
 }
 public void Users (  List<BaseUser> users ) {
 if ( Objects.Equals(this.users,users) ) {
 return ;
 }
 ((D3EPersistanceList < BaseUser >)this.users).SetAll(users) ;
 }
 public bool IsSkipThisDevice (  ) {
 _CheckProxy() ;
 return this.skipThisDevice ;
 }
 public void SkipThisDevice (  bool skipThisDevice ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.skipThisDevice,skipThisDevice) ) {
 return ;
 }
 fieldChanged(SKIPTHISDEVICE,this.skipThisDevice,skipThisDevice) ;
 this.skipThisDevice = skipThisDevice ;
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
 public string Title (  ) {
 _CheckProxy() ;
 return this.title ;
 }
 public void Title (  string title ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.title,title) ) {
 return ;
 }
 fieldChanged(TITLE,this.title,title) ;
 this.title = title ;
 }
 public string Body (  ) {
 _CheckProxy() ;
 return this.body ;
 }
 public void Body (  string body ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.body,body) ) {
 return ;
 }
 fieldChanged(BODY,this.body,body) ;
 this.body = body ;
 }
 public string Path (  ) {
 _CheckProxy() ;
 return this.path ;
 }
 public void Path (  string path ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.path,path) ) {
 return ;
 }
 fieldChanged(PATH,this.path,path) ;
 this.path = path ;
 }
 public List<string> Data (  ) {
 return this.data ;
 }
 public void Data (  List<string> data ) {
 if ( Objects.Equals(this.data,data) ) {
 return ;
 }
 ((D3EPersistanceList < string >)this.data).SetAll(data) ;
 }
 public bool IsFailed (  ) {
 _CheckProxy() ;
 return this.failed ;
 }
 public void Failed (  bool failed ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.failed,failed) ) {
 return ;
 }
 fieldChanged(FAILED,this.failed,failed) ;
 this.failed = failed ;
 }
 public List<UserDevice> FailedDevices (  ) {
 return this.failedDevices ;
 }
 public void FailedDevices (  List<UserDevice> failedDevices ) {
 if ( Objects.Equals(this.failedDevices,failedDevices) ) {
 return ;
 }
 ((D3EPersistanceList < UserDevice >)this.failedDevices).SetAll(failedDevices) ;
 }
 public string DisplayName (  ) {
 return "PushNotification" ;
 }
 public bool equals (  Object a ) {
 return a is PushNotification && base.Equals(a) ;
 }
 public PushNotification DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  PushNotification _obj=((PushNotification)dbObj);
 _obj.Users(users) ;
 _obj.SkipThisDevice(skipThisDevice) ;
 _obj.DeviceToken(deviceToken) ;
 _obj.Title(title) ;
 _obj.Body(body) ;
 _obj.Path(path) ;
 _obj.Data(data) ;
 _obj.Failed(failed) ;
 _obj.FailedDevices(failedDevices) ;
 }
 public PushNotification CloneInstance (  PushNotification cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new PushNotification() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Users(new ArrayList<>(Users())) ;
 cloneObj.SkipThisDevice(this.IsSkipThisDevice()) ;
 cloneObj.DeviceToken(this.DeviceToken()) ;
 cloneObj.Title(this.Title()) ;
 cloneObj.Body(this.Body()) ;
 cloneObj.Path(this.Path()) ;
 cloneObj.Data(new ArrayList<>(Data())) ;
 cloneObj.Failed(this.IsFailed()) ;
 cloneObj.FailedDevices(new ArrayList<>(FailedDevices())) ;
 return cloneObj ;
 }
 public PushNotification CreateNewInstance (  ) {
 return new PushNotification() ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 _refs.AddAll(this.users) ;
 _refs.AddAll(this.failedDevices) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }