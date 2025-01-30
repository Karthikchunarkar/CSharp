namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportKPIConfig :  ReportBaseConfig { public static int VALUE = 0 ;
 
 public static int TARGET = 1 ;
 
 public static int TREND = 2 ;
 
 private ReportField value { get; set; } 
 private List<ReportField> target { get; set; } = D3EPersistanceList.child(TARGET) ;
 
 private ReportField trend { get; set; } 
 private ReportKPIConfig Old { get; set; } 
 public ReportKPIConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportKPIConfig ;
 }
 public string Type (  ) {
 return "ReportKPIConfig" ;
 }
 public int FieldsCount (  ) {
 return 3 ;
 }
 public void AddToTarget (  ReportField val, long index ) {
 if ( index == -1 ) {
 target.Add(val) ;
 }
 else {
 target.Add(((int)index),val) ;
 }
 }
 public void RemoveFromTarget (  ReportField val ) {
 val._clearChildIdx() ;
 target.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 if ( value != null ) {
 visitor.accept(value) ;
 value.MasterReportKPIConfig(this) ;
 value.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Target() ) {
 visitor.accept(obj) ;
 obj.MasterReportKPIConfig(this) ;
 obj.SetChildIdx(TARGET) ;
 obj.UpdateMasters(visitor) ;
 }
 if ( trend != null ) {
 visitor.accept(trend) ;
 trend.MasterReportKPIConfig(this) ;
 trend.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 if ( value != null ) {
 visitor.accept(value) ;
 value.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Target() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 if ( trend != null ) {
 visitor.accept(trend) ;
 trend.VisitChildren(visitor) ;
 }
 }
 public ReportField Value (  ) {
 _CheckProxy() ;
 return this.value ;
 }
 public void Value (  ReportField value ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.value,value) ) {
 if ( this.value != null ) {
 this.value._updateChanges() ;
 }
 return ;
 }
 fieldChanged(VALUE,this.value,value) ;
 this.value = value ;
 if ( this.value != null ) {
 this.value.setMasterReportKPIConfig(this) ;
 this.value._setChildIdx(VALUE) ;
 this.value._updateChanges() ;
 }
 }
 public List<ReportField> Target (  ) {
 return this.target ;
 }
 public void Target (  List<ReportField> target ) {
 if ( Objects.Equals(this.target,target) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.target).SetAll(target) ;
 }
 public ReportField Trend (  ) {
 _CheckProxy() ;
 return this.trend ;
 }
 public void Trend (  ReportField trend ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.trend,trend) ) {
 if ( this.trend != null ) {
 this.trend._updateChanges() ;
 }
 return ;
 }
 fieldChanged(TREND,this.trend,trend) ;
 this.trend = trend ;
 if ( this.trend != null ) {
 this.trend.setMasterReportKPIConfig(this) ;
 this.trend._setChildIdx(TREND) ;
 this.trend._updateChanges() ;
 }
 }
 public ReportKPIConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportKPIConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 if ( this.Value() != null ) {
 this.Value().recordOld(ctx) ;
 }
 this.Target().forEach((  one ) => one.recordOld(ctx)) ;
 if ( this.Trend() != null ) {
 this.Trend().recordOld(ctx) ;
 }
 }
 public bool equals (  Object a ) {
 return a is ReportKPIConfig && base.Equals(a) ;
 }
 public ReportKPIConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChild(value) ;
 ctx.CollectChilds(target) ;
 ctx.CollectChild(trend) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportKPIConfig _obj=((ReportKPIConfig)dbObj);
 ctx.cloneChild(value,(  v ) => _obj.Value(v)) ;
 ctx.cloneChildList(target,(  v ) => _obj.Target(v)) ;
 ctx.cloneChild(trend,(  v ) => _obj.Trend(v)) ;
 }
 public ReportKPIConfig CloneInstance (  ReportKPIConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportKPIConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Value(Value() == null ? null : Value().CloneInstance(null)) ;
 cloneObj.Target(Target().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Trend(Trend() == null ? null : Trend().CloneInstance(null)) ;
 return cloneObj ;
 }
 public ReportKPIConfig CreateNewInstance (  ) {
 return new ReportKPIConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.collectCreatableReferences(_refs,this.value) ;
 Database.CollectCollctionCreatableReferences(_refs,this.target) ;
 Database.collectCreatableReferences(_refs,this.trend) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case VALUE: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 case TARGET: {
 this.ChildCollFieldChanged(childIdx,set,target) ;
 break; }
 case TREND: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }