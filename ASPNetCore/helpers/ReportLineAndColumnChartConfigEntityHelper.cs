namespace helpers ;
 using d3e.core; using helpers; using models; using store;  public class ReportLineAndColumnChartConfigEntityHelper < T > :  ReportBaseConfigEntityHelper<T> where T : ReportLineAndColumnChartConfig { public ReportLineAndColumnChartConfigEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ReportLineAndColumnChartConfig() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 base.ValidateInternal(entity,validationContext,onCreate,onUpdate) ;
  long xAxesIndex=0;
 foreach ( ReportField obj in entity.XAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("xAxes",null,xAxesIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("xAxes",null,xAxesIndex++)) ;
 }
 }
  long columnYAxesIndex=0;
 foreach ( ReportField obj in entity.ColumnYAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("columnYAxes",null,columnYAxesIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("columnYAxes",null,columnYAxesIndex++)) ;
 }
 }
  long lineYAxesIndex=0;
 foreach ( ReportField obj in entity.LineYAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("lineYAxes",null,lineYAxesIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("lineYAxes",null,lineYAxesIndex++)) ;
 }
 }
  long columnLegendIndex=0;
 foreach ( ReportField obj in entity.ColumnLegend ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("columnLegend",null,columnLegendIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("columnLegend",null,columnLegendIndex++)) ;
 }
 }
  long smallMultiplesIndex=0;
 foreach ( ReportField obj in entity.SmallMultiples ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("smallMultiples",null,smallMultiplesIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("smallMultiples",null,smallMultiplesIndex++)) ;
 }
 }
  long tooltipsIndex=0;
 foreach ( ReportField obj in entity.Tooltips ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("tooltips",null,tooltipsIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("tooltips",null,tooltipsIndex++)) ;
 }
 }
 }
 public void ValidateOnCreate (  T entity, EntityValidationContext validationContext ) {
 base.ValidateOnCreate(entity,validationContext) ;
 }
 public void ValidateOnUpdate (  T entity, EntityValidationContext validationContext ) {
 base.ValidateOnUpdate(entity,validationContext) ;
 }
 public T Clone (  T entity ) {
 return null ;
 }
 public T GetById (  long id ) {
 return ((T)provider.get().find(SchemaConstants.ReportLineAndColumnChartConfig,id)) ;
 }
 public void SetDefaults (  T entity ) {
 foreach ( ReportField obj in entity.XAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.ColumnYAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.LineYAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.ColumnLegend ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.SmallMultiples ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.Tooltips ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 }
 public void Compute (  T entity ) {
 foreach ( ReportField obj in entity.XAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.ColumnYAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.LineYAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.ColumnLegend ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.SmallMultiples ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.Tooltips ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 base.OnDelete(entity,internalCall,deletionContext) ;
 return true ;
 }
 public bool OnCreate (  T entity, bool internalCall, EntityValidationContext context ) {
 base.OnCreate(entity,internalCall,context) ;
 return true ;
 }
 public bool OnUpdate (  T entity, bool internalCall, EntityValidationContext context ) {
 base.OnUpdate(entity,internalCall,context) ;
 return true ;
 }
 }