namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class ReportProperty :  DatabaseObject { public static int NAME = 0 ;
 
 public static int PROPERTY = 1 ;
 
 public static int TYPE = 2 ;
 
 public static int CHILD = 3 ;
 
 public static int COLLECTION = 4 ;
 
 public static int ISENUM = 5 ;
 
 public static int ISREFERENCE = 6 ;
 
 private string name { get; set; } 
 private string property { get; set; } 
 private string type { get; set; } 
 private bool child { get; set; } = false ;
 
 private bool collection { get; set; } = false ;
 
 private bool isEnum { get; set; } = false ;
 
 private bool isReference { get; set; } = false ;
 
 private ReportModel masterReportModel { get; set; } 
 private ReportProperty Old { get; set; } 
 public ReportProperty (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportProperty ;
 }
 public string Type (  ) {
 return "ReportProperty" ;
 }
 public int FieldsCount (  ) {
 return 7 ;
 }
 public DatabaseObject MasterObject (  ) {
  DatabaseObject master=base.MasterObject();
 if ( master != null ) {
 return master ;
 }
 if ( masterReportModel != null ) {
 return masterReportModel ;
 }
 return null ;
 }
 public void SetMasterObject (  DBObject master ) {
 base.SetMasterObject(master) ;
 if ( master is ReportModel ) {
 masterReportModel = ((ReportModel)master) ;
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
 if ( masterReportModel != null ) {
 masterReportModel.UpdateFlat(obj) ;
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
 public string Property (  ) {
 _CheckProxy() ;
 return this.property ;
 }
 public void Property (  string property ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.property,property) ) {
 return ;
 }
 fieldChanged(PROPERTY,this.property,property) ;
 this.property = property ;
 }
 public string Type (  ) {
 _CheckProxy() ;
 return this.type ;
 }
 public void Type (  string type ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.type,type) ) {
 return ;
 }
 fieldChanged(TYPE,this.type,type) ;
 this.type = type ;
 }
 public bool IsChild (  ) {
 _CheckProxy() ;
 return this.child ;
 }
 public void Child (  bool child ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.child,child) ) {
 return ;
 }
 fieldChanged(CHILD,this.child,child) ;
 this.child = child ;
 }
 public bool IsCollection (  ) {
 _CheckProxy() ;
 return this.collection ;
 }
 public void Collection (  bool collection ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.collection,collection) ) {
 return ;
 }
 fieldChanged(COLLECTION,this.collection,collection) ;
 this.collection = collection ;
 }
 public bool IsIsEnum (  ) {
 _CheckProxy() ;
 return this.isEnum ;
 }
 public void IsEnum (  bool isEnum ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.isEnum,isEnum) ) {
 return ;
 }
 fieldChanged(ISENUM,this.isEnum,isEnum) ;
 this.isEnum = isEnum ;
 }
 public bool IsIsReference (  ) {
 _CheckProxy() ;
 return this.isReference ;
 }
 public void IsReference (  bool isReference ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.isReference,isReference) ) {
 return ;
 }
 fieldChanged(ISREFERENCE,this.isReference,isReference) ;
 this.isReference = isReference ;
 }
 public ReportModel MasterReportModel (  ) {
 return this.masterReportModel ;
 }
 public void MasterReportModel (  ReportModel masterReportModel ) {
 this.masterReportModel = masterReportModel ;
 }
 public ReportProperty getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportProperty)old) ;
 }
 public string DisplayName (  ) {
 return this.getName() ;
 }
 public bool equals (  Object a ) {
 return a is ReportProperty && base.Equals(a) ;
 }
 public ReportProperty DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportProperty _obj=((ReportProperty)dbObj);
 _obj.Name(name) ;
 _obj.Property(property) ;
 _obj.Type(type) ;
 _obj.Child(child) ;
 _obj.Collection(collection) ;
 _obj.IsEnum(isEnum) ;
 _obj.IsReference(isReference) ;
 }
 public ReportProperty CloneInstance (  ReportProperty cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportProperty() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Name(this.Name()) ;
 cloneObj.Property(this.Property()) ;
 cloneObj.Type(this.Type()) ;
 cloneObj.Child(this.IsChild()) ;
 cloneObj.Collection(this.IsCollection()) ;
 cloneObj.IsEnum(this.IsIsEnum()) ;
 cloneObj.IsReference(this.IsIsReference()) ;
 return cloneObj ;
 }
 public string ToString (  ) {
 return DisplayName() ;
 }
 public bool TransientModel (  ) {
 return true ;
 }
 public ReportProperty CreateNewInstance (  ) {
 return new ReportProperty() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }