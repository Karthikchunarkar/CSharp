namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportKeyInfluencerConfig :  ReportBaseConfig { public static int ANALYZE = 0 ;
 
 public static int EEXPLAINBY = 1 ;
 
 public static int EXPANDBY = 2 ;
 
 private ReportField analyze { get; set; } 
 private List<ReportField> eexplainBy { get; set; } = D3EPersistanceList.child(EEXPLAINBY) ;
 
 private List<ReportField> expandBy { get; set; } = D3EPersistanceList.child(EXPANDBY) ;
 
 private ReportKeyInfluencerConfig Old { get; set; } 
 public ReportKeyInfluencerConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportKeyInfluencerConfig ;
 }
 public string Type (  ) {
 return "ReportKeyInfluencerConfig" ;
 }
 public int FieldsCount (  ) {
 return 3 ;
 }
 public void AddToEexplainBy (  ReportField val, long index ) {
 if ( index == -1 ) {
 eexplainBy.Add(val) ;
 }
 else {
 eexplainBy.Add(((int)index),val) ;
 }
 }
 public void RemoveFromEexplainBy (  ReportField val ) {
 val._clearChildIdx() ;
 eexplainBy.Remove(val) ;
 }
 public void AddToExpandBy (  ReportField val, long index ) {
 if ( index == -1 ) {
 expandBy.Add(val) ;
 }
 else {
 expandBy.Add(((int)index),val) ;
 }
 }
 public void RemoveFromExpandBy (  ReportField val ) {
 val._clearChildIdx() ;
 expandBy.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 if ( analyze != null ) {
 visitor.accept(analyze) ;
 analyze.MasterReportKeyInfluencerConfig(this) ;
 analyze.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.EexplainBy() ) {
 visitor.accept(obj) ;
 obj.MasterReportKeyInfluencerConfig(this) ;
 obj.SetChildIdx(EEXPLAINBY) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.ExpandBy() ) {
 visitor.accept(obj) ;
 obj.MasterReportKeyInfluencerConfig(this) ;
 obj.SetChildIdx(EXPANDBY) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 if ( analyze != null ) {
 visitor.accept(analyze) ;
 analyze.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.EexplainBy() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.ExpandBy() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public ReportField Analyze (  ) {
 _CheckProxy() ;
 return this.analyze ;
 }
 public void Analyze (  ReportField analyze ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.analyze,analyze) ) {
 if ( this.analyze != null ) {
 this.analyze._updateChanges() ;
 }
 return ;
 }
 fieldChanged(ANALYZE,this.analyze,analyze) ;
 this.analyze = analyze ;
 if ( this.analyze != null ) {
 this.analyze.setMasterReportKeyInfluencerConfig(this) ;
 this.analyze._setChildIdx(ANALYZE) ;
 this.analyze._updateChanges() ;
 }
 }
 public List<ReportField> EexplainBy (  ) {
 return this.eexplainBy ;
 }
 public void EexplainBy (  List<ReportField> eexplainBy ) {
 if ( Objects.Equals(this.eexplainBy,eexplainBy) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.eexplainBy).SetAll(eexplainBy) ;
 }
 public List<ReportField> ExpandBy (  ) {
 return this.expandBy ;
 }
 public void ExpandBy (  List<ReportField> expandBy ) {
 if ( Objects.Equals(this.expandBy,expandBy) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.expandBy).SetAll(expandBy) ;
 }
 public ReportKeyInfluencerConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportKeyInfluencerConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 if ( this.Analyze() != null ) {
 this.Analyze().recordOld(ctx) ;
 }
 this.EexplainBy().forEach((  one ) => one.recordOld(ctx)) ;
 this.ExpandBy().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportKeyInfluencerConfig && base.Equals(a) ;
 }
 public ReportKeyInfluencerConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChild(analyze) ;
 ctx.CollectChilds(eexplainBy) ;
 ctx.CollectChilds(expandBy) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportKeyInfluencerConfig _obj=((ReportKeyInfluencerConfig)dbObj);
 ctx.cloneChild(analyze,(  v ) => _obj.Analyze(v)) ;
 ctx.cloneChildList(eexplainBy,(  v ) => _obj.EexplainBy(v)) ;
 ctx.cloneChildList(expandBy,(  v ) => _obj.ExpandBy(v)) ;
 }
 public ReportKeyInfluencerConfig CloneInstance (  ReportKeyInfluencerConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportKeyInfluencerConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Analyze(Analyze() == null ? null : Analyze().CloneInstance(null)) ;
 cloneObj.EexplainBy(EexplainBy().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.ExpandBy(ExpandBy().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportKeyInfluencerConfig CreateNewInstance (  ) {
 return new ReportKeyInfluencerConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.collectCreatableReferences(_refs,this.analyze) ;
 Database.CollectCollctionCreatableReferences(_refs,this.eexplainBy) ;
 Database.CollectCollctionCreatableReferences(_refs,this.expandBy) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case ANALYZE: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 case EEXPLAINBY: {
 this.ChildCollFieldChanged(childIdx,set,eexplainBy) ;
 break; }
 case EXPANDBY: {
 this.ChildCollFieldChanged(childIdx,set,expandBy) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }