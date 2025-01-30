namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportBarChartConfig :  ReportBaseConfig { public static int TYPE = 0 ;
 
 public static int XAXES = 1 ;
 
 public static int YAXES = 2 ;
 
 public static int LEGEND = 3 ;
 
 public static int SMALLMULTIPLES = 4 ;
 
 public static int TOOLTIPS = 5 ;
 
 private ReportBarChartType type { get; set; } = ReportBarChartType.Stacked ;
 
 private List<ReportField> xAxes { get; set; } = D3EPersistanceList.child(XAXES) ;
 
 private List<ReportField> yAxes { get; set; } = D3EPersistanceList.child(YAXES) ;
 
 private ReportField legend { get; set; } 
 private List<ReportField> smallMultiples { get; set; } = D3EPersistanceList.child(SMALLMULTIPLES) ;
 
 private List<ReportField> tooltips { get; set; } = D3EPersistanceList.child(TOOLTIPS) ;
 
 private ReportBarChartConfig Old { get; set; } 
 public ReportBarChartConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportBarChartConfig ;
 }
 public string Type (  ) {
 return "ReportBarChartConfig" ;
 }
 public int FieldsCount (  ) {
 return 6 ;
 }
 public void AddToXAxes (  ReportField val, long index ) {
 if ( index == -1 ) {
 xAxes.Add(val) ;
 }
 else {
 xAxes.Add(((int)index),val) ;
 }
 }
 public void RemoveFromXAxes (  ReportField val ) {
 val._clearChildIdx() ;
 xAxes.Remove(val) ;
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
 public void AddToSmallMultiples (  ReportField val, long index ) {
 if ( index == -1 ) {
 smallMultiples.Add(val) ;
 }
 else {
 smallMultiples.Add(((int)index),val) ;
 }
 }
 public void RemoveFromSmallMultiples (  ReportField val ) {
 val._clearChildIdx() ;
 smallMultiples.Remove(val) ;
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
 foreach ( ReportField obj in this.XAxes() ) {
 visitor.accept(obj) ;
 obj.MasterReportBarChartConfig(this) ;
 obj.SetChildIdx(XAXES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.YAxes() ) {
 visitor.accept(obj) ;
 obj.MasterReportBarChartConfig(this) ;
 obj.SetChildIdx(YAXES) ;
 obj.UpdateMasters(visitor) ;
 }
 if ( legend != null ) {
 visitor.accept(legend) ;
 legend.MasterReportBarChartConfig(this) ;
 legend.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.SmallMultiples() ) {
 visitor.accept(obj) ;
 obj.MasterReportBarChartConfig(this) ;
 obj.SetChildIdx(SMALLMULTIPLES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.MasterReportBarChartConfig(this) ;
 obj.SetChildIdx(TOOLTIPS) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportField obj in this.XAxes() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.YAxes() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 if ( legend != null ) {
 visitor.accept(legend) ;
 legend.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.SmallMultiples() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public ReportBarChartType Type (  ) {
 _CheckProxy() ;
 return this.type ;
 }
 public void Type (  ReportBarChartType type ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.type,type) ) {
 return ;
 }
 fieldChanged(TYPE,this.type,type) ;
 this.type = type ;
 }
 public List<ReportField> XAxes (  ) {
 return this.xAxes ;
 }
 public void XAxes (  List<ReportField> xAxes ) {
 if ( Objects.Equals(this.xAxes,xAxes) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.xAxes).SetAll(xAxes) ;
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
 public ReportField Legend (  ) {
 _CheckProxy() ;
 return this.legend ;
 }
 public void Legend (  ReportField legend ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.legend,legend) ) {
 if ( this.legend != null ) {
 this.legend._updateChanges() ;
 }
 return ;
 }
 fieldChanged(LEGEND,this.legend,legend) ;
 this.legend = legend ;
 if ( this.legend != null ) {
 this.legend.setMasterReportBarChartConfig(this) ;
 this.legend._setChildIdx(LEGEND) ;
 this.legend._updateChanges() ;
 }
 }
 public List<ReportField> SmallMultiples (  ) {
 return this.smallMultiples ;
 }
 public void SmallMultiples (  List<ReportField> smallMultiples ) {
 if ( Objects.Equals(this.smallMultiples,smallMultiples) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.smallMultiples).SetAll(smallMultiples) ;
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
 public ReportBarChartConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportBarChartConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.XAxes().forEach((  one ) => one.recordOld(ctx)) ;
 this.YAxes().forEach((  one ) => one.recordOld(ctx)) ;
 if ( this.Legend() != null ) {
 this.Legend().recordOld(ctx) ;
 }
 this.SmallMultiples().forEach((  one ) => one.recordOld(ctx)) ;
 this.Tooltips().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportBarChartConfig && base.Equals(a) ;
 }
 public ReportBarChartConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(xAxes) ;
 ctx.CollectChilds(yAxes) ;
 ctx.CollectChild(legend) ;
 ctx.CollectChilds(smallMultiples) ;
 ctx.CollectChilds(tooltips) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportBarChartConfig _obj=((ReportBarChartConfig)dbObj);
 _obj.Type(type) ;
 ctx.cloneChildList(xAxes,(  v ) => _obj.XAxes(v)) ;
 ctx.cloneChildList(yAxes,(  v ) => _obj.YAxes(v)) ;
 ctx.cloneChild(legend,(  v ) => _obj.Legend(v)) ;
 ctx.cloneChildList(smallMultiples,(  v ) => _obj.SmallMultiples(v)) ;
 ctx.cloneChildList(tooltips,(  v ) => _obj.Tooltips(v)) ;
 }
 public ReportBarChartConfig CloneInstance (  ReportBarChartConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportBarChartConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Type(this.Type()) ;
 cloneObj.XAxes(XAxes().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.YAxes(YAxes().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Legend(Legend() == null ? null : Legend().CloneInstance(null)) ;
 cloneObj.SmallMultiples(SmallMultiples().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Tooltips(Tooltips().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportBarChartConfig CreateNewInstance (  ) {
 return new ReportBarChartConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.xAxes) ;
 Database.CollectCollctionCreatableReferences(_refs,this.yAxes) ;
 Database.collectCreatableReferences(_refs,this.legend) ;
 Database.CollectCollctionCreatableReferences(_refs,this.smallMultiples) ;
 Database.CollectCollctionCreatableReferences(_refs,this.tooltips) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case XAXES: {
 this.ChildCollFieldChanged(childIdx,set,xAxes) ;
 break; }
 case YAXES: {
 this.ChildCollFieldChanged(childIdx,set,yAxes) ;
 break; }
 case LEGEND: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 case SMALLMULTIPLES: {
 this.ChildCollFieldChanged(childIdx,set,smallMultiples) ;
 break; }
 case TOOLTIPS: {
 this.ChildCollFieldChanged(childIdx,set,tooltips) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }