namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportRuleSet :  ReportRule { public static int ALL = 1 ;
 
 public static int RULES = 2 ;
 
 private bool all { get; set; } = false ;
 
 private List<ReportRule> rules { get; set; } = D3EPersistanceList.child(RULES) ;
 
 private Report masterReport { get; set; } 
 private ReportRuleSet Old { get; set; } 
 public ReportRuleSet (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportRuleSet ;
 }
 public string Type (  ) {
 return "ReportRuleSet" ;
 }
 public int FieldsCount (  ) {
 return 3 ;
 }
 public DatabaseObject MasterObject (  ) {
  DatabaseObject master=base.MasterObject();
 if ( master != null ) {
 return master ;
 }
 if ( masterReport != null ) {
 return masterReport ;
 }
 return null ;
 }
 public void SetMasterObject (  DBObject master ) {
 base.SetMasterObject(master) ;
 if ( master is Report ) {
 masterReport = ((Report)master) ;
 }
 }
 public void AddToRules (  ReportRule val, long index ) {
 if ( index == -1 ) {
 rules.Add(val) ;
 }
 else {
 rules.Add(((int)index),val) ;
 }
 }
 public void RemoveFromRules (  ReportRule val ) {
 val._clearChildIdx() ;
 rules.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 foreach ( ReportRule obj in this.Rules() ) {
 visitor.accept(obj) ;
 obj.MasterReportRuleSet(this) ;
 obj.SetChildIdx(RULES) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportRule obj in this.Rules() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public void updateFlat (  DatabaseObject obj ) {
 super.updateFlat(obj) ;
 if ( masterReport != null ) {
 masterReport.UpdateFlat(obj) ;
 }
 }
 public bool IsAll (  ) {
 _CheckProxy() ;
 return this.all ;
 }
 public void All (  bool all ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.all,all) ) {
 return ;
 }
 fieldChanged(ALL,this.all,all) ;
 this.all = all ;
 }
 public List<ReportRule> Rules (  ) {
 return this.rules ;
 }
 public void Rules (  List<ReportRule> rules ) {
 if ( Objects.Equals(this.rules,rules) ) {
 return ;
 }
 ((D3EPersistanceList < ReportRule >)this.rules).SetAll(rules) ;
 }
 public Report MasterReport (  ) {
 return this.masterReport ;
 }
 public void MasterReport (  Report masterReport ) {
 this.masterReport = masterReport ;
 }
 public ReportRuleSet getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportRuleSet)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.Rules().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportRuleSet && base.Equals(a) ;
 }
 public ReportRuleSet DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(rules) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportRuleSet _obj=((ReportRuleSet)dbObj);
 _obj.All(all) ;
 ctx.cloneChildList(rules,(  v ) => _obj.Rules(v)) ;
 }
 public ReportRuleSet CloneInstance (  ReportRuleSet cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportRuleSet() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.All(this.IsAll()) ;
 cloneObj.Rules(Rules().Stream().Dictionary((  ReportRule colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportRuleSet CreateNewInstance (  ) {
 return new ReportRuleSet() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.rules) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case RULES: {
 this.ChildCollFieldChanged(childIdx,set,rules) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }