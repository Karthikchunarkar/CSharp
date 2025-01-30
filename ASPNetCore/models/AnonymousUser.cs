namespace models ;
 using d3e.core; using java.util.function; using store;  public class AnonymousUser :  BaseUser { public AnonymousUser (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.AnonymousUser ;
 }
 public string Type (  ) {
 return "AnonymousUser" ;
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
 public bool equals (  Object a ) {
 return a is AnonymousUser && base.Equals(a) ;
 }
 public AnonymousUser DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public AnonymousUser CloneInstance (  AnonymousUser cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new AnonymousUser() ;
 }
 base.CloneInstance(cloneObj) ;
 return cloneObj ;
 }
 public AnonymousUser CreateNewInstance (  ) {
 return new AnonymousUser() ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 base.HandleChildChange(childIdx,set) ;
 }
 }