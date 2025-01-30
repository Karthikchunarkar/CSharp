namespace classes ;
 using classes; using d3e.core; using java.util; using store;  public class ReportOutCell :  DBObject { public static int _KEY = 0 ;
 
 public static int _TYPE = 1 ;
 
 public static int _VALUE = 2 ;
 
 public static int _ATTRIBUTES = 3 ;
 
 private long id 
 private string key 
 private string type 
 private string value 
 private List < ReportOutAttribute > attributes = new D3EPersistanceList<>(this,_ATTRIBUTES) ;
 
 public ReportOutCell (  ) {
 }
 public ReportOutCell (  List < ReportOutAttribute > attributes, string key, string type, string value ) {
 this.Attributes.addAll(attributes) ;
 this.Key = key ;
 this.Type = type ;
 this.Value = value ;
 }
 public long getId (  ) {
 return id ;
 }
 public void setId (  long id ) {
 this.id = id ;
 }
 public string getKey (  ) {
 return key ;
 }
 public void setKey (  string key ) {
 fieldChanged(_KEY,this.key,key) ;
 this.key = key ;
 }
 public string getType (  ) {
 return type ;
 }
 public void setType (  string type ) {
 fieldChanged(_TYPE,this.type,type) ;
 this.type = type ;
 }
 public string getValue (  ) {
 return value ;
 }
 public void setValue (  string value ) {
 fieldChanged(_VALUE,this.value,value) ;
 this.value = value ;
 }
 public List < ReportOutAttribute > getAttributes (  ) {
 return attributes ;
 }
 public void setAttributes (  List < ReportOutAttribute > attributes ) {
 ((D3EPersistanceList < ReportOutAttribute >)this.attributes).setAll(attributes) ;
 }
 public void addToAttributes (  ReportOutAttribute val, long index ) {
 if ( index == -1 ) {
 this.attributes.add(val) ;
 }
 else {
 this.attributes.add(((int)index),val) ;
 }
 }
 public void removeFromAttributes (  ReportOutAttribute val ) {
 this.attributes.remove(val) ;
 }
 public int _typeIdx (  ) {
 return SchemaConstants.ReportOutCell ;
 }
 public String _type (  ) {
 return "ReportOutCell" ;
 }
 public int _fieldsCount (  ) {
 return 4 ;
 }
 public void _convertToObjectRef (  ) {
 this.attributes.forEach((  a ) => a._convertToObjectRef()) ;
 }
 }