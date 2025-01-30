namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class ReportConfigOption :  DatabaseObject { public static int IDENTITY = 0 ;
 
 public static int VALUE = 1 ;
 
 private string identity { get; set; } 
 private string value { get; set; } 
 private ReportConfig masterReportConfig { get; set; } 
 private ReportConfigOption Old { get; set; } 
 public ReportConfigOption (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportConfigOption ;
 }
 public string Type (  ) {
 return "ReportConfigOption" ;
 }
 public int FieldsCount (  ) {
 return 2 ;
 }
 public DatabaseObject MasterObject (  ) {
  DatabaseObject master=base.MasterObject();
 if ( master != null ) {
 return master ;
 }
 if ( masterReportConfig != null ) {
 return masterReportConfig ;
 }
 return null ;
 }
 public void SetMasterObject (  DBObject master ) {
 base.SetMasterObject(master) ;
 if ( master is ReportConfig ) {
 masterReportConfig = ((ReportConfig)master) ;
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
 if ( masterReportConfig != null ) {
 masterReportConfig.UpdateFlat(obj) ;
 }
 }
 public string Identity (  ) {
 _CheckProxy() ;
 return this.identity ;
 }
 public void Identity (  string identity ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.identity,identity) ) {
 return ;
 }
 fieldChanged(IDENTITY,this.identity,identity) ;
 this.identity = identity ;
 }
 public string Value (  ) {
 _CheckProxy() ;
 return this.value ;
 }
 public void Value (  string value ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.value,value) ) {
 return ;
 }
 fieldChanged(VALUE,this.value,value) ;
 this.value = value ;
 }
 public ReportConfig MasterReportConfig (  ) {
 return this.masterReportConfig ;
 }
 public void MasterReportConfig (  ReportConfig masterReportConfig ) {
 this.masterReportConfig = masterReportConfig ;
 }
 public ReportConfigOption getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportConfigOption)old) ;
 }
 public string DisplayName (  ) {
 return "ReportConfigOption" ;
 }
 public bool equals (  Object a ) {
 return a is ReportConfigOption && base.Equals(a) ;
 }
 public ReportConfigOption DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportConfigOption _obj=((ReportConfigOption)dbObj);
 _obj.Identity(identity) ;
 _obj.Value(value) ;
 }
 public ReportConfigOption CloneInstance (  ReportConfigOption cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportConfigOption() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Identity(this.Identity()) ;
 cloneObj.Value(this.Value()) ;
 return cloneObj ;
 }
 public ReportConfigOption CreateNewInstance (  ) {
 return new ReportConfigOption() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }