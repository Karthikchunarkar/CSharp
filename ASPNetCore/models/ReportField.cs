namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class ReportField :  DatabaseObject { public static int NAME = 0 ;
 
 public static int FIELD = 1 ;
 
 public static int AGGREGATE = 2 ;
 
 private string name { get; set; } 
 private string field { get; set; } 
 private ReportAggregateType aggregate { get; set; } = ReportAggregateType.None ;
 
 private ReportBarChartConfig masterReportBarChartConfig { get; set; } 
 private ReportCardConfig masterReportCardConfig { get; set; } 
 private ReportFunnelChartConfig masterReportFunnelChartConfig { get; set; } 
 private ReportGuageConfig masterReportGuageConfig { get; set; } 
 private ReportKPIConfig masterReportKPIConfig { get; set; } 
 private ReportKeyInfluencerConfig masterReportKeyInfluencerConfig { get; set; } 
 private ReportLineAndAreaChartConfig masterReportLineAndAreaChartConfig { get; set; } 
 private ReportLineAndColumnChartConfig masterReportLineAndColumnChartConfig { get; set; } 
 private ReportMapConfig masterReportMapConfig { get; set; } 
 private ReportMatrixConfig masterReportMatrixConfig { get; set; } 
 private ReportMultiRowCardConfig masterReportMultiRowCardConfig { get; set; } 
 private ReportPieChartConfig masterReportPieChartConfig { get; set; } 
 private ReportScatterChartConfig masterReportScatterChartConfig { get; set; } 
 private ReportSlicerConfig masterReportSlicerConfig { get; set; } 
 private ReportTableConfig masterReportTableConfig { get; set; } 
 private ReportWaterfallChartConfig masterReportWaterfallChartConfig { get; set; } 
 private ReportField Old { get; set; } 
 public ReportField (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.ReportField ;
 }
 public string Type (  ) {
 return "ReportField" ;
 }
 public int FieldsCount (  ) {
 return 3 ;
 }
 public DatabaseObject MasterObject (  ) {
  DatabaseObject master=base.MasterObject();
 if ( master != null ) {
 return master ;
 }
 if ( masterReportBarChartConfig != null ) {
 return masterReportBarChartConfig ;
 }
 if ( masterReportCardConfig != null ) {
 return masterReportCardConfig ;
 }
 if ( masterReportFunnelChartConfig != null ) {
 return masterReportFunnelChartConfig ;
 }
 if ( masterReportGuageConfig != null ) {
 return masterReportGuageConfig ;
 }
 if ( masterReportKPIConfig != null ) {
 return masterReportKPIConfig ;
 }
 if ( masterReportKeyInfluencerConfig != null ) {
 return masterReportKeyInfluencerConfig ;
 }
 if ( masterReportLineAndAreaChartConfig != null ) {
 return masterReportLineAndAreaChartConfig ;
 }
 if ( masterReportLineAndColumnChartConfig != null ) {
 return masterReportLineAndColumnChartConfig ;
 }
 if ( masterReportMapConfig != null ) {
 return masterReportMapConfig ;
 }
 if ( masterReportMatrixConfig != null ) {
 return masterReportMatrixConfig ;
 }
 if ( masterReportMultiRowCardConfig != null ) {
 return masterReportMultiRowCardConfig ;
 }
 if ( masterReportPieChartConfig != null ) {
 return masterReportPieChartConfig ;
 }
 if ( masterReportScatterChartConfig != null ) {
 return masterReportScatterChartConfig ;
 }
 if ( masterReportSlicerConfig != null ) {
 return masterReportSlicerConfig ;
 }
 if ( masterReportTableConfig != null ) {
 return masterReportTableConfig ;
 }
 if ( masterReportWaterfallChartConfig != null ) {
 return masterReportWaterfallChartConfig ;
 }
 return null ;
 }
 public void SetMasterObject (  DBObject master ) {
 base.SetMasterObject(master) ;
 if ( master is ReportBarChartConfig ) {
 masterReportBarChartConfig = ((ReportBarChartConfig)master) ;
 }
 if ( master is ReportCardConfig ) {
 masterReportCardConfig = ((ReportCardConfig)master) ;
 }
 if ( master is ReportFunnelChartConfig ) {
 masterReportFunnelChartConfig = ((ReportFunnelChartConfig)master) ;
 }
 if ( master is ReportGuageConfig ) {
 masterReportGuageConfig = ((ReportGuageConfig)master) ;
 }
 if ( master is ReportKPIConfig ) {
 masterReportKPIConfig = ((ReportKPIConfig)master) ;
 }
 if ( master is ReportKeyInfluencerConfig ) {
 masterReportKeyInfluencerConfig = ((ReportKeyInfluencerConfig)master) ;
 }
 if ( master is ReportLineAndAreaChartConfig ) {
 masterReportLineAndAreaChartConfig = ((ReportLineAndAreaChartConfig)master) ;
 }
 if ( master is ReportLineAndColumnChartConfig ) {
 masterReportLineAndColumnChartConfig = ((ReportLineAndColumnChartConfig)master) ;
 }
 if ( master is ReportMapConfig ) {
 masterReportMapConfig = ((ReportMapConfig)master) ;
 }
 if ( master is ReportMatrixConfig ) {
 masterReportMatrixConfig = ((ReportMatrixConfig)master) ;
 }
 if ( master is ReportMultiRowCardConfig ) {
 masterReportMultiRowCardConfig = ((ReportMultiRowCardConfig)master) ;
 }
 if ( master is ReportPieChartConfig ) {
 masterReportPieChartConfig = ((ReportPieChartConfig)master) ;
 }
 if ( master is ReportScatterChartConfig ) {
 masterReportScatterChartConfig = ((ReportScatterChartConfig)master) ;
 }
 if ( master is ReportSlicerConfig ) {
 masterReportSlicerConfig = ((ReportSlicerConfig)master) ;
 }
 if ( master is ReportTableConfig ) {
 masterReportTableConfig = ((ReportTableConfig)master) ;
 }
 if ( master is ReportWaterfallChartConfig ) {
 masterReportWaterfallChartConfig = ((ReportWaterfallChartConfig)master) ;
 }
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public void updateFlat (  DatabaseObject obj ) {
 super.updateFlat(obj) ;
 if ( masterReportBarChartConfig != null ) {
 masterReportBarChartConfig.UpdateFlat(obj) ;
 }
 if ( masterReportCardConfig != null ) {
 masterReportCardConfig.UpdateFlat(obj) ;
 }
 if ( masterReportFunnelChartConfig != null ) {
 masterReportFunnelChartConfig.UpdateFlat(obj) ;
 }
 if ( masterReportGuageConfig != null ) {
 masterReportGuageConfig.UpdateFlat(obj) ;
 }
 if ( masterReportKPIConfig != null ) {
 masterReportKPIConfig.UpdateFlat(obj) ;
 }
 if ( masterReportKeyInfluencerConfig != null ) {
 masterReportKeyInfluencerConfig.UpdateFlat(obj) ;
 }
 if ( masterReportLineAndAreaChartConfig != null ) {
 masterReportLineAndAreaChartConfig.UpdateFlat(obj) ;
 }
 if ( masterReportLineAndColumnChartConfig != null ) {
 masterReportLineAndColumnChartConfig.UpdateFlat(obj) ;
 }
 if ( masterReportMapConfig != null ) {
 masterReportMapConfig.UpdateFlat(obj) ;
 }
 if ( masterReportMatrixConfig != null ) {
 masterReportMatrixConfig.UpdateFlat(obj) ;
 }
 if ( masterReportMultiRowCardConfig != null ) {
 masterReportMultiRowCardConfig.UpdateFlat(obj) ;
 }
 if ( masterReportPieChartConfig != null ) {
 masterReportPieChartConfig.UpdateFlat(obj) ;
 }
 if ( masterReportScatterChartConfig != null ) {
 masterReportScatterChartConfig.UpdateFlat(obj) ;
 }
 if ( masterReportSlicerConfig != null ) {
 masterReportSlicerConfig.UpdateFlat(obj) ;
 }
 if ( masterReportTableConfig != null ) {
 masterReportTableConfig.UpdateFlat(obj) ;
 }
 if ( masterReportWaterfallChartConfig != null ) {
 masterReportWaterfallChartConfig.UpdateFlat(obj) ;
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
 public string Field (  ) {
 _CheckProxy() ;
 return this.field ;
 }
 public void Field (  string field ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.field,field) ) {
 return ;
 }
 fieldChanged(FIELD,this.field,field) ;
 this.field = field ;
 }
 public ReportAggregateType Aggregate (  ) {
 _CheckProxy() ;
 return this.aggregate ;
 }
 public void Aggregate (  ReportAggregateType aggregate ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.aggregate,aggregate) ) {
 return ;
 }
 fieldChanged(AGGREGATE,this.aggregate,aggregate) ;
 this.aggregate = aggregate ;
 }
 public ReportBarChartConfig MasterReportBarChartConfig (  ) {
 return this.masterReportBarChartConfig ;
 }
 public void MasterReportBarChartConfig (  ReportBarChartConfig masterReportBarChartConfig ) {
 this.masterReportBarChartConfig = masterReportBarChartConfig ;
 }
 public ReportCardConfig MasterReportCardConfig (  ) {
 return this.masterReportCardConfig ;
 }
 public void MasterReportCardConfig (  ReportCardConfig masterReportCardConfig ) {
 this.masterReportCardConfig = masterReportCardConfig ;
 }
 public ReportFunnelChartConfig MasterReportFunnelChartConfig (  ) {
 return this.masterReportFunnelChartConfig ;
 }
 public void MasterReportFunnelChartConfig (  ReportFunnelChartConfig masterReportFunnelChartConfig ) {
 this.masterReportFunnelChartConfig = masterReportFunnelChartConfig ;
 }
 public ReportGuageConfig MasterReportGuageConfig (  ) {
 return this.masterReportGuageConfig ;
 }
 public void MasterReportGuageConfig (  ReportGuageConfig masterReportGuageConfig ) {
 this.masterReportGuageConfig = masterReportGuageConfig ;
 }
 public ReportKPIConfig MasterReportKPIConfig (  ) {
 return this.masterReportKPIConfig ;
 }
 public void MasterReportKPIConfig (  ReportKPIConfig masterReportKPIConfig ) {
 this.masterReportKPIConfig = masterReportKPIConfig ;
 }
 public ReportKeyInfluencerConfig MasterReportKeyInfluencerConfig (  ) {
 return this.masterReportKeyInfluencerConfig ;
 }
 public void MasterReportKeyInfluencerConfig (  ReportKeyInfluencerConfig masterReportKeyInfluencerConfig ) {
 this.masterReportKeyInfluencerConfig = masterReportKeyInfluencerConfig ;
 }
 public ReportLineAndAreaChartConfig MasterReportLineAndAreaChartConfig (  ) {
 return this.masterReportLineAndAreaChartConfig ;
 }
 public void MasterReportLineAndAreaChartConfig (  ReportLineAndAreaChartConfig masterReportLineAndAreaChartConfig ) {
 this.masterReportLineAndAreaChartConfig = masterReportLineAndAreaChartConfig ;
 }
 public ReportLineAndColumnChartConfig MasterReportLineAndColumnChartConfig (  ) {
 return this.masterReportLineAndColumnChartConfig ;
 }
 public void MasterReportLineAndColumnChartConfig (  ReportLineAndColumnChartConfig masterReportLineAndColumnChartConfig ) {
 this.masterReportLineAndColumnChartConfig = masterReportLineAndColumnChartConfig ;
 }
 public ReportMapConfig MasterReportMapConfig (  ) {
 return this.masterReportMapConfig ;
 }
 public void MasterReportMapConfig (  ReportMapConfig masterReportMapConfig ) {
 this.masterReportMapConfig = masterReportMapConfig ;
 }
 public ReportMatrixConfig MasterReportMatrixConfig (  ) {
 return this.masterReportMatrixConfig ;
 }
 public void MasterReportMatrixConfig (  ReportMatrixConfig masterReportMatrixConfig ) {
 this.masterReportMatrixConfig = masterReportMatrixConfig ;
 }
 public ReportMultiRowCardConfig MasterReportMultiRowCardConfig (  ) {
 return this.masterReportMultiRowCardConfig ;
 }
 public void MasterReportMultiRowCardConfig (  ReportMultiRowCardConfig masterReportMultiRowCardConfig ) {
 this.masterReportMultiRowCardConfig = masterReportMultiRowCardConfig ;
 }
 public ReportPieChartConfig MasterReportPieChartConfig (  ) {
 return this.masterReportPieChartConfig ;
 }
 public void MasterReportPieChartConfig (  ReportPieChartConfig masterReportPieChartConfig ) {
 this.masterReportPieChartConfig = masterReportPieChartConfig ;
 }
 public ReportScatterChartConfig MasterReportScatterChartConfig (  ) {
 return this.masterReportScatterChartConfig ;
 }
 public void MasterReportScatterChartConfig (  ReportScatterChartConfig masterReportScatterChartConfig ) {
 this.masterReportScatterChartConfig = masterReportScatterChartConfig ;
 }
 public ReportSlicerConfig MasterReportSlicerConfig (  ) {
 return this.masterReportSlicerConfig ;
 }
 public void MasterReportSlicerConfig (  ReportSlicerConfig masterReportSlicerConfig ) {
 this.masterReportSlicerConfig = masterReportSlicerConfig ;
 }
 public ReportTableConfig MasterReportTableConfig (  ) {
 return this.masterReportTableConfig ;
 }
 public void MasterReportTableConfig (  ReportTableConfig masterReportTableConfig ) {
 this.masterReportTableConfig = masterReportTableConfig ;
 }
 public ReportWaterfallChartConfig MasterReportWaterfallChartConfig (  ) {
 return this.masterReportWaterfallChartConfig ;
 }
 public void MasterReportWaterfallChartConfig (  ReportWaterfallChartConfig masterReportWaterfallChartConfig ) {
 this.masterReportWaterfallChartConfig = masterReportWaterfallChartConfig ;
 }
 public ReportField getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((ReportField)old) ;
 }
 public string DisplayName (  ) {
 return "ReportField" ;
 }
 public bool equals (  Object a ) {
 return a is ReportField && base.Equals(a) ;
 }
 public ReportField DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  ReportField _obj=((ReportField)dbObj);
 _obj.Name(name) ;
 _obj.Field(field) ;
 _obj.Aggregate(aggregate) ;
 }
 public ReportField CloneInstance (  ReportField cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new ReportField() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.Name(this.Name()) ;
 cloneObj.Field(this.Field()) ;
 cloneObj.Aggregate(this.Aggregate()) ;
 return cloneObj ;
 }
 public ReportField CreateNewInstance (  ) {
 return new ReportField() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }