namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class EmailMessage :  CreatableObject { public static int FROM = 0 ;
 
 public static int TO = 1 ;
 
 public static int BODY = 2 ;
 
 public static int CREATEDON = 3 ;
 
 public static int BCC = 4 ;
 
 public static int CC = 5 ;
 
 public static int SUBJECT = 6 ;
 
 public static int HTML = 7 ;
 
 public static int INLINEATTACHMENTS = 8 ;
 
 public static int ATTACHMENTS = 9 ;
 
 private string from { get; set; } 
 private List<string> to { get; set; } = D3EPersistanceList.primitive(TO) ;
 
 private string body { get; set; } 
 private DateTime createdOn { get; set; } 
 private List<string> bcc { get; set; } = D3EPersistanceList.primitive(BCC) ;
 
 private List<string> cc { get; set; } = D3EPersistanceList.primitive(CC) ;
 
 private string subject { get; set; } 
 private bool html { get; set; } = false ;
 
 private List<DFile> inlineAttachments { get; set; } = D3EPersistanceList.primitive(INLINEATTACHMENTS) ;
 
 private List<DFile> attachments { get; set; } = D3EPersistanceList.primitive(ATTACHMENTS) ;
 
 public EmailMessage (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.EmailMessage ;
 }
 public string Type (  ) {
 return "EmailMessage" ;
 }
 public int FieldsCount (  ) {
 return 10 ;
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
 public void AddToBcc (  string val, long index ) {
 if ( index == -1 ) {
 bcc.Add(val) ;
 }
 else {
 bcc.Add(((int)index),val) ;
 }
 }
 public void RemoveFromBcc (  string val ) {
 bcc.Remove(val) ;
 }
 public void AddToCc (  string val, long index ) {
 if ( index == -1 ) {
 cc.Add(val) ;
 }
 else {
 cc.Add(((int)index),val) ;
 }
 }
 public void RemoveFromCc (  string val ) {
 cc.Remove(val) ;
 }
 public void AddToInlineAttachments (  DFile val, long index ) {
 if ( index == -1 ) {
 inlineAttachments.Add(val) ;
 }
 else {
 inlineAttachments.Add(((int)index),val) ;
 }
 }
 public void RemoveFromInlineAttachments (  DFile val ) {
 inlineAttachments.Remove(val) ;
 }
 public void AddToAttachments (  DFile val, long index ) {
 if ( index == -1 ) {
 attachments.Add(val) ;
 }
 else {
 attachments.Add(((int)index),val) ;
 }
 }
 public void RemoveFromAttachments (  DFile val ) {
 attachments.Remove(val) ;
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
 public List<string> Bcc (  ) {
 return this.bcc ;
 }
 public void Bcc (  List<string> bcc ) {
 if ( Objects.Equals(this.bcc,bcc) ) {
 return ;
 }
 ((D3EPersistanceList < string >)this.bcc).SetAll(bcc) ;
 }
 public List<string> Cc (  ) {
 return this.cc ;
 }
 public void Cc (  List<string> cc ) {
 if ( Objects.Equals(this.cc,cc) ) {
 return ;
 }
 ((D3EPersistanceList < string >)this.cc).SetAll(cc) ;
 }
 public string Subject (  ) {
 _CheckProxy() ;
 return this.subject ;
 }
 public void Subject (  string subject ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.subject,subject) ) {
 return ;
 }
 fieldChanged(SUBJECT,this.subject,subject) ;
 this.subject = subject ;
 }
 public bool IsHtml (  ) {
 _CheckProxy() ;
 return this.html ;
 }
 public void Html (  bool html ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.html,html) ) {
 return ;
 }
 fieldChanged(HTML,this.html,html) ;
 this.html = html ;
 }
 public List<DFile> InlineAttachments (  ) {
 return this.inlineAttachments ;
 }
 public void InlineAttachments (  List<DFile> inlineAttachments ) {
 if ( Objects.Equals(this.inlineAttachments,inlineAttachments) ) {
 return ;
 }
 ((D3EPersistanceList < DFile >)this.inlineAttachments).SetAll(inlineAttachments) ;
 }
 public List<DFile> Attachments (  ) {
 return this.attachments ;
 }
 public void Attachments (  List<DFile> attachments ) {
 if ( Objects.Equals(this.attachments,attachments) ) {
 return ;
 }
 ((D3EPersistanceList < DFile >)this.attachments).SetAll(attachments) ;
 }
 public string DisplayName (  ) {
 return "EmailMessage" ;
 }
 public bool equals (  Object a ) {
 return a is EmailMessage && base.Equals(a) ;
 }
 public EmailMessage DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  EmailMessage _obj=((EmailMessage)dbObj);
 _obj.From(from) ;
 _obj.To(to) ;
 _obj.Body(body) ;
 _obj.CreatedOn(createdOn) ;
 _obj.Bcc(bcc) ;
 _obj.Cc(cc) ;
 _obj.Subject(subject) ;
 _obj.Html(html) ;
 _obj.InlineAttachments(inlineAttachments) ;
 _obj.Attachments(attachments) ;
 }
 public EmailMessage CloneInstance (  EmailMessage cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new EmailMessage() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.From(this.From()) ;
 cloneObj.To(new ArrayList<>(To())) ;
 cloneObj.Body(this.Body()) ;
 cloneObj.CreatedOn(this.CreatedOn()) ;
 cloneObj.Bcc(new ArrayList<>(Bcc())) ;
 cloneObj.Cc(new ArrayList<>(Cc())) ;
 cloneObj.Subject(this.Subject()) ;
 cloneObj.Html(this.IsHtml()) ;
 cloneObj.InlineAttachments(new ArrayList<>(InlineAttachments())) ;
 cloneObj.Attachments(new ArrayList<>(Attachments())) ;
 return cloneObj ;
 }
 public bool TransientModel (  ) {
 return true ;
 }
 public EmailMessage CreateNewInstance (  ) {
 return new EmailMessage() ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 _refs.AddAll(this.inlineAttachments) ;
 _refs.AddAll(this.attachments) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }