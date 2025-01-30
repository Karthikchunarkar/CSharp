namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class ReportPropertyFilter :  ReportFilter { public static int NAME = 0 ;
 
 public static int PROPERTY = 1 ;
 
 public static int TYPE = 2 ;
 
 public static int ISENUM = 3 ;
 
 public static int ISREFERENCE = 4 ;
 
 public static int ALLOWMULTIPLE = 5 ;
 
 public static int APPLYRANGE = 6 ;
 
 private string name { get; set; } 
 private string property { get; set; } 
 private string type { get; set; } 
 private bool isEnum { get; set; } = false ;
 
 private bool isReference { get; set; } = false ;
 
 private bool allowMultiple { get; set; } = false ;
 
 private bool applyRange { get; set; } = false ;
 
 private ReportPropertyFilter Old { get; set; } 
 public ReportPropertyFilter (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportPropertyFilter ;
 }
 public string Type (  ) {
 return "ReportPropertyFilter" ;
 }
 public int FieldsCount (  ) {
 return 7 ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
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
 public bool IsAllowMultiple (  ) {
 _CheckProxy() ;
 return this.allowMultiple ;
 }
 public void AllowMultiple (  bool allowMultiple ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.allowMultiple,allowMultiple) ) {
 return ;
 }
 fieldChanged(ALLOWMULTIPLE,this.allowMultiple,allowMultiple) ;
 this.allowMultiple = allowMultiple ;
 }
 public bool IsApplyRange (  ) {
 _CheckProxy() ;
 return this.applyRange ;
 }
 public void ApplyRange (  bool applyRange ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.applyRange,applyRange) ) {
 return ;
 }
 fieldChanged(APPLYRANGE,this.applyRange,applyRange) ;
 this.applyRange = applyRange ;
 }
 public ReportPropertyFilter getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportPropertyFilter)old) ;
 }
 public bool equals (  Object a ) {
 return a is ReportPropertyFilter && base.Equals(a) ;
 }
 public ReportPropertyFilter DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportPropertyFilter _obj=((ReportPropertyFilter)dbObj);
 _obj.Name(name) ;
 _obj.Property(property) ;
 _obj.Type(type) ;
 _obj.IsEnum(isEnum) ;
 _obj.IsReference(isReference) ;
 _obj.AllowMultiple(allowMultiple) ;
 _obj.ApplyRange(applyRange) ;
 }
 public ReportPropertyFilter CloneInstance (  ReportPropertyFilter cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportPropertyFilter() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Name(this.Name()) ;
 cloneObj.Property(this.Property()) ;
 cloneObj.Type(this.Type()) ;
 cloneObj.IsEnum(this.IsIsEnum()) ;
 cloneObj.IsReference(this.IsIsReference()) ;
 cloneObj.AllowMultiple(this.IsAllowMultiple()) ;
 cloneObj.ApplyRange(this.IsApplyRange()) ;
 return cloneObj ;
 }
 public ReportPropertyFilter CreateNewInstance (  ) {
 return new ReportPropertyFilter() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 base.HandleChildChange(childIdx,set) ;
 }
 }