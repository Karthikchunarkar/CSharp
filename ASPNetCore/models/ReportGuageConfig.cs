namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportGuageConfig :  ReportBaseConfig { public static int VALUE = 0 ;
 
 public static int MIN = 1 ;
 
 public static int MAX = 2 ;
 
 public static int TARGET = 3 ;
 
 public static int TOOLTIPS = 4 ;
 
 private List<ReportField> value { get; set; } = D3EPersistanceList.child(VALUE) ;
 
 private List<ReportField> min { get; set; } = D3EPersistanceList.child(MIN) ;
 
 private List<ReportField> max { get; set; } = D3EPersistanceList.child(MAX) ;
 
 private List<ReportField> target { get; set; } = D3EPersistanceList.child(TARGET) ;
 
 private List<ReportField> tooltips { get; set; } = D3EPersistanceList.child(TOOLTIPS) ;
 
 private ReportGuageConfig Old { get; set; } 
 public ReportGuageConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportGuageConfig ;
 }
 public string Type (  ) {
 return "ReportGuageConfig" ;
 }
 public int FieldsCount (  ) {
 return 5 ;
 }
 public void AddToValue (  ReportField val, long index ) {
 if ( index == -1 ) {
 value.Add(val) ;
 }
 else {
 value.Add(((int)index),val) ;
 }
 }
 public void RemoveFromValue (  ReportField val ) {
 val._clearChildIdx() ;
 value.Remove(val) ;
 }
 public void AddToMin (  ReportField val, long index ) {
 if ( index == -1 ) {
 min.Add(val) ;
 }
 else {
 min.Add(((int)index),val) ;
 }
 }
 public void RemoveFromMin (  ReportField val ) {
 val._clearChildIdx() ;
 min.Remove(val) ;
 }
 public void AddToMax (  ReportField val, long index ) {
 if ( index == -1 ) {
 max.Add(val) ;
 }
 else {
 max.Add(((int)index),val) ;
 }
 }
 public void RemoveFromMax (  ReportField val ) {
 val._clearChildIdx() ;
 max.Remove(val) ;
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
 public void AddToTooltips (  ReportField val, long index ) {
 if ( index == -1 ) {
 tooltips.Add(val) ;
 }
 else {
 tooltips.Add(((int)index),val) ;
 }
 }
 public void RemoveFromTooltips (  ReportField val ) {
 val._clearChildIdx() ;
 tooltips.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 foreach ( ReportField obj in this.Value() ) {
 visitor.accept(obj) ;
 obj.MasterReportGuageConfig(this) ;
 obj.SetChildIdx(VALUE) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Min() ) {
 visitor.accept(obj) ;
 obj.MasterReportGuageConfig(this) ;
 obj.SetChildIdx(MIN) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Max() ) {
 visitor.accept(obj) ;
 obj.MasterReportGuageConfig(this) ;
 obj.SetChildIdx(MAX) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Target() ) {
 visitor.accept(obj) ;
 obj.MasterReportGuageConfig(this) ;
 obj.SetChildIdx(TARGET) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.MasterReportGuageConfig(this) ;
 obj.SetChildIdx(TOOLTIPS) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportField obj in this.Value() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Min() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Max() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Target() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public List<ReportField> Value (  ) {
 return this.value ;
 }
 public void Value (  List<ReportField> value ) {
 if ( Objects.Equals(this.value,value) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.value).SetAll(value) ;
 }
 public List<ReportField> Min (  ) {
 return this.min ;
 }
 public void Min (  List<ReportField> min ) {
 if ( Objects.Equals(this.min,min) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.min).SetAll(min) ;
 }
 public List<ReportField> Max (  ) {
 return this.max ;
 }
 public void Max (  List<ReportField> max ) {
 if ( Objects.Equals(this.max,max) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.max).SetAll(max) ;
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
 public List<ReportField> Tooltips (  ) {
 return this.tooltips ;
 }
 public void Tooltips (  List<ReportField> tooltips ) {
 if ( Objects.Equals(this.tooltips,tooltips) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.tooltips).SetAll(tooltips) ;
 }
 public ReportGuageConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportGuageConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.Value().forEach((  one ) => one.recordOld(ctx)) ;
 this.Min().forEach((  one ) => one.recordOld(ctx)) ;
 this.Max().forEach((  one ) => one.recordOld(ctx)) ;
 this.Target().forEach((  one ) => one.recordOld(ctx)) ;
 this.Tooltips().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportGuageConfig && base.Equals(a) ;
 }
 public ReportGuageConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(value) ;
 ctx.CollectChilds(min) ;
 ctx.CollectChilds(max) ;
 ctx.CollectChilds(target) ;
 ctx.CollectChilds(tooltips) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportGuageConfig _obj=((ReportGuageConfig)dbObj);
 ctx.cloneChildList(value,(  v ) => _obj.Value(v)) ;
 ctx.cloneChildList(min,(  v ) => _obj.Min(v)) ;
 ctx.cloneChildList(max,(  v ) => _obj.Max(v)) ;
 ctx.cloneChildList(target,(  v ) => _obj.Target(v)) ;
 ctx.cloneChildList(tooltips,(  v ) => _obj.Tooltips(v)) ;
 }
 public ReportGuageConfig CloneInstance (  ReportGuageConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportGuageConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Value(Value().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Min(Min().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Max(Max().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Target(Target().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Tooltips(Tooltips().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportGuageConfig CreateNewInstance (  ) {
 return new ReportGuageConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.value) ;
 Database.CollectCollctionCreatableReferences(_refs,this.min) ;
 Database.CollectCollctionCreatableReferences(_refs,this.max) ;
 Database.CollectCollctionCreatableReferences(_refs,this.target) ;
 Database.CollectCollctionCreatableReferences(_refs,this.tooltips) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case VALUE: {
 this.ChildCollFieldChanged(childIdx,set,value) ;
 break; }
 case MIN: {
 this.ChildCollFieldChanged(childIdx,set,min) ;
 break; }
 case MAX: {
 this.ChildCollFieldChanged(childIdx,set,max) ;
 break; }
 case TARGET: {
 this.ChildCollFieldChanged(childIdx,set,target) ;
 break; }
 case TOOLTIPS: {
 this.ChildCollFieldChanged(childIdx,set,tooltips) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }