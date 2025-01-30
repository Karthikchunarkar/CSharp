namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class SMSMessage :  CreatableObject { public static int FROM = 0 ;
 
 public static int TO = 1 ;
 
 public static int BODY = 2 ;
 
 public static int CREATEDON = 3 ;
 
 public static int DLTTEMPLATEID = 4 ;
 
 private string from { get; set; } 
 private List<string> to { get; set; } = D3EPersistanceList.primitive(TO) ;
 
 private string body { get; set; } 
 private DateTime createdOn { get; set; } 
 private string dltTemplateId { get; set; } 
 public SMSMessage (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.SMSMessage ;
 }
 public string Type (  ) {
 return "SMSMessage" ;
 }
 public int FieldsCount (  ) {
 return 5 ;
 }
 public void AddToTo (  string val, long index ) {
 if ( index == -1 ) {
 to.Add(val) ;
 }
 else {
 to.Add(((int)index),val) ;
 }
 }
 public void RemoveFromTo (  string val ) {
 to.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public string From (  ) {
 _CheckProxy() ;
 return this.from ;
 }
 public void From (  string from ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.from,from) ) {
 return ;
 }
 fieldChanged(FROM,this.from,from) ;
 this.from = from ;
 }
 public List<string> To (  ) {
 return this.to ;
 }
 public void To (  List<string> to ) {
 if ( Objects.Equals(this.to,to) ) {
 return ;
 }
 ((D3EPersistanceList < string >)this.to).SetAll(to) ;
 }
 public string Body (  ) {
 _CheckProxy() ;
 return this.body ;
 }
 public void Body (  string body ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.body,body) ) {
 return ;
 }
 fieldChanged(BODY,this.body,body) ;
 this.body = body ;
 }
 public DateTime CreatedOn (  ) {
 _CheckProxy() ;
 return this.createdOn ;
 }
 public void CreatedOn (  DateTime createdOn ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.createdOn,createdOn) ) {
 return ;
 }
 fieldChanged(CREATEDON,this.createdOn,createdOn) ;
 this.createdOn = createdOn ;
 }
 public string DltTemplateId (  ) {
 _CheckProxy() ;
 return this.dltTemplateId ;
 }
 public void DltTemplateId (  string dltTemplateId ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.dltTemplateId,dltTemplateId) ) {
 return ;
 }
 fieldChanged(DLTTEMPLATEID,this.dltTemplateId,dltTemplateId) ;
 this.dltTemplateId = dltTemplateId ;
 }
 public string DisplayName (  ) {
 return "SMSMessage" ;
 }
 public bool equals (  Object a ) {
 return a is SMSMessage && base.Equals(a) ;
 }
 public SMSMessage DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  SMSMessage _obj=((SMSMessage)dbObj);
 _obj.From(from) ;
 _obj.To(to) ;
 _obj.Body(body) ;
 _obj.CreatedOn(createdOn) ;
 _obj.DltTemplateId(dltTemplateId) ;
 }
 public SMSMessage CloneInstance (  SMSMessage cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new SMSMessage() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.From(this.From()) ;
 cloneObj.To(new ArrayList<>(To())) ;
 cloneObj.Body(this.Body()) ;
 cloneObj.CreatedOn(this.CreatedOn()) ;
 cloneObj.DltTemplateId(this.DltTemplateId()) ;
 return cloneObj ;
 }
 public bool TransientModel (  ) {
 return true ;
 }
 public SMSMessage CreateNewInstance (  ) {
 return new SMSMessage() ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }