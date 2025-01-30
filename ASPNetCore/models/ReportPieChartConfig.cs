namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportPieChartConfig :  ReportBaseConfig { public static int TYPE = 0 ;
 
 public static int LEGEND = 1 ;
 
 public static int VALUES = 2 ;
 
 public static int DETAILS = 3 ;
 
 public static int TOOLTIPS = 4 ;
 
 private ReportPieChartType type { get; set; } = ReportPieChartType.Pie ;
 
 private List<ReportField> legend { get; set; } = D3EPersistanceList.child(LEGEND) ;
 
 private List<ReportField> values { get; set; } = D3EPersistanceList.child(VALUES) ;
 
 private List<ReportField> details { get; set; } = D3EPersistanceList.child(DETAILS) ;
 
 private List<ReportField> tooltips { get; set; } = D3EPersistanceList.child(TOOLTIPS) ;
 
 private ReportPieChartConfig Old { get; set; } 
 public ReportPieChartConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportPieChartConfig ;
 }
 public string Type (  ) {
 return "ReportPieChartConfig" ;
 }
 public int FieldsCount (  ) {
 return 5 ;
 }
 public void AddToLegend (  ReportField val, long index ) {
 if ( index == -1 ) {
 legend.Add(val) ;
 }
 else {
 legend.Add(((int)index),val) ;
 }
 }
 public void RemoveFromLegend (  ReportField val ) {
 val._clearChildIdx() ;
 legend.Remove(val) ;
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
 public void AddToDetails (  ReportField val, long index ) {
 if ( index == -1 ) {
 details.Add(val) ;
 }
 else {
 details.Add(((int)index),val) ;
 }
 }
 public void RemoveFromDetails (  ReportField val ) {
 val._clearChildIdx() ;
 details.Remove(val) ;
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
 foreach ( ReportField obj in this.Legend() ) {
 visitor.accept(obj) ;
 obj.MasterReportPieChartConfig(this) ;
 obj.SetChildIdx(LEGEND) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Values() ) {
 visitor.accept(obj) ;
 obj.MasterReportPieChartConfig(this) ;
 obj.SetChildIdx(VALUES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Details() ) {
 visitor.accept(obj) ;
 obj.MasterReportPieChartConfig(this) ;
 obj.SetChildIdx(DETAILS) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.MasterReportPieChartConfig(this) ;
 obj.SetChildIdx(TOOLTIPS) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportField obj in this.Legend() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Values() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Details() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public ReportPieChartType Type (  ) {
 _CheckProxy() ;
 return this.type ;
 }
 public void Type (  ReportPieChartType type ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.type,type) ) {
 return ;
 }
 fieldChanged(TYPE,this.type,type) ;
 this.type = type ;
 }
 public List<ReportField> Legend (  ) {
 return this.legend ;
 }
 public void Legend (  List<ReportField> legend ) {
 if ( Objects.Equals(this.legend,legend) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.legend).SetAll(legend) ;
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
 public List<ReportField> Details (  ) {
 return this.details ;
 }
 public void Details (  List<ReportField> details ) {
 if ( Objects.Equals(this.details,details) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.details).SetAll(details) ;
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
 public ReportPieChartConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportPieChartConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.Legend().forEach((  one ) => one.recordOld(ctx)) ;
 this.Values().forEach((  one ) => one.recordOld(ctx)) ;
 this.Details().forEach((  one ) => one.recordOld(ctx)) ;
 this.Tooltips().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportPieChartConfig && base.Equals(a) ;
 }
 public ReportPieChartConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(legend) ;
 ctx.CollectChilds(values) ;
 ctx.CollectChilds(details) ;
 ctx.CollectChilds(tooltips) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportPieChartConfig _obj=((ReportPieChartConfig)dbObj);
 _obj.Type(type) ;
 ctx.cloneChildList(legend,(  v ) => _obj.Legend(v)) ;
 ctx.cloneChildList(values,(  v ) => _obj.Values(v)) ;
 ctx.cloneChildList(details,(  v ) => _obj.Details(v)) ;
 ctx.cloneChildList(tooltips,(  v ) => _obj.Tooltips(v)) ;
 }
 public ReportPieChartConfig CloneInstance (  ReportPieChartConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportPieChartConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Type(this.Type()) ;
 cloneObj.Legend(Legend().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Values(Values().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Details(Details().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Tooltips(Tooltips().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportPieChartConfig CreateNewInstance (  ) {
 return new ReportPieChartConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.legend) ;
 Database.CollectCollctionCreatableReferences(_refs,this.values) ;
 Database.CollectCollctionCreatableReferences(_refs,this.details) ;
 Database.CollectCollctionCreatableReferences(_refs,this.tooltips) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case LEGEND: {
 this.ChildCollFieldChanged(childIdx,set,legend) ;
 break; }
 case VALUES: {
 this.ChildCollFieldChanged(childIdx,set,values) ;
 break; }
 case DETAILS: {
 this.ChildCollFieldChanged(childIdx,set,details) ;
 break; }
 case TOOLTIPS: {
 this.ChildCollFieldChanged(childIdx,set,tooltips) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }