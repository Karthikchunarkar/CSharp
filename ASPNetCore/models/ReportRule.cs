namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public abstract class ReportRule :  DatabaseObject { public static int PARENT = 0 ;
 
 private ReportRule parent { get; set; } 
 private ReportNamedCondition masterReportNamedCondition { get; set; } 
 private ReportRuleSet masterReportRuleSet { get; set; } 
 public ReportRule (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportRule ;
 }
 public string Type (  ) {
 return "ReportRule" ;
 }
 public int FieldsCount (  ) {
 return 1 ;
 }
 public DatabaseObject MasterObject (  ) {
  DatabaseObject master=base.MasterObject();
 if ( master != null ) {
 return master ;
 }
 if ( masterReportNamedCondition != null ) {
 return masterReportNamedCondition ;
 }
 if ( masterReportRuleSet != null ) {
 return masterReportRuleSet ;
 }
 return null ;
 }
 public void SetMasterObject (  DBObject master ) {
 base.SetMasterObject(master) ;
 if ( master is ReportNamedCondition ) {
 masterReportNamedCondition = ((ReportNamedCondition)master) ;
 }
 if ( master is ReportRuleSet ) {
 masterReportRuleSet = ((ReportRuleSet)master) ;
 }
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public void updateFlat (  DatabaseObject obj ) {
 super.updateFlat(obj) ;
 if ( masterReportNamedCondition != null ) {
 masterReportNamedCondition.UpdateFlat(obj) ;
 }
 if ( masterReportRuleSet != null ) {
 masterReportRuleSet.UpdateFlat(obj) ;
 }
 }
 public ReportRule Parent (  ) {
 _CheckProxy() ;
 return this.parent ;
 }
 public void Parent (  ReportRule parent ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.parent,parent) ) {
 return ;
 }
 fieldChanged(PARENT,this.parent,parent) ;
 this.parent = parent ;
 }
 public ReportNamedCondition MasterReportNamedCondition (  ) {
 return this.masterReportNamedCondition ;
 }
 public void MasterReportNamedCondition (  ReportNamedCondition masterReportNamedCondition ) {
 this.masterReportNamedCondition = masterReportNamedCondition ;
 }
 public ReportRuleSet MasterReportRuleSet (  ) {
 return this.masterReportRuleSet ;
 }
 public void MasterReportRuleSet (  ReportRuleSet masterReportRuleSet ) {
 this.masterReportRuleSet = masterReportRuleSet ;
 }
 public string DisplayName (  ) {
 return "ReportRule" ;
 }
 public bool equals (  Object a ) {
 return a is ReportRule && base.Equals(a) ;
 }
 public ReportRule DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportRule _obj=((ReportRule)dbObj);
 _obj.Parent(ctx.cloneRef(parent)) ;
 }
 public ReportRule CloneInstance (  ReportRule cloneObj ) {
 base.CloneInstance(cloneObj) ;
 cloneObj.Parent(this.Parent()) ;
 return cloneObj ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }