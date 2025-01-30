namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportSlicerConfig :  ReportBaseConfig { public static int FIELDS = 0 ;
 
 private List<ReportField> fields { get; set; } = D3EPersistanceList.child(FIELDS) ;
 
 private ReportSlicerConfig Old { get; set; } 
 public ReportSlicerConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportSlicerConfig ;
 }
 public string Type (  ) {
 return "ReportSlicerConfig" ;
 }
 public int FieldsCount (  ) {
 return 1 ;
 }
 public void AddToFields (  ReportField val, long index ) {
 if ( index == -1 ) {
 fields.Add(val) ;
 }
 else {
 fields.Add(((int)index),val) ;
 }
 }
 public void RemoveFromFields (  ReportField val ) {
 val._clearChildIdx() ;
 fields.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 foreach ( ReportField obj in this.Fields() ) {
 visitor.accept(obj) ;
 obj.MasterReportSlicerConfig(this) ;
 obj.SetChildIdx(FIELDS) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportField obj in this.Fields() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public List<ReportField> Fields (  ) {
 return this.fields ;
 }
 public void Fields (  List<ReportField> fields ) {
 if ( Objects.Equals(this.fields,fields) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.fields).SetAll(fields) ;
 }
 public ReportSlicerConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportSlicerConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.Fields().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportSlicerConfig && base.Equals(a) ;
 }
 public ReportSlicerConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(fields) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportSlicerConfig _obj=((ReportSlicerConfig)dbObj);
 ctx.cloneChildList(fields,(  v ) => _obj.Fields(v)) ;
 }
 public ReportSlicerConfig CloneInstance (  ReportSlicerConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportSlicerConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Fields(Fields().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportSlicerConfig CreateNewInstance (  ) {
 return new ReportSlicerConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.fields) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case FIELDS: {
 this.ChildCollFieldChanged(childIdx,set,fields) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }