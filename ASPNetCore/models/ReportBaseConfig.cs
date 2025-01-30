namespace models ;
 using d3e.core; using java.util.function; using store;  public abstract class ReportBaseConfig :  DatabaseObject { private Report masterReport { get; set; } 
 public ReportBaseConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportBaseConfig ;
 }
 public string Type (  ) {
 return "ReportBaseConfig" ;
 }
 public int FieldsCount (  ) {
 return 0 ;
 }
 public DatabaseObject MasterObject (  ) {
  DatabaseObject master=base.MasterObject();
 if ( master != null ) {
 return master ;
 }
 if ( masterReport != null ) {
 return masterReport ;
 }
 return null ;
 }
 public void SetMasterObject (  DBObject master ) {
 base.SetMasterObject(master) ;
 if ( master is Report ) {
 masterReport = ((Report)master) ;
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
 if ( masterReport != null ) {
 masterReport.UpdateFlat(obj) ;
 }
 }
 public Report MasterReport (  ) {
 return this.masterReport ;
 }
 public void MasterReport (  Report masterReport ) {
 this.masterReport = masterReport ;
 }
 public string DisplayName (  ) {
 return "ReportBaseConfig" ;
 }
 public bool equals (  Object a ) {
 return a is ReportBaseConfig && base.Equals(a) ;
 }
 public ReportBaseConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public ReportBaseConfig CloneInstance (  ReportBaseConfig cloneObj ) {
 base.CloneInstance(cloneObj) ;
 return cloneObj ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }