namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportMultiRowCardConfig :  ReportBaseConfig { public static int VALUES = 0 ;
 
 private List<ReportField> values { get; set; } = D3EPersistanceList.child(VALUES) ;
 
 private ReportMultiRowCardConfig Old { get; set; } 
 public ReportMultiRowCardConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportMultiRowCardConfig ;
 }
 public string Type (  ) {
 return "ReportMultiRowCardConfig" ;
 }
 public int FieldsCount (  ) {
 return 1 ;
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
 foreach ( ReportField obj in this.Values() ) {
 visitor.accept(obj) ;
 obj.MasterReportMultiRowCardConfig(this) ;
 obj.SetChildIdx(VALUES) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportField obj in this.Values() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
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
 public ReportMultiRowCardConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportMultiRowCardConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.Values().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportMultiRowCardConfig && base.Equals(a) ;
 }
 public ReportMultiRowCardConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(values) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportMultiRowCardConfig _obj=((ReportMultiRowCardConfig)dbObj);
 ctx.cloneChildList(values,(  v ) => _obj.Values(v)) ;
 }
 public ReportMultiRowCardConfig CloneInstance (  ReportMultiRowCardConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportMultiRowCardConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Values(Values().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportMultiRowCardConfig CreateNewInstance (  ) {
 return new ReportMultiRowCardConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.values) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case VALUES: {
 this.ChildCollFieldChanged(childIdx,set,values) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }