namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class AllDevicesRequest :  CreatableObject { public static int USERS = 0 ;
 
 private List<BaseUser> users { get; set; } = D3EPersistanceList.reference(USERS) ;
 
 public AllDevicesRequest (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.AllDevicesRequest ;
 }
 public string Type (  ) {
 return "AllDevicesRequest" ;
 }
 public int FieldsCount (  ) {
 return 1 ;
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
 public string DisplayName (  ) {
 return "AllDevicesRequest" ;
 }
 public bool equals (  Object a ) {
 return a is AllDevicesRequest && base.Equals(a) ;
 }
 public AllDevicesRequest DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  AllDevicesRequest _obj=((AllDevicesRequest)dbObj);
 _obj.Users(users) ;
 }
 public AllDevicesRequest CloneInstance (  AllDevicesRequest cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new AllDevicesRequest() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Users(new ArrayList<>(Users())) ;
 return cloneObj ;
 }
 public bool TransientModel (  ) {
 return true ;
 }
 public AllDevicesRequest CreateNewInstance (  ) {
 return new AllDevicesRequest() ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 _refs.AddAll(this.users) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }