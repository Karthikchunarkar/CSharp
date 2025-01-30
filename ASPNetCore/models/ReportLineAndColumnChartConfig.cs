namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportLineAndColumnChartConfig :  ReportBaseConfig { public static int TYPE = 0 ;
 
 public static int XAXES = 1 ;
 
 public static int COLUMNYAXES = 2 ;
 
 public static int LINEYAXES = 3 ;
 
 public static int COLUMNLEGEND = 4 ;
 
 public static int SMALLMULTIPLES = 5 ;
 
 public static int TOOLTIPS = 6 ;
 
 private ReportLineAndColumnChartType type { get; set; } = ReportLineAndColumnChartType.Stacked ;
 
 private List<ReportField> xAxes { get; set; } = D3EPersistanceList.child(XAXES) ;
 
 private List<ReportField> columnYAxes { get; set; } = D3EPersistanceList.child(COLUMNYAXES) ;
 
 private List<ReportField> lineYAxes { get; set; } = D3EPersistanceList.child(LINEYAXES) ;
 
 private List<ReportField> columnLegend { get; set; } = D3EPersistanceList.child(COLUMNLEGEND) ;
 
 private List<ReportField> smallMultiples { get; set; } = D3EPersistanceList.child(SMALLMULTIPLES) ;
 
 private List<ReportField> tooltips { get; set; } = D3EPersistanceList.child(TOOLTIPS) ;
 
 private ReportLineAndColumnChartConfig Old { get; set; } 
 public ReportLineAndColumnChartConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportLineAndColumnChartConfig ;
 }
 public string Type (  ) {
 return "ReportLineAndColumnChartConfig" ;
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
 public void AddToColumnYAxes (  ReportField val, long index ) {
 if ( index == -1 ) {
 columnYAxes.Add(val) ;
 }
 else {
 columnYAxes.Add(((int)index),val) ;
 }
 }
 public void RemoveFromColumnYAxes (  ReportField val ) {
 val._clearChildIdx() ;
 columnYAxes.Remove(val) ;
 }
 public void AddToLineYAxes (  ReportField val, long index ) {
 if ( index == -1 ) {
 lineYAxes.Add(val) ;
 }
 else {
 lineYAxes.Add(((int)index),val) ;
 }
 }
 public void RemoveFromLineYAxes (  ReportField val ) {
 val._clearChildIdx() ;
 lineYAxes.Remove(val) ;
 }
 public void AddToColumnLegend (  ReportField val, long index ) {
 if ( index == -1 ) {
 columnLegend.Add(val) ;
 }
 else {
 columnLegend.Add(((int)index),val) ;
 }
 }
 public void RemoveFromColumnLegend (  ReportField val ) {
 val._clearChildIdx() ;
 columnLegend.Remove(val) ;
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
 obj.MasterReportLineAndColumnChartConfig(this) ;
 obj.SetChildIdx(XAXES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.ColumnYAxes() ) {
 visitor.accept(obj) ;
 obj.MasterReportLineAndColumnChartConfig(this) ;
 obj.SetChildIdx(COLUMNYAXES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.LineYAxes() ) {
 visitor.accept(obj) ;
 obj.MasterReportLineAndColumnChartConfig(this) ;
 obj.SetChildIdx(LINEYAXES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.ColumnLegend() ) {
 visitor.accept(obj) ;
 obj.MasterReportLineAndColumnChartConfig(this) ;
 obj.SetChildIdx(COLUMNLEGEND) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.SmallMultiples() ) {
 visitor.accept(obj) ;
 obj.MasterReportLineAndColumnChartConfig(this) ;
 obj.SetChildIdx(SMALLMULTIPLES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.MasterReportLineAndColumnChartConfig(this) ;
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
 foreach ( ReportField obj in this.ColumnYAxes() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.LineYAxes() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.ColumnLegend() ) {
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
 public ReportLineAndColumnChartType Type (  ) {
 _CheckProxy() ;
 return this.type ;
 }
 public void Type (  ReportLineAndColumnChartType type ) {
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
 public List<ReportField> ColumnYAxes (  ) {
 return this.columnYAxes ;
 }
 public void ColumnYAxes (  List<ReportField> columnYAxes ) {
 if ( Objects.Equals(this.columnYAxes,columnYAxes) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.columnYAxes).SetAll(columnYAxes) ;
 }
 public List<ReportField> LineYAxes (  ) {
 return this.lineYAxes ;
 }
 public void LineYAxes (  List<ReportField> lineYAxes ) {
 if ( Objects.Equals(this.lineYAxes,lineYAxes) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.lineYAxes).SetAll(lineYAxes) ;
 }
 public List<ReportField> ColumnLegend (  ) {
 return this.columnLegend ;
 }
 public void ColumnLegend (  List<ReportField> columnLegend ) {
 if ( Objects.Equals(this.columnLegend,columnLegend) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.columnLegend).SetAll(columnLegend) ;
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
 public ReportLineAndColumnChartConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportLineAndColumnChartConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.XAxes().forEach((  one ) => one.recordOld(ctx)) ;
 this.ColumnYAxes().forEach((  one ) => one.recordOld(ctx)) ;
 this.LineYAxes().forEach((  one ) => one.recordOld(ctx)) ;
 this.ColumnLegend().forEach((  one ) => one.recordOld(ctx)) ;
 this.SmallMultiples().forEach((  one ) => one.recordOld(ctx)) ;
 this.Tooltips().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportLineAndColumnChartConfig && base.Equals(a) ;
 }
 public ReportLineAndColumnChartConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(xAxes) ;
 ctx.CollectChilds(columnYAxes) ;
 ctx.CollectChilds(lineYAxes) ;
 ctx.CollectChilds(columnLegend) ;
 ctx.CollectChilds(smallMultiples) ;
 ctx.CollectChilds(tooltips) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportLineAndColumnChartConfig _obj=((ReportLineAndColumnChartConfig)dbObj);
 _obj.Type(type) ;
 ctx.cloneChildList(xAxes,(  v ) => _obj.XAxes(v)) ;
 ctx.cloneChildList(columnYAxes,(  v ) => _obj.ColumnYAxes(v)) ;
 ctx.cloneChildList(lineYAxes,(  v ) => _obj.LineYAxes(v)) ;
 ctx.cloneChildList(columnLegend,(  v ) => _obj.ColumnLegend(v)) ;
 ctx.cloneChildList(smallMultiples,(  v ) => _obj.SmallMultiples(v)) ;
 ctx.cloneChildList(tooltips,(  v ) => _obj.Tooltips(v)) ;
 }
 public ReportLineAndColumnChartConfig CloneInstance (  ReportLineAndColumnChartConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportLineAndColumnChartConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Type(this.Type()) ;
 cloneObj.XAxes(XAxes().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.ColumnYAxes(ColumnYAxes().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.LineYAxes(LineYAxes().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.ColumnLegend(ColumnLegend().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.SmallMultiples(SmallMultiples().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Tooltips(Tooltips().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportLineAndColumnChartConfig CreateNewInstance (  ) {
 return new ReportLineAndColumnChartConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.xAxes) ;
 Database.CollectCollctionCreatableReferences(_refs,this.columnYAxes) ;
 Database.CollectCollctionCreatableReferences(_refs,this.lineYAxes) ;
 Database.CollectCollctionCreatableReferences(_refs,this.columnLegend) ;
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
 case COLUMNYAXES: {
 this.ChildCollFieldChanged(childIdx,set,columnYAxes) ;
 break; }
 case LINEYAXES: {
 this.ChildCollFieldChanged(childIdx,set,lineYAxes) ;
 break; }
 case COLUMNLEGEND: {
 this.ChildCollFieldChanged(childIdx,set,columnLegend) ;
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