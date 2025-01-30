namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class CategoriesListRequest :  CreatableObject { public static int PAGESIZE = 0 ;
 
 public static int OFFSET = 1 ;
 
 public static int ORDERBY = 2 ;
 
 public static int ASCENDING = 3 ;
 
 private int pageSize { get; set; } = 0 ;
 
 private int offset { get; set; } = 0 ;
 
 private string orderBy { get; set; } 
 private bool ascending { get; set; } = false ;
 
 public CategoriesListRequest (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.CategoriesListRequest ;
 }
 public string Type (  ) {
 return "CategoriesListRequest" ;
 }
 public int FieldsCount (  ) {
 return 4 ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public int PageSize (  ) {
 _CheckProxy() ;
 return this.pageSize ;
 }
 public void PageSize (  int pageSize ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.pageSize,pageSize) ) {
 return ;
 }
 fieldChanged(PAGESIZE,this.pageSize,pageSize) ;
 this.pageSize = pageSize ;
 }
 public int Offset (  ) {
 _CheckProxy() ;
 return this.offset ;
 }
 public void Offset (  int offset ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.offset,offset) ) {
 return ;
 }
 fieldChanged(OFFSET,this.offset,offset) ;
 this.offset = offset ;
 }
 public string OrderBy (  ) {
 _CheckProxy() ;
 return this.orderBy ;
 }
 public void OrderBy (  string orderBy ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.orderBy,orderBy) ) {
 return ;
 }
 fieldChanged(ORDERBY,this.orderBy,orderBy) ;
 this.orderBy = orderBy ;
 }
 public bool IsAscending (  ) {
 _CheckProxy() ;
 return this.ascending ;
 }
 public void Ascending (  bool ascending ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.ascending,ascending) ) {
 return ;
 }
 fieldChanged(ASCENDING,this.ascending,ascending) ;
 this.ascending = ascending ;
 }
 public string DisplayName (  ) {
 return "CategoriesListRequest" ;
 }
 public bool equals (  Object a ) {
 return a is CategoriesListRequest && base.Equals(a) ;
 }
 public CategoriesListRequest DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  CategoriesListRequest _obj=((CategoriesListRequest)dbObj);
 _obj.PageSize(pageSize) ;
 _obj.Offset(offset) ;
 _obj.OrderBy(orderBy) ;
 _obj.Ascending(ascending) ;
 }
 public CategoriesListRequest CloneInstance (  CategoriesListRequest cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new CategoriesListRequest() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.PageSize(this.PageSize()) ;
 cloneObj.Offset(this.Offset()) ;
 cloneObj.OrderBy(this.OrderBy()) ;
 cloneObj.Ascending(this.IsAscending()) ;
 return cloneObj ;
 }
 public bool TransientModel (  ) {
 return true ;
 }
 public CategoriesListRequest CreateNewInstance (  ) {
 return new CategoriesListRequest() ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }