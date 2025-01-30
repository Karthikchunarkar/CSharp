namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportConfig :  DatabaseObject { public static int IDENTITY = 0 ;
 
 public static int VALUES = 1 ;
 
 private string identity { get; set; } 
 private List<ReportConfigOption> values { get; set; } = D3EPersistanceList.child(VALUES) ;
 
 private ReportConfig Old { get; set; } 
 public ReportConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportConfig ;
 }
 public string Type (  ) {
 return "ReportConfig" ;
 }
 public int FieldsCount (  ) {
 return 2 ;
 }
 public void AddToValues (  ReportConfigOption val, long index ) {
 if ( index == -1 ) {
 values.Add(val) ;
 }
 else {
 values.Add(((int)index),val) ;
 }
 }
 public void RemoveFromValues (  ReportConfigOption val ) {
 val._clearChildIdx() ;
 values.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 foreach ( ReportConfigOption obj in this.Values() ) {
 visitor.accept(obj) ;
 obj.MasterReportConfig(this) ;
 obj.SetChildIdx(VALUES) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportConfigOption obj in this.Values() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public string Identity (  ) {
 _CheckProxy() ;
 return this.identity ;
 }
 public void Identity (  string identity ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.identity,identity) ) {
 return ;
 }
 fieldChanged(IDENTITY,this.identity,identity) ;
 this.identity = identity ;
 }
 public List<ReportConfigOption> Values (  ) {
 return this.values ;
 }
 public void Values (  List<ReportConfigOption> values ) {
 if ( Objects.Equals(this.values,values) ) {
 return ;
 }
 ((D3EPersistanceList < ReportConfigOption >)this.values).SetAll(values) ;
 }
 public ReportConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.Values().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public string DisplayName (  ) {
 return "ReportConfig" ;
 }
 public bool equals (  Object a ) {
 return a is ReportConfig && base.Equals(a) ;
 }
 public ReportConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(values) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportConfig _obj=((ReportConfig)dbObj);
 _obj.Identity(identity) ;
 ctx.cloneChildList(values,(  v ) => _obj.Values(v)) ;
 }
 public ReportConfig CloneInstance (  ReportConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Identity(this.Identity()) ;
 cloneObj.Values(Values().Stream().Dictionary((  ReportConfigOption colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportConfig CreateNewInstance (  ) {
 return new ReportConfig() ;
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
 } }
 }