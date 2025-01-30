namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class ReportNamedCondition :  DatabaseObject { public static int NAME = 0 ;
 
 public static int CONDITION = 1 ;
 
 private string name { get; set; } 
 private ReportRule condition { get; set; } 
 private ReportNamedCondition Old { get; set; } 
 public ReportNamedCondition (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportNamedCondition ;
 }
 public string Type (  ) {
 return "ReportNamedCondition" ;
 }
 public int FieldsCount (  ) {
 return 2 ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 if ( condition != null ) {
 visitor.accept(condition) ;
 condition.MasterReportNamedCondition(this) ;
 condition.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 if ( condition != null ) {
 visitor.accept(condition) ;
 condition.VisitChildren(visitor) ;
 }
 }
 public string Name (  ) {
 _CheckProxy() ;
 return this.name ;
 }
 public void Name (  string name ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.name,name) ) {
 return ;
 }
 fieldChanged(NAME,this.name,name) ;
 this.name = name ;
 }
 public ReportRule Condition (  ) {
 _CheckProxy() ;
 return this.condition ;
 }
 public void Condition (  ReportRule condition ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.condition,condition) ) {
 if ( this.condition != null ) {
 this.condition._updateChanges() ;
 }
 return ;
 }
 fieldChanged(CONDITION,this.condition,condition) ;
 this.condition = condition ;
 if ( this.condition != null ) {
 this.condition.setMasterReportNamedCondition(this) ;
 this.condition._setChildIdx(CONDITION) ;
 this.condition._updateChanges() ;
 }
 }
 public ReportNamedCondition getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportNamedCondition)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 if ( this.Condition() != null ) {
 this.Condition().recordOld(ctx) ;
 }
 }
 public string DisplayName (  ) {
 return "ReportNamedCondition" ;
 }
 public bool equals (  Object a ) {
 return a is ReportNamedCondition && base.Equals(a) ;
 }
 public ReportNamedCondition DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChild(condition) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportNamedCondition _obj=((ReportNamedCondition)dbObj);
 _obj.Name(name) ;
 ctx.cloneChild(condition,(  v ) => _obj.Condition(v)) ;
 }
 public ReportNamedCondition CloneInstance (  ReportNamedCondition cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportNamedCondition() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Name(this.Name()) ;
 cloneObj.Condition(Condition() == null ? null : Condition().CloneInstance(null)) ;
 return cloneObj ;
 }
 public ReportNamedCondition CreateNewInstance (  ) {
 return new ReportNamedCondition() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.collectCreatableReferences(_refs,this.condition) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case CONDITION: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 } }
 }