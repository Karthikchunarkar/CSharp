namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class ReportDataRow :  DatabaseObject { public static int ROW = 0 ;
 
 private List<string> row { get; set; } = D3EPersistanceList.primitive(ROW) ;
 
 private ReportData masterReportData { get; set; } 
 private ReportDataRow Old { get; set; } 
 public ReportDataRow (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportDataRow ;
 }
 public string Type (  ) {
 return "ReportDataRow" ;
 }
 public int FieldsCount (  ) {
 return 1 ;
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
 public void AddToRow (  string val, long index ) {
 if ( index == -1 ) {
 row.Add(val) ;
 }
 else {
 row.Add(((int)index),val) ;
 }
 }
 public void RemoveFromRow (  string val ) {
 row.Remove(val) ;
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
 public List<string> Row (  ) {
 return this.row ;
 }
 public void Row (  List<string> row ) {
 if ( Objects.Equals(this.row,row) ) {
 return ;
 }
 ((D3EPersistanceList < string >)this.row).SetAll(row) ;
 }
 public ReportData MasterReportData (  ) {
 return this.masterReportData ;
 }
 public void MasterReportData (  ReportData masterReportData ) {
 this.masterReportData = masterReportData ;
 }
 public ReportDataRow getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportDataRow)old) ;
 }
 public string DisplayName (  ) {
 return "ReportDataRow" ;
 }
 public bool equals (  Object a ) {
 return a is ReportDataRow && base.Equals(a) ;
 }
 public ReportDataRow DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportDataRow _obj=((ReportDataRow)dbObj);
 _obj.Row(row) ;
 }
 public ReportDataRow CloneInstance (  ReportDataRow cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportDataRow() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Row(new ArrayList<>(Row())) ;
 return cloneObj ;
 }
 public bool TransientModel (  ) {
 return true ;
 }
 public ReportDataRow CreateNewInstance (  ) {
 return new ReportDataRow() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }