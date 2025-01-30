namespace classes ;
 using classes; using d3e.core; using java.util; using store;  public class ReportOutRow :  DBObject { public static int _KEY = 0 ;
 
 public static int _PARENTKEY = 1 ;
 
 public static int _CELLS = 2 ;
 
 public static int _GROUPINGKEY = 3 ;
 
 private long id 
 private string key 
 private string parentKey 
 private List < ReportOutCell > cells = new D3EPersistanceList<>(this,_CELLS) ;
 
 private string groupingKey 
 public ReportOutRow (  ) {
 }
 public ReportOutRow (  List < ReportOutCell > cells, string groupingKey, string key, string parentKey ) {
 this.Cells.addAll(cells) ;
 this.GroupingKey = groupingKey ;
 this.Key = key ;
 this.ParentKey = parentKey ;
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
 public string getParentKey (  ) {
 return parentKey ;
 }
 public void setParentKey (  string parentKey ) {
 fieldChanged(_PARENTKEY,this.parentKey,parentKey) ;
 this.parentKey = parentKey ;
 }
 public List < ReportOutCell > getCells (  ) {
 return cells ;
 }
 public void setCells (  List < ReportOutCell > cells ) {
 ((D3EPersistanceList < ReportOutCell >)this.cells).setAll(cells) ;
 }
 public void addToCells (  ReportOutCell val, long index ) {
 if ( index == -1 ) {
 this.cells.add(val) ;
 }
 else {
 this.cells.add(((int)index),val) ;
 }
 }
 public void removeFromCells (  ReportOutCell val ) {
 this.cells.remove(val) ;
 }
 public string getGroupingKey (  ) {
 return groupingKey ;
 }
 public void setGroupingKey (  string groupingKey ) {
 fieldChanged(_GROUPINGKEY,this.groupingKey,groupingKey) ;
 this.groupingKey = groupingKey ;
 }
 public int _typeIdx (  ) {
 return SchemaConstants.ReportOutRow ;
 }
 public String _type (  ) {
 return "ReportOutRow" ;
 }
 public int _fieldsCount (  ) {
 return 4 ;
 }
 public void _convertToObjectRef (  ) {
 this.cells.forEach((  a ) => a._convertToObjectRef()) ;
 }
 }