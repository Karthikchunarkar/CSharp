namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportWaterfallChartConfig :  ReportBaseConfig { public static int CATEGORY = 0 ;
 
 public static int BREAKDOWNFIELDS = 1 ;
 
 public static int YAXES = 2 ;
 
 public static int TOOLTIPS = 3 ;
 
 private List<ReportField> category { get; set; } = D3EPersistanceList.child(CATEGORY) ;
 
 private List<ReportField> breakdownFields { get; set; } = D3EPersistanceList.child(BREAKDOWNFIELDS) ;
 
 private List<ReportField> yAxes { get; set; } = D3EPersistanceList.child(YAXES) ;
 
 private List<ReportField> tooltips { get; set; } = D3EPersistanceList.child(TOOLTIPS) ;
 
 private ReportWaterfallChartConfig Old { get; set; } 
 public ReportWaterfallChartConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportWaterfallChartConfig ;
 }
 public string Type (  ) {
 return "ReportWaterfallChartConfig" ;
 }
 public int FieldsCount (  ) {
 return 4 ;
 }
 public void AddToCategory (  ReportField val, long index ) {
 if ( index == -1 ) {
 category.Add(val) ;
 }
 else {
 category.Add(((int)index),val) ;
 }
 }
 public void RemoveFromCategory (  ReportField val ) {
 val._clearChildIdx() ;
 category.Remove(val) ;
 }
 public void AddToBreakdownFields (  ReportField val, long index ) {
 if ( index == -1 ) {
 breakdownFields.Add(val) ;
 }
 else {
 breakdownFields.Add(((int)index),val) ;
 }
 }
 public void RemoveFromBreakdownFields (  ReportField val ) {
 val._clearChildIdx() ;
 breakdownFields.Remove(val) ;
 }
 public void AddToYAxes (  ReportField val, long index ) {
 if ( index == -1 ) {
 yAxes.Add(val) ;
 }
 else {
 yAxes.Add(((int)index),val) ;
 }
 }
 public void RemoveFromYAxes (  ReportField val ) {
 val._clearChildIdx() ;
 yAxes.Remove(val) ;
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
 foreach ( ReportField obj in this.Category() ) {
 visitor.accept(obj) ;
 obj.MasterReportWaterfallChartConfig(this) ;
 obj.SetChildIdx(CATEGORY) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.BreakdownFields() ) {
 visitor.accept(obj) ;
 obj.MasterReportWaterfallChartConfig(this) ;
 obj.SetChildIdx(BREAKDOWNFIELDS) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.YAxes() ) {
 visitor.accept(obj) ;
 obj.MasterReportWaterfallChartConfig(this) ;
 obj.SetChildIdx(YAXES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.MasterReportWaterfallChartConfig(this) ;
 obj.SetChildIdx(TOOLTIPS) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportField obj in this.Category() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.BreakdownFields() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.YAxes() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public List<ReportField> Category (  ) {
 return this.category ;
 }
 public void Category (  List<ReportField> category ) {
 if ( Objects.Equals(this.category,category) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.category).SetAll(category) ;
 }
 public List<ReportField> BreakdownFields (  ) {
 return this.breakdownFields ;
 }
 public void BreakdownFields (  List<ReportField> breakdownFields ) {
 if ( Objects.Equals(this.breakdownFields,breakdownFields) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.breakdownFields).SetAll(breakdownFields) ;
 }
 public List<ReportField> YAxes (  ) {
 return this.yAxes ;
 }
 public void YAxes (  List<ReportField> yAxes ) {
 if ( Objects.Equals(this.yAxes,yAxes) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.yAxes).SetAll(yAxes) ;
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
 public ReportWaterfallChartConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportWaterfallChartConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.Category().forEach((  one ) => one.recordOld(ctx)) ;
 this.BreakdownFields().forEach((  one ) => one.recordOld(ctx)) ;
 this.YAxes().forEach((  one ) => one.recordOld(ctx)) ;
 this.Tooltips().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportWaterfallChartConfig && base.Equals(a) ;
 }
 public ReportWaterfallChartConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(category) ;
 ctx.CollectChilds(breakdownFields) ;
 ctx.CollectChilds(yAxes) ;
 ctx.CollectChilds(tooltips) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportWaterfallChartConfig _obj=((ReportWaterfallChartConfig)dbObj);
 ctx.cloneChildList(category,(  v ) => _obj.Category(v)) ;
 ctx.cloneChildList(breakdownFields,(  v ) => _obj.BreakdownFields(v)) ;
 ctx.cloneChildList(yAxes,(  v ) => _obj.YAxes(v)) ;
 ctx.cloneChildList(tooltips,(  v ) => _obj.Tooltips(v)) ;
 }
 public ReportWaterfallChartConfig CloneInstance (  ReportWaterfallChartConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportWaterfallChartConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Category(Category().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.BreakdownFields(BreakdownFields().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.YAxes(YAxes().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Tooltips(Tooltips().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportWaterfallChartConfig CreateNewInstance (  ) {
 return new ReportWaterfallChartConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.category) ;
 Database.CollectCollctionCreatableReferences(_refs,this.breakdownFields) ;
 Database.CollectCollctionCreatableReferences(_refs,this.yAxes) ;
 Database.CollectCollctionCreatableReferences(_refs,this.tooltips) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case CATEGORY: {
 this.ChildCollFieldChanged(childIdx,set,category) ;
 break; }
 case BREAKDOWNFIELDS: {
 this.ChildCollFieldChanged(childIdx,set,breakdownFields) ;
 break; }
 case YAXES: {
 this.ChildCollFieldChanged(childIdx,set,yAxes) ;
 break; }
 case TOOLTIPS: {
 this.ChildCollFieldChanged(childIdx,set,tooltips) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }