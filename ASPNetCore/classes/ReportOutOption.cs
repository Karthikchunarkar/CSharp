namespace classes ;
 using d3e.core; using store;  public class ReportOutOption :  DBObject { public static int _KEY = 0 ;
 
 public static int _VALUE = 1 ;
 
 private long id 
 private string key 
 private string value 
 public ReportOutOption (  ) {
 }
 public ReportOutOption (  string key, string value ) {
 this.Key = key ;
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
 public string getValue (  ) {
 return value ;
 }
 public void setValue (  string value ) {
 fieldChanged(_VALUE,this.value,value) ;
 this.value = value ;
 }
 public int _typeIdx (  ) {
 return SchemaConstants.ReportOutOption ;
 }
 public String _type (  ) {
 return "ReportOutOption" ;
 }
 public int _fieldsCount (  ) {
 return 2 ;
 }
 public void _convertToObjectRef (  ) {
 }
 }