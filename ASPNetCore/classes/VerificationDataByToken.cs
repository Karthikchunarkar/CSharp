namespace classes ;
 using classes; using d3e.core; using java.util; using lists; using models; using store;  public class VerificationDataByToken :  DBObject { public static int _STATUS = 0 ;
 
 public static int _ERRORS = 1 ;
 
 public static int _ITEMS = 2 ;
 
 private long id 
 private ResultStatus status 
 private List < string > errors = new D3EPersistanceList<>(this,_ERRORS) ;
 
 private List < VerificationData > items = new D3EPersistanceList<>(this,_ITEMS) ;
 
 private List < TypeAndId > itemsRef 
 public VerificationDataByToken (  ) {
 }
 public VerificationDataByToken (  List < string > errors, List < VerificationData > items, ResultStatus status ) {
 this.Errors.addAll(errors) ;
 this.Items.addAll(items) ;
 this.Status = status ;
 }
 public long getId (  ) {
 return id ;
 }
 public void setId (  long id ) {
 this.id = id ;
 }
 public ResultStatus getStatus (  ) {
 return status ;
 }
 public void setStatus (  ResultStatus status ) {
 fieldChanged(_STATUS,this.status,status) ;
 this.status = status ;
 }
 public List < string > getErrors (  ) {
 return errors ;
 }
 public void setErrors (  List < string > errors ) {
 ((D3EPersistanceList < string >)this.errors).setAll(errors) ;
 }
 public void addToErrors (  string val, long index ) {
 if ( index == -1 ) {
 this.errors.add(val) ;
 }
 else {
 this.errors.add(((int)index),val) ;
 }
 }
 public void removeFromErrors (  string val ) {
 this.errors.remove(val) ;
 }
 public List < VerificationData > getItems (  ) {
 return items ;
 }
 public List < TypeAndId > getItemsRef (  ) {
 return itemsRef ;
 }
 public void setItems (  List < VerificationData > items ) {
 ((D3EPersistanceList < VerificationData >)this.items).setAll(items) ;
 }
 public void setItemsRef (  List < TypeAndId > itemsRef ) {
 if ( this.itemsRef != null ) {
 ((D3EPersistanceList < TypeAndId >)this.itemsRef).setAll(itemsRef) ;
 }
 else {
 this.itemsRef = itemsRef ;
 }
 }
 public void addToItems (  VerificationData val, long index ) {
 if ( index == -1 ) {
 this.items.add(val) ;
 }
 else {
 this.items.add(((int)index),val) ;
 }
 }
 public void addToItemsRef (  TypeAndId val, long index ) {
 if ( index == -1 ) {
 this.itemsRef.add(val) ;
 }
 else {
 this.itemsRef.add(((int)index),val) ;
 }
 }
 public void removeFromItems (  VerificationData val ) {
 this.items.remove(val) ;
 }
 public void removeFromItemsRef (  TypeAndId val ) {
 this.itemsRef.remove(val) ;
 }
 public int _typeIdx (  ) {
 return SchemaConstants.VerificationDataByToken ;
 }
 public String _type (  ) {
 return "VerificationDataByToken" ;
 }
 public int _fieldsCount (  ) {
 return 3 ;
 }
 public void _convertToObjectRef (  ) {
 this.itemsRef = TypeAndId.fromList(this.items,_ITEMS,this) ;
 this.items.clear() ;
 }
 }