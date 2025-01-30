namespace classes ;
 using classes;  public class DBOperation { public int Type { get; set; } = 0 ;
 
 public Object Data { get; set; } 
 public DBOperation (  int type, Object data ) {
 this.Type = type ;
 this.Data = data ;
 }
 public int getType (  ) {
 return this.type ;
 }
 public Object getData (  ) {
 return this.data ;
 }
 }