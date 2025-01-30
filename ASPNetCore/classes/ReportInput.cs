namespace classes ;
 using d3e.core; using java.util; using store;  public class ReportInput :  DBObject { public static int _NAME = 0 ;
 
 public static int _VALUE = 1 ;
 
 public static int _VALUES = 2 ;
 
 private long id 
 private string name 
 private string value 
 private List < string > values = new D3EPersistanceList<>(this,_VALUES) ;
 
 public ReportInput (  ) {
 }
 public ReportInput (  string name, string value, List < string > values ) {
 this.Name = name ;
 this.Value = value ;
 this.Values.addAll(values) ;
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
 public List < string > getValues (  ) {
 return values ;
 }
 public void setValues (  List < string > values ) {
 ((D3EPersistanceList < string >)this.values).setAll(values) ;
 }
 public void addToValues (  string val, long index ) {
 if ( index == -1 ) {
 this.values.add(val) ;
 }
 else {
 this.values.add(((int)index),val) ;
 }
 }
 public void removeFromValues (  string val ) {
 this.values.remove(val) ;
 }
 public int _typeIdx (  ) {
 return SchemaConstants.ReportInput ;
 }
 public String _type (  ) {
 return "ReportInput" ;
 }
 public int _fieldsCount (  ) {
 return 3 ;
 }
 public void _convertToObjectRef (  ) {
 }
 }