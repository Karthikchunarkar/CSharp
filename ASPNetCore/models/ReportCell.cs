namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class ReportCell :  DatabaseObject { public static int TYPE = 0 ;
 
 public static int X = 1 ;
 
 public static int Y = 2 ;
 
 public static int STYLE = 3 ;
 
 public static int VALUE = 4 ;
 
 private ReportCellType type { get; set; } = ReportCellType.Data ;
 
 private int x { get; set; } = 0 ;
 
 private int y { get; set; } = 0 ;
 
 private ReportCellStyle style { get; set; } 
 private string value { get; set; } 
 private Report masterReport { get; set; } 
 private ReportCell Old { get; set; } 
 public ReportCell (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportCell ;
 }
 public string Type (  ) {
 return "ReportCell" ;
 }
 public int FieldsCount (  ) {
 return 5 ;
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
 if ( style != null ) {
 visitor.accept(style) ;
 style.MasterReportCell(this) ;
 style.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 if ( style != null ) {
 visitor.accept(style) ;
 style.VisitChildren(visitor) ;
 }
 }
 public void updateFlat (  DatabaseObject obj ) {
 super.updateFlat(obj) ;
 if ( masterReport != null ) {
 masterReport.UpdateFlat(obj) ;
 }
 }
 public ReportCellType Type (  ) {
 _CheckProxy() ;
 return this.type ;
 }
 public void Type (  ReportCellType type ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.type,type) ) {
 return ;
 }
 fieldChanged(TYPE,this.type,type) ;
 this.type = type ;
 }
 public int X (  ) {
 _CheckProxy() ;
 return this.x ;
 }
 public void X (  int x ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.x,x) ) {
 return ;
 }
 fieldChanged(X,this.x,x) ;
 this.x = x ;
 }
 public int Y (  ) {
 _CheckProxy() ;
 return this.y ;
 }
 public void Y (  int y ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.y,y) ) {
 return ;
 }
 fieldChanged(Y,this.y,y) ;
 this.y = y ;
 }
 public ReportCellStyle Style (  ) {
 _CheckProxy() ;
 return this.style ;
 }
 public void Style (  ReportCellStyle style ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.style,style) ) {
 if ( this.style != null ) {
 this.style._updateChanges() ;
 }
 return ;
 }
 fieldChanged(STYLE,this.style,style) ;
 this.style = style ;
 if ( this.style != null ) {
 this.style.setMasterReportCell(this) ;
 this.style._setChildIdx(STYLE) ;
 this.style._updateChanges() ;
 }
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
 public Report MasterReport (  ) {
 return this.masterReport ;
 }
 public void MasterReport (  Report masterReport ) {
 this.masterReport = masterReport ;
 }
 public ReportCell getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportCell)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 if ( this.Style() != null ) {
 this.Style().recordOld(ctx) ;
 }
 }
 public string DisplayName (  ) {
 return "ReportCell" ;
 }
 public bool equals (  Object a ) {
 return a is ReportCell && base.Equals(a) ;
 }
 public ReportCell DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChild(style) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportCell _obj=((ReportCell)dbObj);
 _obj.Type(type) ;
 _obj.X(x) ;
 _obj.Y(y) ;
 ctx.cloneChild(style,(  v ) => _obj.Style(v)) ;
 _obj.Value(value) ;
 }
 public ReportCell CloneInstance (  ReportCell cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportCell() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Type(this.Type()) ;
 cloneObj.X(this.X()) ;
 cloneObj.Y(this.Y()) ;
 cloneObj.Style(Style() == null ? null : Style().CloneInstance(null)) ;
 cloneObj.Value(this.Value()) ;
 return cloneObj ;
 }
 public ReportCell CreateNewInstance (  ) {
 return new ReportCell() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.collectCreatableReferences(_refs,this.style) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case STYLE: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 } }
 }