namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class ReportNamedConditionFilter :  ReportFilter { public static int NAME = 0 ;
 
 public static int CONDITIONS = 1 ;
 
 private string name { get; set; } 
 private List<ReportNamedCondition> conditions { get; set; } = D3EPersistanceList.reference(CONDITIONS) ;
 
 private ReportNamedConditionFilter Old { get; set; } 
 public ReportNamedConditionFilter (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportNamedConditionFilter ;
 }
 public string Type (  ) {
 return "ReportNamedConditionFilter" ;
 }
 public int FieldsCount (  ) {
 return 2 ;
 }
 public void AddToConditions (  ReportNamedCondition val, long index ) {
 if ( index == -1 ) {
 conditions.Add(val) ;
 }
 else {
 conditions.Add(((int)index),val) ;
 }
 }
 public void RemoveFromConditions (  ReportNamedCondition val ) {
 conditions.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
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
 public List<ReportNamedCondition> Conditions (  ) {
 return this.conditions ;
 }
 public void Conditions (  List<ReportNamedCondition> conditions ) {
 if ( Objects.Equals(this.conditions,conditions) ) {
 return ;
 }
 ((D3EPersistanceList < ReportNamedCondition >)this.conditions).SetAll(conditions) ;
 }
 public ReportNamedConditionFilter getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportNamedConditionFilter)old) ;
 }
 public bool equals (  Object a ) {
 return a is ReportNamedConditionFilter && base.Equals(a) ;
 }
 public ReportNamedConditionFilter DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportNamedConditionFilter _obj=((ReportNamedConditionFilter)dbObj);
 _obj.Name(name) ;
 _obj.Conditions(ctx.cloneRefList(conditions)) ;
 }
 public ReportNamedConditionFilter CloneInstance (  ReportNamedConditionFilter cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportNamedConditionFilter() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Name(this.Name()) ;
 cloneObj.Conditions(new ArrayList<>(Conditions())) ;
 return cloneObj ;
 }
 public ReportNamedConditionFilter CreateNewInstance (  ) {
 return new ReportNamedConditionFilter() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 base.HandleChildChange(childIdx,set) ;
 }
 }