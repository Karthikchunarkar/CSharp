namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class ReportCardConfig :  ReportBaseConfig { public static int VALUE = 0 ;
 
 private ReportField value { get; set; } 
 private ReportCardConfig Old { get; set; } 
 public ReportCardConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportCardConfig ;
 }
 public string Type (  ) {
 return "ReportCardConfig" ;
 }
 public int FieldsCount (  ) {
 return 1 ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 if ( value != null ) {
 visitor.accept(value) ;
 value.MasterReportCardConfig(this) ;
 value.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 if ( value != null ) {
 visitor.accept(value) ;
 value.VisitChildren(visitor) ;
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
 this.value.setMasterReportCardConfig(this) ;
 this.value._setChildIdx(VALUE) ;
 this.value._updateChanges() ;
 }
 }
 public ReportCardConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportCardConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 if ( this.Value() != null ) {
 this.Value().recordOld(ctx) ;
 }
 }
 public bool equals (  Object a ) {
 return a is ReportCardConfig && base.Equals(a) ;
 }
 public ReportCardConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChild(value) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportCardConfig _obj=((ReportCardConfig)dbObj);
 ctx.cloneChild(value,(  v ) => _obj.Value(v)) ;
 }
 public ReportCardConfig CloneInstance (  ReportCardConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportCardConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Value(Value() == null ? null : Value().CloneInstance(null)) ;
 return cloneObj ;
 }
 public ReportCardConfig CreateNewInstance (  ) {
 return new ReportCardConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.collectCreatableReferences(_refs,this.value) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case VALUE: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }