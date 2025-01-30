namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class Category :  CreatableObject { public static int NAME = 0 ;
 
 public static int DESCRIPTION = 1 ;
 
 public static int COLOR = 2 ;
 
 private string name { get; set; } 
 private string description { get; set; } 
 private string color { get; set; } = "ffe91e63" ;
 
 private Category Old { get; set; } 
 public Category (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.Category ;
 }
 public string Type (  ) {
 return "Category" ;
 }
 public int FieldsCount (  ) {
 return 3 ;
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
 public string Description (  ) {
 _CheckProxy() ;
 return this.description ;
 }
 public void Description (  string description ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.description,description) ) {
 return ;
 }
 fieldChanged(DESCRIPTION,this.description,description) ;
 this.description = description ;
 }
 public string Color (  ) {
 _CheckProxy() ;
 return this.color ;
 }
 public void Color (  string color ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.color,color) ) {
 return ;
 }
 fieldChanged(COLOR,this.color,color) ;
 this.color = color ;
 }
 public Category getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((Category)old) ;
 }
 public string DisplayName (  ) {
 return this.getName() ;
 }
 public bool equals (  Object a ) {
 return a is Category && base.Equals(a) ;
 }
 public Category DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  Category _obj=((Category)dbObj);
 _obj.Name(name) ;
 _obj.Description(description) ;
 _obj.Color(color) ;
 }
 public Category CloneInstance (  Category cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new Category() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Name(this.Name()) ;
 cloneObj.Description(this.Description()) ;
 cloneObj.Color(this.Color()) ;
 return cloneObj ;
 }
 public string ToString (  ) {
 return DisplayName() ;
 }
 public Category CreateNewInstance (  ) {
 return new Category() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }