namespace models ;
 using d3e.core; using java.util; using java.util.function; using store;  public class Avatar :  DatabaseObject { public static int IMAGE = 0 ;
 
 public static int CREATEFROM = 1 ;
 
 private D3EImage image { get; set; } = new D3EImage() ;
 
 private string createFrom { get; set; } 
 private Avatar Old { get; set; } 
 public Avatar (  ) {
 this.image.setMasterAvatar(this) ;
 this.image._setChildIdx(IMAGE) ;
 }
 public int TypeIdx (  ) {
 return SchemaConstants.Avatar ;
 }
 public string Type (  ) {
 return "Avatar" ;
 }
 public int FieldsCount (  ) {
 return 2 ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 if ( image != null ) {
 image.MasterAvatar(this) ;
 image.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 if ( image != null ) {
 image.VisitChildren(visitor) ;
 }
 }
 public D3EImage Image (  ) {
 _CheckProxy() ;
 return this.image ;
 }
 public void Image (  D3EImage image ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.image,image) ) {
 this.image._updateChanges() ;
 return ;
 }
 fieldChanged(IMAGE,this.image,image) ;
 if ( image == null ) {
 image = new D3EImage() ;
 }
 this.image = image ;
 this.image.setMasterAvatar(this) ;
 this.image._setChildIdx(IMAGE) ;
 this.image._updateChanges() ;
 }
 public string CreateFrom (  ) {
 _CheckProxy() ;
 return this.createFrom ;
 }
 public void CreateFrom (  string createFrom ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.createFrom,createFrom) ) {
 return ;
 }
 fieldChanged(CREATEFROM,this.createFrom,createFrom) ;
 this.createFrom = createFrom ;
 }
 public Avatar getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((Avatar)old) ;
 }
 public string DisplayName (  ) {
 return "Avatar" ;
 }
 public bool equals (  Object a ) {
 return a is Avatar && base.Equals(a) ;
 }
 public Avatar DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChild(image) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  Avatar _obj=((Avatar)dbObj);
 ctx.cloneChild(image,(  v ) => _obj.Image(v)) ;
 _obj.CreateFrom(createFrom) ;
 }
 public Avatar CloneInstance (  Avatar cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new Avatar() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Image(Image().CloneInstance(null)) ;
 cloneObj.CreateFrom(this.CreateFrom()) ;
 return cloneObj ;
 }
 public Avatar CreateNewInstance (  ) {
 return new Avatar() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 _refs.Add(this.image.File()) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case IMAGE: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 } }
 }