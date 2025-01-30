namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportData :  CreatableObject { public static int SECTIONS = 0 ;
 
 public static int ROWS = 1 ;
 
 private List<ReportDataSection> sections { get; set; } = D3EPersistanceList.child(SECTIONS) ;
 
 private List<ReportDataRow> rows { get; set; } = D3EPersistanceList.child(ROWS) ;
 
 public ReportData (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportData ;
 }
 public string Type (  ) {
 return "ReportData" ;
 }
 public int FieldsCount (  ) {
 return 2 ;
 }
 public void AddToSections (  ReportDataSection val, long index ) {
 if ( index == -1 ) {
 sections.Add(val) ;
 }
 else {
 sections.Add(((int)index),val) ;
 }
 }
 public void RemoveFromSections (  ReportDataSection val ) {
 val._clearChildIdx() ;
 sections.Remove(val) ;
 }
 public void AddToRows (  ReportDataRow val, long index ) {
 if ( index == -1 ) {
 rows.Add(val) ;
 }
 else {
 rows.Add(((int)index),val) ;
 }
 }
 public void RemoveFromRows (  ReportDataRow val ) {
 val._clearChildIdx() ;
 rows.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 foreach ( ReportDataSection obj in this.Sections() ) {
 visitor.accept(obj) ;
 obj.MasterReportData(this) ;
 obj.SetChildIdx(SECTIONS) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportDataRow obj in this.Rows() ) {
 visitor.accept(obj) ;
 obj.MasterReportData(this) ;
 obj.SetChildIdx(ROWS) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportDataSection obj in this.Sections() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportDataRow obj in this.Rows() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public List<ReportDataSection> Sections (  ) {
 return this.sections ;
 }
 public void Sections (  List<ReportDataSection> sections ) {
 if ( Objects.Equals(this.sections,sections) ) {
 return ;
 }
 ((D3EPersistanceList < ReportDataSection >)this.sections).SetAll(sections) ;
 }
 public List<ReportDataRow> Rows (  ) {
 return this.rows ;
 }
 public void Rows (  List<ReportDataRow> rows ) {
 if ( Objects.Equals(this.rows,rows) ) {
 return ;
 }
 ((D3EPersistanceList < ReportDataRow >)this.rows).SetAll(rows) ;
 }
 public string DisplayName (  ) {
 return "ReportData" ;
 }
 public bool equals (  Object a ) {
 return a is ReportData && base.Equals(a) ;
 }
 public ReportData DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(sections) ;
 ctx.CollectChilds(rows) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportData _obj=((ReportData)dbObj);
 ctx.cloneChildList(sections,(  v ) => _obj.Sections(v)) ;
 ctx.cloneChildList(rows,(  v ) => _obj.Rows(v)) ;
 }
 public ReportData CloneInstance (  ReportData cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportData() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Sections(Sections().Stream().Dictionary((  ReportDataSection colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Rows(Rows().Stream().Dictionary((  ReportDataRow colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public bool TransientModel (  ) {
 return true ;
 }
 public ReportData CreateNewInstance (  ) {
 return new ReportData() ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.sections) ;
 Database.CollectCollctionCreatableReferences(_refs,this.rows) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case SECTIONS: {
 this.ChildCollFieldChanged(childIdx,set,sections) ;
 break; }
 case ROWS: {
 this.ChildCollFieldChanged(childIdx,set,rows) ;
 break; }
 } }
 }