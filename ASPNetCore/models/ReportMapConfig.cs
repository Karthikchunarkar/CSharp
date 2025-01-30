namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using java.util.stream; using store;  public class ReportMapConfig :  ReportBaseConfig { public static int TYPE = 0 ;
 
 public static int LOCATION = 1 ;
 
 public static int LEGEND = 2 ;
 
 public static int LATITUDE = 3 ;
 
 public static int LONGITUDE = 4 ;
 
 public static int SIZE = 5 ;
 
 public static int TOOLTIPS = 6 ;
 
 private ReportMapType type { get; set; } = ReportMapType.Map ;
 
 private List<ReportField> location { get; set; } = D3EPersistanceList.child(LOCATION) ;
 
 private List<ReportField> legend { get; set; } = D3EPersistanceList.child(LEGEND) ;
 
 private ReportField latitude { get; set; } 
 private ReportField longitude { get; set; } 
 private List<ReportField> size { get; set; } = D3EPersistanceList.child(SIZE) ;
 
 private List<ReportField> tooltips { get; set; } = D3EPersistanceList.child(TOOLTIPS) ;
 
 private ReportMapConfig Old { get; set; } 
 public ReportMapConfig (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportMapConfig ;
 }
 public string Type (  ) {
 return "ReportMapConfig" ;
 }
 public int FieldsCount (  ) {
 return 7 ;
 }
 public void AddToLocation (  ReportField val, long index ) {
 if ( index == -1 ) {
 location.Add(val) ;
 }
 else {
 location.Add(((int)index),val) ;
 }
 }
 public void RemoveFromLocation (  ReportField val ) {
 val._clearChildIdx() ;
 location.Remove(val) ;
 }
 public void AddToLegend (  ReportField val, long index ) {
 if ( index == -1 ) {
 legend.Add(val) ;
 }
 else {
 legend.Add(((int)index),val) ;
 }
 }
 public void RemoveFromLegend (  ReportField val ) {
 val._clearChildIdx() ;
 legend.Remove(val) ;
 }
 public void AddToSize (  ReportField val, long index ) {
 if ( index == -1 ) {
 size.Add(val) ;
 }
 else {
 size.Add(((int)index),val) ;
 }
 }
 public void RemoveFromSize (  ReportField val ) {
 val._clearChildIdx() ;
 size.Remove(val) ;
 }
 public void AddToTooltips (  ReportField val, long index ) {
 if ( index == -1 ) {
 tooltips.Add(val) ;
 }
 else {
 tooltips.Add(((int)index),val) ;
 }
 }
 public void RemoveFromTooltips (  ReportField val ) {
 val._clearChildIdx() ;
 tooltips.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 foreach ( ReportField obj in this.Location() ) {
 visitor.accept(obj) ;
 obj.MasterReportMapConfig(this) ;
 obj.SetChildIdx(LOCATION) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Legend() ) {
 visitor.accept(obj) ;
 obj.MasterReportMapConfig(this) ;
 obj.SetChildIdx(LEGEND) ;
 obj.UpdateMasters(visitor) ;
 }
 if ( latitude != null ) {
 visitor.accept(latitude) ;
 latitude.MasterReportMapConfig(this) ;
 latitude.UpdateMasters(visitor) ;
 }
 if ( longitude != null ) {
 visitor.accept(longitude) ;
 longitude.MasterReportMapConfig(this) ;
 longitude.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Size() ) {
 visitor.accept(obj) ;
 obj.MasterReportMapConfig(this) ;
 obj.SetChildIdx(SIZE) ;
 obj.UpdateMasters(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.MasterReportMapConfig(this) ;
 obj.SetChildIdx(TOOLTIPS) ;
 obj.UpdateMasters(visitor) ;
 }
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 foreach ( ReportField obj in this.Location() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Legend() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 if ( latitude != null ) {
 visitor.accept(latitude) ;
 latitude.VisitChildren(visitor) ;
 }
 if ( longitude != null ) {
 visitor.accept(longitude) ;
 longitude.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Size() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 foreach ( ReportField obj in this.Tooltips() ) {
 visitor.accept(obj) ;
 obj.VisitChildren(visitor) ;
 }
 }
 public ReportMapType Type (  ) {
 _CheckProxy() ;
 return this.type ;
 }
 public void Type (  ReportMapType type ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.type,type) ) {
 return ;
 }
 fieldChanged(TYPE,this.type,type) ;
 this.type = type ;
 }
 public List<ReportField> Location (  ) {
 return this.location ;
 }
 public void Location (  List<ReportField> location ) {
 if ( Objects.Equals(this.location,location) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.location).SetAll(location) ;
 }
 public List<ReportField> Legend (  ) {
 return this.legend ;
 }
 public void Legend (  List<ReportField> legend ) {
 if ( Objects.Equals(this.legend,legend) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.legend).SetAll(legend) ;
 }
 public ReportField Latitude (  ) {
 _CheckProxy() ;
 return this.latitude ;
 }
 public void Latitude (  ReportField latitude ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.latitude,latitude) ) {
 if ( this.latitude != null ) {
 this.latitude._updateChanges() ;
 }
 return ;
 }
 fieldChanged(LATITUDE,this.latitude,latitude) ;
 this.latitude = latitude ;
 if ( this.latitude != null ) {
 this.latitude.setMasterReportMapConfig(this) ;
 this.latitude._setChildIdx(LATITUDE) ;
 this.latitude._updateChanges() ;
 }
 }
 public ReportField Longitude (  ) {
 _CheckProxy() ;
 return this.longitude ;
 }
 public void Longitude (  ReportField longitude ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.longitude,longitude) ) {
 if ( this.longitude != null ) {
 this.longitude._updateChanges() ;
 }
 return ;
 }
 fieldChanged(LONGITUDE,this.longitude,longitude) ;
 this.longitude = longitude ;
 if ( this.longitude != null ) {
 this.longitude.setMasterReportMapConfig(this) ;
 this.longitude._setChildIdx(LONGITUDE) ;
 this.longitude._updateChanges() ;
 }
 }
 public List<ReportField> Size (  ) {
 return this.size ;
 }
 public void Size (  List<ReportField> size ) {
 if ( Objects.Equals(this.size,size) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.size).SetAll(size) ;
 }
 public List<ReportField> Tooltips (  ) {
 return this.tooltips ;
 }
 public void Tooltips (  List<ReportField> tooltips ) {
 if ( Objects.Equals(this.tooltips,tooltips) ) {
 return ;
 }
 ((D3EPersistanceList < ReportField >)this.tooltips).SetAll(tooltips) ;
 }
 public ReportMapConfig getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportMapConfig)old) ;
 }
 public void recordOld (  CloneContext ctx ) {
 this.setOld(ctx.getFromCache(this)) ;
 this.Location().forEach((  one ) => one.recordOld(ctx)) ;
 this.Legend().forEach((  one ) => one.recordOld(ctx)) ;
 if ( this.Latitude() != null ) {
 this.Latitude().recordOld(ctx) ;
 }
 if ( this.Longitude() != null ) {
 this.Longitude().recordOld(ctx) ;
 }
 this.Size().forEach((  one ) => one.recordOld(ctx)) ;
 this.Tooltips().forEach((  one ) => one.recordOld(ctx)) ;
 }
 public bool equals (  Object a ) {
 return a is ReportMapConfig && base.Equals(a) ;
 }
 public ReportMapConfig DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void CollectChildValues (  CloneContext ctx ) {
 base.CollectChildValues(ctx) ;
 ctx.CollectChilds(location) ;
 ctx.CollectChilds(legend) ;
 ctx.CollectChild(latitude) ;
 ctx.CollectChild(longitude) ;
 ctx.CollectChilds(size) ;
 ctx.CollectChilds(tooltips) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportMapConfig _obj=((ReportMapConfig)dbObj);
 _obj.Type(type) ;
 ctx.cloneChildList(location,(  v ) => _obj.Location(v)) ;
 ctx.cloneChildList(legend,(  v ) => _obj.Legend(v)) ;
 ctx.cloneChild(latitude,(  v ) => _obj.Latitude(v)) ;
 ctx.cloneChild(longitude,(  v ) => _obj.Longitude(v)) ;
 ctx.cloneChildList(size,(  v ) => _obj.Size(v)) ;
 ctx.cloneChildList(tooltips,(  v ) => _obj.Tooltips(v)) ;
 }
 public ReportMapConfig CloneInstance (  ReportMapConfig cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportMapConfig() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Type(this.Type()) ;
 cloneObj.Location(Location().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Legend(Legend().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Latitude(Latitude() == null ? null : Latitude().CloneInstance(null)) ;
 cloneObj.Longitude(Longitude() == null ? null : Longitude().CloneInstance(null)) ;
 cloneObj.Size(Size().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 cloneObj.Tooltips(Tooltips().Stream().Dictionary((  ReportField colObj ) => colObj.CloneInstance(null)).Collect(Collectors.ToList())) ;
 return cloneObj ;
 }
 public ReportMapConfig CreateNewInstance (  ) {
 return new ReportMapConfig() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 Database.CollectCollctionCreatableReferences(_refs,this.location) ;
 Database.CollectCollctionCreatableReferences(_refs,this.legend) ;
 Database.collectCreatableReferences(_refs,this.latitude) ;
 Database.collectCreatableReferences(_refs,this.longitude) ;
 Database.CollectCollctionCreatableReferences(_refs,this.size) ;
 Database.CollectCollctionCreatableReferences(_refs,this.tooltips) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 protected void HndleChildChange (  int childIdx, bool set ) {
 switch ( childIdx ) { case LOCATION: {
 this.ChildCollFieldChanged(childIdx,set,location) ;
 break; }
 case LEGEND: {
 this.ChildCollFieldChanged(childIdx,set,legend) ;
 break; }
 case LATITUDE: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 case LONGITUDE: {
 this.ChildFieldChanged(childIdx,set) ;
 break; }
 case SIZE: {
 this.ChildCollFieldChanged(childIdx,set,size) ;
 break; }
 case TOOLTIPS: {
 this.ChildCollFieldChanged(childIdx,set,tooltips) ;
 break; }
 default: {
 base.HandleChildChange(childIdx,set) ;
 }
 } }
 }