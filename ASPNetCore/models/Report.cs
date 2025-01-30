namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using models; using store;  public class Report :  CreatableObject { public static int MODEL = 0 ;
 
 public static int NAME = 1 ;
 
 public static int CELLS = 2 ;
 
 public static int CONFIG = 3 ;
 
 public static int FILTERS = 4 ;
 
 public static int CRITERIA = 5 ;
 
 public static int FLATREPORTRULE = 6 ;
 
 private string model { get; set; } 
 private string name { get; set; } 
 private ReportCell cells { get; set; } 
 private ReportBaseConfig config { get; set; } 
 private List<ReportFilter> filters { get; set; } = D3EPersistanceList.child(FILTERS) ;
 
 private ReportRuleSet criteria { get; set; } 
 private List<ReportRule> flatReportRule = new ArrayList<>() ;
 
 public Report (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.Report ;
 }
 public string Type (  ) {
 return "Report" ;
 }
 public int FieldsCount (  ) {
 return 7 ;
 }
 public void AddToFilters (  ReportFilter val, long index ) {
 if ( index == -1 ) {
 filters.Add(val) ;
 }
 else {
 filters.Add(((int)index),val) ;
 }
 }
 public void RemoveFromFilters (  ReportFilter val ) {
 val._clearChildIdx() ;
 filters.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 flatReportRule.clear() ;
 base.UpdateMasters(visitor) ;
 if ( cells != null ) {
 visitor.accept(cells) ;
 cells.MasterReport(this) ;
 cells.UpdateMasters(visitor) ;
 }
 if ( config != null ) {
 visitor.accept(config) ;
 config.MasterReport(this) ;
 config.UpdateMasters(visitor) ;
 }
 foreach ( ReportFilter obj in this.Filters() ) {
 visitor.accept(obj) ;
 obj.MasterReport(this) ;
 obj.SetChildIdx(FILTERS) ;
 obj.UpdateMasters(visitor) ;
 }
 if ( criteria != null ) {
 visitor.accept(criteria) ;
 criteria.MasterReport(this) ;
 criteria.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 if ( cells != null ) {
 visitor.accept(cells) ;
 cells.VisitChildren(visitor) ;
 }
 if ( config != null ) {
 visitor.accept(config) ;
 config.VisitChildren(visitor) ;
 }
 foreach ( ReportFilter obj in this.Filters() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 if ( criteria != null ) {
 visitor.accept(criteria) ;
 criteria.VisitChildren(visitor) ;
 }
 }
 public void updateFlat (  DatabaseObject obj ) {
 super.updateFlat(obj) ;
 if ( obj is ReportRule ) {
 flatReportRule.Add(((ReportRule)obj)) ;
 }
 }
 public string Model (  ) {
 _CheckProxy() ;
 return this.model ;
 }
 public void Model (  string model ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.model,model) ) {
 return ;
 }
 fieldChanged(MODEL,this.model,model) ;
 this.model = model ;
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
 public ReportCell Cells (  ) {
 _CheckProxy() ;
 return this.cells ;
 }
 public void Cells (  ReportCell cells ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.cells,cells) ) {
 if ( this.cells != null ) {
 this.cells._updateChanges() ;
 }
 return ;
 }
 fieldChanged(CELLS,this.cells,cells) ;
 this.cells = cells ;
 if ( this.cells != null ) {
 this.cells.setMasterReport(this) ;
 this.cells._setChildIdx(CELLS) ;
 this.cells._updateChanges() ;
 }
 }
 public ReportBaseConfig Config (  ) {
 _CheckProxy() ;
 return this.config ;
 }
 public void Config (  ReportBaseConfig config ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.config,config) ) {
 if ( this.config != null ) {
 this.config._updateChanges() ;
 }
 return ;
 }
 fieldChanged(CONFIG,this.config,config) ;
 this.config = config ;
 if ( this.config != null ) {
 this.config.setMasterReport(this) ;
 this.config._setChildIdx(CONFIG) ;
 this.config._updateChanges() ;
 }
 }
 public List<ReportFilter> Filters (  ) {
 return this.filters ;
 }
 public void Filters (  List<ReportFilter> filters ) {
 if ( Objects.Equals(this.filters,filters) ) {
 return ;
 }
 ((D3EPersistanceList < ReportFilter >)this.filters).SetAll(filters) ;
 }
 public ReportRuleSet Criteria (  ) {
 _CheckProxy() ;
 return this.criteria ;
 }
 public void Criteria (  ReportRuleSet criteria ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.criteria,criteria) ) {
 if ( this.criteria != null ) {
 this.criteria._updateChanges() ;
 }
 return ;
 }
 fieldChanged(CRITERIA,this.criteria,criteria) ;
 this.criteria = criteria ;
 if ( this.criteria != null ) {
 this.criteria.setMasterReport(this) ;
 this.criteria._setChildIdx(CRITERIA) ;
 this.criteria._updateChanges() ;
 }
 }
 public List<ReportRule> FlatReportRule (  ) {
 return this.flatReportRule ;
 }
 public string DisplayName (  ) {
 return "Report" ;
 }
 public bool equals (  Object a ) {
 return a is Report && base.Equals(a) ;
 }
 public Report DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChild(cells) ;
 ctx.CollectChild(config) ;
 ctx.CollectChilds(filters) ;
 ctx.CollectChild(criteria) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  Report _obj=((Report)dbObj);
 _obj.Model(model) ;
 _obj.Name(name) ;
 ctx.cloneChild(cells,(  v ) => _obj.Cells(v)) ;
 ctx.cloneChild(config,(  v ) => _obj.Config(v)) ;
 ctx.cloneChildList(filters,(  v ) => _obj.Filters(v)) ;
 ctx.cloneChild(criteria,(  v ) => _obj.Criteria(v)) ;
 }
 public Report CloneInstance (  Report cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new Report() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Model(this.Model()) ;
 cloneObj.Name(this.Name()) ;
 cloneObj.Cells(Cells() == null ? null : Cells().CloneInstance(null)) ;
 cloneObj.Config(Config() == null ? null : Config().CloneInstance(null)) ;
 cloneObj.Filters(Filters().Stream().Dictionary((  ReportFilter colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Criteria(Criteria() == null ? null : Criteria().CloneInstance(null)) ;
 return cloneObj ;
 }
 public Report CreateNewInstance (  ) {
 return new Report() ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.collectCreatableReferences(_refs,this.cells) ;
 Database.collectCreatableReferences(_refs,this.config) ;
 Database.CollectCollctionCreatableReferences(_refs,this.filters) ;
 Database.collectCreatableReferences(_refs,this.criteria) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case CELLS: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 case CONFIG: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 case FILTERS: {
 this.ChildCollFieldChanged(childIdx,set,filters) ;
 break; }
 case CRITERIA: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 } }
 }