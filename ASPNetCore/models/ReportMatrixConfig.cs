namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportMatrixConfig :  ReportBaseConfig { public static int COLUMNS = 0 ;
 
 public static int ROWS = 1 ;
 
 public static int VALUES = 2 ;
 
 private List<ReportField> columns { get; set; } = D3EPersistanceList.child(COLUMNS) ;
 
 private List<ReportField> rows { get; set; } = D3EPersistanceList.child(ROWS) ;
 
 private List<ReportField> values { get; set; } = D3EPersistanceList.child(VALUES) ;
 
 private ReportMatrixConfig Old { get; set; } 
 public ReportMatrixConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportMatrixConfig ;
 }
 public string Type (  ) {
 return "ReportMatrixConfig" ;
 }
 public int FieldsCount (  ) {
 return 3 ;
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
 public void AddToRows (  ReportField val, long index ) {
 if ( index == -1 ) {
 rows.Add(val) ;
 }
 else {
 rows.Add(((int)index),val) ;
 }
 }
 public void RemoveFromRows (  ReportField val ) {
 val._clearChildIdx() ;
 rows.Remove(val) ;
 }
 public void AddToValues (  ReportField val, long index ) {
 if ( index == -1 ) {
 values.Add(val) ;
 }
 else {
 values.Add(((int)index),val) ;
 }
 }
 public void RemoveFromValues (  ReportField val ) {
 val._clearChildIdx() ;
 values.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 foreach ( ReportField obj in this.Columns() ) {
 visitor.accept(obj) ;
 obj.MasterReportMatrixConfig(this) ;
 obj.SetChildIdx(COLUMNS) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Rows() ) {
 visitor.accept(obj) ;
 obj.MasterReportMatrixConfig(this) ;
 obj.SetChildIdx(ROWS) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Values() ) {
 visitor.accept(obj) ;
 obj.MasterReportMatrixConfig(this) ;
 obj.SetChildIdx(VALUES) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportField obj in this.Columns() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Rows() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Values() ) {
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
 public List<ReportField> Rows (  ) {
 return this.rows ;
 }
 public void Rows (  List<ReportField> rows ) {
 if ( Objects.Equals(this.rows,rows) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.rows).SetAll(rows) ;
 }
 public List<ReportField> Values (  ) {
 return this.values ;
 }
 public void Values (  List<ReportField> values ) {
 if ( Objects.Equals(this.values,values) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.values).SetAll(values) ;
 }
 public ReportMatrixConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportMatrixConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.Columns().forEach((  one ) => one.recordOld(ctx)) ;
 this.Rows().forEach((  one ) => one.recordOld(ctx)) ;
 this.Values().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportMatrixConfig && base.Equals(a) ;
 }
 public ReportMatrixConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(columns) ;
 ctx.CollectChilds(rows) ;
 ctx.CollectChilds(values) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportMatrixConfig _obj=((ReportMatrixConfig)dbObj);
 ctx.cloneChildList(columns,(  v ) => _obj.Columns(v)) ;
 ctx.cloneChildList(rows,(  v ) => _obj.Rows(v)) ;
 ctx.cloneChildList(values,(  v ) => _obj.Values(v)) ;
 }
 public ReportMatrixConfig CloneInstance (  ReportMatrixConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportMatrixConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Columns(Columns().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Rows(Rows().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Values(Values().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportMatrixConfig CreateNewInstance (  ) {
 return new ReportMatrixConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.columns) ;
 Database.CollectCollctionCreatableReferences(_refs,this.rows) ;
 Database.CollectCollctionCreatableReferences(_refs,this.values) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case COLUMNS: {
 this.ChildCollFieldChanged(childIdx,set,columns) ;
 break; }
 case ROWS: {
 this.ChildCollFieldChanged(childIdx,set,rows) ;
 break; }
 case VALUES: {
 this.ChildCollFieldChanged(childIdx,set,values) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }