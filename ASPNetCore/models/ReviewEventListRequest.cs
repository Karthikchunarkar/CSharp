namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class ReviewEventListRequest :  CreatableObject { public static int REFERENCEID = 0 ;
 
 private string referenceID { get; set; } 
 public ReviewEventListRequest (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReviewEventListRequest ;
 }
 public string Type (  ) {
 return "ReviewEventListRequest" ;
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
 public string ReferenceID (  ) {
 _CheckProxy() ;
 return this.referenceID ;
 }
 public void ReferenceID (  string referenceID ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.referenceID,referenceID) ) {
 return ;
 }
 fieldChanged(REFERENCEID,this.referenceID,referenceID) ;
 this.referenceID = referenceID ;
 }
 public string DisplayName (  ) {
 return "ReviewEventListRequest" ;
 }
 public bool equals (  Object a ) {
 return a is ReviewEventListRequest && base.Equals(a) ;
 }
 public ReviewEventListRequest DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReviewEventListRequest _obj=((ReviewEventListRequest)dbObj);
 _obj.ReferenceID(referenceID) ;
 }
 public ReviewEventListRequest CloneInstance (  ReviewEventListRequest cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReviewEventListRequest() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.ReferenceID(this.ReferenceID()) ;
 return cloneObj ;
 }
 public bool TransientModel (  ) {
 return true ;
 }
 public ReviewEventListRequest CreateNewInstance (  ) {
 return new ReviewEventListRequest() ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }