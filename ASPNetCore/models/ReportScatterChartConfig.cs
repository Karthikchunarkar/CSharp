namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportScatterChartConfig :  ReportBaseConfig { public static int VALUES = 0 ;
 
 public static int XAXES = 1 ;
 
 public static int YAXES = 2 ;
 
 public static int SIZE = 3 ;
 
 public static int LEGENDS = 4 ;
 
 public static int PLAYAXIS = 5 ;
 
 public static int TOOLTIPS = 6 ;
 
 private List<ReportField> values { get; set; } = D3EPersistanceList.child(VALUES) ;
 
 private List<ReportField> xAxes { get; set; } = D3EPersistanceList.child(XAXES) ;
 
 private List<ReportField> yAxes { get; set; } = D3EPersistanceList.child(YAXES) ;
 
 private List<ReportField> size { get; set; } = D3EPersistanceList.child(SIZE) ;
 
 private List<ReportField> legends { get; set; } = D3EPersistanceList.child(LEGENDS) ;
 
 private List<ReportField> playAxis { get; set; } = D3EPersistanceList.child(PLAYAXIS) ;
 
 private List<ReportField> tooltips { get; set; } = D3EPersistanceList.child(TOOLTIPS) ;
 
 private ReportScatterChartConfig Old { get; set; } 
 public ReportScatterChartConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportScatterChartConfig ;
 }
 public string Type (  ) {
 return "ReportScatterChartConfig" ;
 }
 public int FieldsCount (  ) {
 return 7 ;
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
 public void AddToSize (  ReportField val, long index ) {
 if ( index == -1 ) {
 size.Add(val) ;
 }
 else {
 size.Add(((int)index),val) ;
 }
 }
 public void RemoveFromSize (  ReportField val ) {
 val._clearChildIdx() ;
 size.Remove(val) ;
 }
 public void AddToLegends (  ReportField val, long index ) {
 if ( index == -1 ) {
 legends.Add(val) ;
 }
 else {
 legends.Add(((int)index),val) ;
 }
 }
 public void RemoveFromLegends (  ReportField val ) {
 val._clearChildIdx() ;
 legends.Remove(val) ;
 }
 public void AddToPlayAxis (  ReportField val, long index ) {
 if ( index == -1 ) {
 playAxis.Add(val) ;
 }
 else {
 playAxis.Add(((int)index),val) ;
 }
 }
 public void RemoveFromPlayAxis (  ReportField val ) {
 val._clearChildIdx() ;
 playAxis.Remove(val) ;
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
 foreach ( ReportField obj in this.Values() ) {
 visitor.accept(obj) ;
 obj.MasterReportScatterChartConfig(this) ;
 obj.SetChildIdx(VALUES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.XAxes() ) {
 visitor.accept(obj) ;
 obj.MasterReportScatterChartConfig(this) ;
 obj.SetChildIdx(XAXES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.YAxes() ) {
 visitor.accept(obj) ;
 obj.MasterReportScatterChartConfig(this) ;
 obj.SetChildIdx(YAXES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Size() ) {
 visitor.accept(obj) ;
 obj.MasterReportScatterChartConfig(this) ;
 obj.SetChildIdx(SIZE) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Legends() ) {
 visitor.accept(obj) ;
 obj.MasterReportScatterChartConfig(this) ;
 obj.SetChildIdx(LEGENDS) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.PlayAxis() ) {
 visitor.accept(obj) ;
 obj.MasterReportScatterChartConfig(this) ;
 obj.SetChildIdx(PLAYAXIS) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.MasterReportScatterChartConfig(this) ;
 obj.SetChildIdx(TOOLTIPS) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportField obj in this.Values() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.XAxes() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.YAxes() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Size() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Legends() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.PlayAxis() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
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
 public List<ReportField> Size (  ) {
 return this.size ;
 }
 public void Size (  List<ReportField> size ) {
 if ( Objects.Equals(this.size,size) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.size).SetAll(size) ;
 }
 public List<ReportField> Legends (  ) {
 return this.legends ;
 }
 public void Legends (  List<ReportField> legends ) {
 if ( Objects.Equals(this.legends,legends) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.legends).SetAll(legends) ;
 }
 public List<ReportField> PlayAxis (  ) {
 return this.playAxis ;
 }
 public void PlayAxis (  List<ReportField> playAxis ) {
 if ( Objects.Equals(this.playAxis,playAxis) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.playAxis).SetAll(playAxis) ;
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
 public ReportScatterChartConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportScatterChartConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.Values().forEach((  one ) => one.recordOld(ctx)) ;
 this.XAxes().forEach((  one ) => one.recordOld(ctx)) ;
 this.YAxes().forEach((  one ) => one.recordOld(ctx)) ;
 this.Size().forEach((  one ) => one.recordOld(ctx)) ;
 this.Legends().forEach((  one ) => one.recordOld(ctx)) ;
 this.PlayAxis().forEach((  one ) => one.recordOld(ctx)) ;
 this.Tooltips().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportScatterChartConfig && base.Equals(a) ;
 }
 public ReportScatterChartConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(values) ;
 ctx.CollectChilds(xAxes) ;
 ctx.CollectChilds(yAxes) ;
 ctx.CollectChilds(size) ;
 ctx.CollectChilds(legends) ;
 ctx.CollectChilds(playAxis) ;
 ctx.CollectChilds(tooltips) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportScatterChartConfig _obj=((ReportScatterChartConfig)dbObj);
 ctx.cloneChildList(values,(  v ) => _obj.Values(v)) ;
 ctx.cloneChildList(xAxes,(  v ) => _obj.XAxes(v)) ;
 ctx.cloneChildList(yAxes,(  v ) => _obj.YAxes(v)) ;
 ctx.cloneChildList(size,(  v ) => _obj.Size(v)) ;
 ctx.cloneChildList(legends,(  v ) => _obj.Legends(v)) ;
 ctx.cloneChildList(playAxis,(  v ) => _obj.PlayAxis(v)) ;
 ctx.cloneChildList(tooltips,(  v ) => _obj.Tooltips(v)) ;
 }
 public ReportScatterChartConfig CloneInstance (  ReportScatterChartConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportScatterChartConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Values(Values().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.XAxes(XAxes().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.YAxes(YAxes().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Size(Size().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Legends(Legends().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.PlayAxis(PlayAxis().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Tooltips(Tooltips().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportScatterChartConfig CreateNewInstance (  ) {
 return new ReportScatterChartConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.values) ;
 Database.CollectCollctionCreatableReferences(_refs,this.xAxes) ;
 Database.CollectCollctionCreatableReferences(_refs,this.yAxes) ;
 Database.CollectCollctionCreatableReferences(_refs,this.size) ;
 Database.CollectCollctionCreatableReferences(_refs,this.legends) ;
 Database.CollectCollctionCreatableReferences(_refs,this.playAxis) ;
 Database.CollectCollctionCreatableReferences(_refs,this.tooltips) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case VALUES: {
 this.ChildCollFieldChanged(childIdx,set,values) ;
 break; }
 case XAXES: {
 this.ChildCollFieldChanged(childIdx,set,xAxes) ;
 break; }
 case YAXES: {
 this.ChildCollFieldChanged(childIdx,set,yAxes) ;
 break; }
 case SIZE: {
 this.ChildCollFieldChanged(childIdx,set,size) ;
 break; }
 case LEGENDS: {
 this.ChildCollFieldChanged(childIdx,set,legends) ;
 break; }
 case PLAYAXIS: {
 this.ChildCollFieldChanged(childIdx,set,playAxis) ;
 break; }
 case TOOLTIPS: {
 this.ChildCollFieldChanged(childIdx,set,tooltips) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }