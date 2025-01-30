namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportModel :  CreatableObject { public static int NAME = 0 ;
 
 public static int PROPERTIES = 1 ;
 
 private string name { get; set; } 
 private List<ReportProperty> properties { get; set; } = D3EPersistanceList.child(PROPERTIES) ;
 
 public ReportModel (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportModel ;
 }
 public string Type (  ) {
 return "ReportModel" ;
 }
 public int FieldsCount (  ) {
 return 2 ;
 }
 public void AddToProperties (  ReportProperty val, long index ) {
 if ( index == -1 ) {
 properties.Add(val) ;
 }
 else {
 properties.Add(((int)index),val) ;
 }
 }
 public void RemoveFromProperties (  ReportProperty val ) {
 val._clearChildIdx() ;
 properties.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 foreach ( ReportProperty obj in this.Properties() ) {
 visitor.accept(obj) ;
 obj.MasterReportModel(this) ;
 obj.SetChildIdx(PROPERTIES) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportProperty obj in this.Properties() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public string Name (  ) {
 _CheckProxy() ;
 return this.name ;
 }
 public void Name (  string name ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.name,name) ) {
 return ;
 }
 fieldChanged(NAME,this.name,name) ;
 this.name = name ;
 }
 public List<ReportProperty> Properties (  ) {
 return this.properties ;
 }
 public void Properties (  List<ReportProperty> properties ) {
 if ( Objects.Equals(this.properties,properties) ) {
 return ;
 }
 ((D3EPersistanceList < ReportProperty >)this.properties).SetAll(properties) ;
 }
 public string DisplayName (  ) {
 return this.getName() ;
 }
 public bool equals (  Object a ) {
 return a is ReportModel && base.Equals(a) ;
 }
 public ReportModel DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(properties) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportModel _obj=((ReportModel)dbObj);
 _obj.Name(name) ;
 ctx.cloneChildList(properties,(  v ) => _obj.Properties(v)) ;
 }
 public ReportModel CloneInstance (  ReportModel cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportModel() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Name(this.Name()) ;
 cloneObj.Properties(Properties().Stream().Dictionary((  ReportProperty colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public string ToString (  ) {
 return DisplayName() ;
 }
 public bool TransientModel (  ) {
 return true ;
 }
 public ReportModel CreateNewInstance (  ) {
 return new ReportModel() ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.properties) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case PROPERTIES: {
 this.ChildCollFieldChanged(childIdx,set,properties) ;
 break; }
 } }
 }