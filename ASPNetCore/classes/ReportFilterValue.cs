namespace classes ;
 using d3e.core; using store;  public class ReportFilterValue :  DBObject { public static int _NAME = 0 ;
 
 public static int _VALUE = 1 ;
 
 private long id 
 private string name 
 private string value 
 public ReportFilterValue (  ) {
 }
 public ReportFilterValue (  string name, string value ) {
 this.Name = name ;
 this.Value = value ;
 }
 public long getId (  ) {
 return id ;
 }
 public void setId (  long id ) {
 this.id = id ;
 }
 public string getName (  ) {
 return name ;
 }
 public void setName (  string name ) {
 fieldChanged(_NAME,this.name,name) ;
 this.name = name ;
 }
 public string getValue (  ) {
 return value ;
 }
 public void setValue (  string value ) {
 fieldChanged(_VALUE,this.value,value) ;
 this.value = value ;
 }
 public int _typeIdx (  ) {
 return SchemaConstants.ReportFilterValue ;
 }
 public String _type (  ) {
 return "ReportFilterValue" ;
 }
 public int _fieldsCount (  ) {
 return 2 ;
 }
 public void _convertToObjectRef (  ) {
 }
 }