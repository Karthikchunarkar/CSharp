namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportFunnelChartConfig :  ReportBaseConfig { public static int CATEGORYFIELDS = 0 ;
 
 public static int VALUES = 1 ;
 
 public static int TOOLTIPS = 2 ;
 
 private List<ReportField> categoryFields { get; set; } = D3EPersistanceList.child(CATEGORYFIELDS) ;
 
 private List<ReportField> values { get; set; } = D3EPersistanceList.child(VALUES) ;
 
 private List<ReportField> tooltips { get; set; } = D3EPersistanceList.child(TOOLTIPS) ;
 
 private ReportFunnelChartConfig Old { get; set; } 
 public ReportFunnelChartConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportFunnelChartConfig ;
 }
 public string Type (  ) {
 return "ReportFunnelChartConfig" ;
 }
 public int FieldsCount (  ) {
 return 3 ;
 }
 public void AddToCategoryFields (  ReportField val, long index ) {
 if ( index == -1 ) {
 categoryFields.Add(val) ;
 }
 else {
 categoryFields.Add(((int)index),val) ;
 }
 }
 public void RemoveFromCategoryFields (  ReportField val ) {
 val._clearChildIdx() ;
 categoryFields.Remove(val) ;
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
 foreach ( ReportField obj in this.CategoryFields() ) {
 visitor.accept(obj) ;
 obj.MasterReportFunnelChartConfig(this) ;
 obj.SetChildIdx(CATEGORYFIELDS) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Values() ) {
 visitor.accept(obj) ;
 obj.MasterReportFunnelChartConfig(this) ;
 obj.SetChildIdx(VALUES) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.MasterReportFunnelChartConfig(this) ;
 obj.SetChildIdx(TOOLTIPS) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportField obj in this.CategoryFields() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Values() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public List<ReportField> CategoryFields (  ) {
 return this.categoryFields ;
 }
 public void CategoryFields (  List<ReportField> categoryFields ) {
 if ( Objects.Equals(this.categoryFields,categoryFields) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.categoryFields).SetAll(categoryFields) ;
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
 public List<ReportField> Tooltips (  ) {
 return this.tooltips ;
 }
 public void Tooltips (  List<ReportField> tooltips ) {
 if ( Objects.Equals(this.tooltips,tooltips) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.tooltips).SetAll(tooltips) ;
 }
 public ReportFunnelChartConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportFunnelChartConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.CategoryFields().forEach((  one ) => one.recordOld(ctx)) ;
 this.Values().forEach((  one ) => one.recordOld(ctx)) ;
 this.Tooltips().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportFunnelChartConfig && base.Equals(a) ;
 }
 public ReportFunnelChartConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(categoryFields) ;
 ctx.CollectChilds(values) ;
 ctx.CollectChilds(tooltips) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportFunnelChartConfig _obj=((ReportFunnelChartConfig)dbObj);
 ctx.cloneChildList(categoryFields,(  v ) => _obj.CategoryFields(v)) ;
 ctx.cloneChildList(values,(  v ) => _obj.Values(v)) ;
 ctx.cloneChildList(tooltips,(  v ) => _obj.Tooltips(v)) ;
 }
 public ReportFunnelChartConfig CloneInstance (  ReportFunnelChartConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportFunnelChartConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.CategoryFields(CategoryFields().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Values(Values().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Tooltips(Tooltips().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportFunnelChartConfig CreateNewInstance (  ) {
 return new ReportFunnelChartConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.categoryFields) ;
 Database.CollectCollctionCreatableReferences(_refs,this.values) ;
 Database.CollectCollctionCreatableReferences(_refs,this.tooltips) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case CATEGORYFIELDS: {
 this.ChildCollFieldChanged(childIdx,set,categoryFields) ;
 break; }
 case VALUES: {
 this.ChildCollFieldChanged(childIdx,set,values) ;
 break; }
 case TOOLTIPS: {
 this.ChildCollFieldChanged(childIdx,set,tooltips) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }