namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportTableConfig :  ReportBaseConfig { public static int COLUMNS = 0 ;
 
 private List<ReportField> columns { get; set; } = D3EPersistanceList.child(COLUMNS) ;
 
 private ReportTableConfig Old { get; set; } 
 public ReportTableConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportTableConfig ;
 }
 public string Type (  ) {
 return "ReportTableConfig" ;
 }
 public int FieldsCount (  ) {
 return 1 ;
 }
 public void AddToColumns (  ReportField val, long index ) {
 if ( index == -1 ) {
 columns.Add(val) ;
 }
 else {
 columns.Add(((int)index),val) ;
 }
 }
 public void RemoveFromColumns (  ReportField val ) {
 val._clearChildIdx() ;
 columns.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 foreach ( ReportField obj in this.Columns() ) {
 visitor.accept(obj) ;
 obj.MasterReportTableConfig(this) ;
 obj.SetChildIdx(COLUMNS) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportField obj in this.Columns() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public List<ReportField> Columns (  ) {
 return this.columns ;
 }
 public void Columns (  List<ReportField> columns ) {
 if ( Objects.Equals(this.columns,columns) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.columns).SetAll(columns) ;
 }
 public ReportTableConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportTableConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.Columns().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportTableConfig && base.Equals(a) ;
 }
 public ReportTableConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(columns) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportTableConfig _obj=((ReportTableConfig)dbObj);
 ctx.cloneChildList(columns,(  v ) => _obj.Columns(v)) ;
 }
 public ReportTableConfig CloneInstance (  ReportTableConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportTableConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Columns(Columns().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportTableConfig CreateNewInstance (  ) {
 return new ReportTableConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.columns) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case COLUMNS: {
 this.ChildCollFieldChanged(childIdx,set,columns) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }