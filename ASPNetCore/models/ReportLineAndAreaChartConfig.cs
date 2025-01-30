namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportLineAndAreaChartConfig :  ReportBaseConfig { public static int TYPE = 0 ;
 
 public static int XAXES = 1 ;
 
 public static int YAXES = 2 ;
 
 public static int SECONDARYYAXES = 3 ;
 
 public static int LEGEND = 4 ;
 
 public static int SMALLMULTIPLES = 5 ;
 
 public static int TOOLTIPS = 6 ;
 
 private ReportLineAndAreaChartType type { get; set; } = ReportLineAndAreaChartType.Line ;
 
 private List<ReportField> xAxes { get; set; } = D3EPersistanceList.child(XAXES) ;
 
 private List<ReportField> yAxes { get; set; } = D3EPersistanceList.child(YAXES) ;
 
 private List<ReportField> secondaryYAxes { get; set; } = D3EPersistanceList.child(SECONDARYYAXES) ;
 
 private List<ReportField> legend { get; set; } = D3EPersistanceList.child(LEGEND) ;
 
 private List<ReportField> smallMultiples { get; set; } = D3EPersistanceList.child(SMALLMULTIPLES) ;
 
 private List<ReportField> tooltips { get; set; } = D3EPersistanceList.child(TOOLTIPS) ;
 
 private ReportLineAndAreaChartConfig Old { get; set; } 
 public ReportLineAndAreaChartConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportLineAndAreaChartConfig ;
 }
 public string Type (  ) {
 return "ReportLineAndAreaChartConfig" ;
 }
 public int FieldsCount (  ) {
 return 7 ;
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
 public void AddToSecondaryYAxes (  ReportField val, long index ) {
 if ( index == -1 ) {
 secondaryYAxes.Add(val) ;
 }
 else {
 secondaryYAxes.Add(((int)index),val) ;
 }
 }
 public void RemoveFromSecondaryYAxes (  ReportField val ) {
 val._clearChildIdx() ;
 secondaryYAxes.Remove(val) ;
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
 obj.MasterReportLineAndAreaChartConfig(this) ;
 obj.SetChildIdx(XAXES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.YAxes() ) {
 visitor.accept(obj) ;
 obj.MasterReportLineAndAreaChartConfig(this) ;
 obj.SetChildIdx(YAXES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.SecondaryYAxes() ) {
 visitor.accept(obj) ;
 obj.MasterReportLineAndAreaChartConfig(this) ;
 obj.SetChildIdx(SECONDARYYAXES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Legend() ) {
 visitor.accept(obj) ;
 obj.MasterReportLineAndAreaChartConfig(this) ;
 obj.SetChildIdx(LEGEND) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.SmallMultiples() ) {
 visitor.accept(obj) ;
 obj.MasterReportLineAndAreaChartConfig(this) ;
 obj.SetChildIdx(SMALLMULTIPLES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.MasterReportLineAndAreaChartConfig(this) ;
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
 foreach ( ReportField obj in this.SecondaryYAxes() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Legend() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
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
 public ReportLineAndAreaChartType Type (  ) {
 _CheckProxy() ;
 return this.type ;
 }
 public void Type (  ReportLineAndAreaChartType type ) {
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
 public List<ReportField> SecondaryYAxes (  ) {
 return this.secondaryYAxes ;
 }
 public void SecondaryYAxes (  List<ReportField> secondaryYAxes ) {
 if ( Objects.Equals(this.secondaryYAxes,secondaryYAxes) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.secondaryYAxes).SetAll(secondaryYAxes) ;
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
 public ReportLineAndAreaChartConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportLineAndAreaChartConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.XAxes().forEach((  one ) => one.recordOld(ctx)) ;
 this.YAxes().forEach((  one ) => one.recordOld(ctx)) ;
 this.SecondaryYAxes().forEach((  one ) => one.recordOld(ctx)) ;
 this.Legend().forEach((  one ) => one.recordOld(ctx)) ;
 this.SmallMultiples().forEach((  one ) => one.recordOld(ctx)) ;
 this.Tooltips().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportLineAndAreaChartConfig && base.Equals(a) ;
 }
 public ReportLineAndAreaChartConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(xAxes) ;
 ctx.CollectChilds(yAxes) ;
 ctx.CollectChilds(secondaryYAxes) ;
 ctx.CollectChilds(legend) ;
 ctx.CollectChilds(smallMultiples) ;
 ctx.CollectChilds(tooltips) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportLineAndAreaChartConfig _obj=((ReportLineAndAreaChartConfig)dbObj);
 _obj.Type(type) ;
 ctx.cloneChildList(xAxes,(  v ) => _obj.XAxes(v)) ;
 ctx.cloneChildList(yAxes,(  v ) => _obj.YAxes(v)) ;
 ctx.cloneChildList(secondaryYAxes,(  v ) => _obj.SecondaryYAxes(v)) ;
 ctx.cloneChildList(legend,(  v ) => _obj.Legend(v)) ;
 ctx.cloneChildList(smallMultiples,(  v ) => _obj.SmallMultiples(v)) ;
 ctx.cloneChildList(tooltips,(  v ) => _obj.Tooltips(v)) ;
 }
 public ReportLineAndAreaChartConfig CloneInstance (  ReportLineAndAreaChartConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportLineAndAreaChartConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Type(this.Type()) ;
 cloneObj.XAxes(XAxes().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.YAxes(YAxes().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.SecondaryYAxes(SecondaryYAxes().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Legend(Legend().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.SmallMultiples(SmallMultiples().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Tooltips(Tooltips().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportLineAndAreaChartConfig CreateNewInstance (  ) {
 return new ReportLineAndAreaChartConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.xAxes) ;
 Database.CollectCollctionCreatableReferences(_refs,this.yAxes) ;
 Database.CollectCollctionCreatableReferences(_refs,this.secondaryYAxes) ;
 Database.CollectCollctionCreatableReferences(_refs,this.legend) ;
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
 case SECONDARYYAXES: {
 this.ChildCollFieldChanged(childIdx,set,secondaryYAxes) ;
 break; }
 case LEGEND: {
 this.ChildCollFieldChanged(childIdx,set,legend) ;
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