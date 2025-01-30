namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class ReportDataSection :  DatabaseObject { public static int HEADER = 0 ;
 
 public static int COLUMNS = 1 ;
 
 private string header { get; set; } 
 private List<string> columns { get; set; } = D3EPersistanceList.primitive(COLUMNS) ;
 
 private ReportData masterReportData { get; set; } 
 private ReportDataSection Old { get; set; } 
 public ReportDataSection (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportDataSection ;
 }
 public string Type (  ) {
 return "ReportDataSection" ;
 }
 public int FieldsCount (  ) {
 return 2 ;
 }
 public DatabaseObject MasterObject (  ) {
  DatabaseObject master=base.MasterObject();
 if ( master != null ) {
 return master ;
 }
 if ( masterReportData != null ) {
 return masterReportData ;
 }
 return null ;
 }
 public void SetMasterObject (  DBObject master ) {
 base.SetMasterObject(master) ;
 if ( master is ReportData ) {
 masterReportData = ((ReportData)master) ;
 }
 }
 public void AddToColumns (  string val, long index ) {
 if ( index == -1 ) {
 columns.Add(val) ;
 }
 else {
 columns.Add(((int)index),val) ;
 }
 }
 public void RemoveFromColumns (  string val ) {
 columns.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public void updateFlat (  DatabaseObject obj ) {
 super.updateFlat(obj) ;
 if ( masterReportData != null ) {
 masterReportData.UpdateFlat(obj) ;
 }
 }
 public string Header (  ) {
 _CheckProxy() ;
 return this.header ;
 }
 public void Header (  string header ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.header,header) ) {
 return ;
 }
 fieldChanged(HEADER,this.header,header) ;
 this.header = header ;
 }
 public List<string> Columns (  ) {
 return this.columns ;
 }
 public void Columns (  List<string> columns ) {
 if ( Objects.Equals(this.columns,columns) ) {
 return ;
 }
 ((D3EPersistanceList < string >)this.columns).SetAll(columns) ;
 }
 public ReportData MasterReportData (  ) {
 return this.masterReportData ;
 }
 public void MasterReportData (  ReportData masterReportData ) {
 this.masterReportData = masterReportData ;
 }
 public ReportDataSection getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportDataSection)old) ;
 }
 public string DisplayName (  ) {
 return "ReportDataSection" ;
 }
 public bool equals (  Object a ) {
 return a is ReportDataSection && base.Equals(a) ;
 }
 public ReportDataSection DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportDataSection _obj=((ReportDataSection)dbObj);
 _obj.Header(header) ;
 _obj.Columns(columns) ;
 }
 public ReportDataSection CloneInstance (  ReportDataSection cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportDataSection() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Header(this.Header()) ;
 cloneObj.Columns(new ArrayList<>(Columns())) ;
 return cloneObj ;
 }
 public bool TransientModel (  ) {
 return true ;
 }
 public ReportDataSection CreateNewInstance (  ) {
 return new ReportDataSection() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }