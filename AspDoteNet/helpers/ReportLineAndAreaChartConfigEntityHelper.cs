namespace helpers ;
 using EntityValidationContext = store.EntityValidationContext; using IEntityMutator = store.IEntityMutator; using ReportBaseConfigEntityHelper = helpers.ReportBaseConfigEntityHelper; using ReportField = models.ReportField; using ReportFieldEntityHelper = helpers.ReportFieldEntityHelper; using ReportLineAndAreaChartConfig = models.ReportLineAndAreaChartConfig; using ReportLineAndAreaChartType = classes.ReportLineAndAreaChartType; using SchemaConstants = d3e.core.SchemaConstants;  public class ReportLineAndAreaChartConfigEntityHelper < T > :  ReportBaseConfigEntityHelper<T> where T : ReportLineAndAreaChartConfig { public ReportLineAndAreaChartConfigEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ReportLineAndAreaChartConfig() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldType (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  ReportLineAndAreaChartType type=entity.Type;
 if ( type == null ) {
 validationContext.AddFieldError("type","Type is required.") ;
 }
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 base.ValidateInternal(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldType(entity,validationContext,onCreate,onUpdate) ;
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
  long yAxesIndex=0;
 foreach ( ReportField obj in entity.YAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("yAxes",null,yAxesIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("yAxes",null,yAxesIndex++)) ;
 }
 }
  long secondaryYAxesIndex=0;
 foreach ( ReportField obj in entity.SecondaryYAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("secondaryYAxes",null,secondaryYAxesIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("secondaryYAxes",null,secondaryYAxesIndex++)) ;
 }
 }
  long legendIndex=0;
 foreach ( ReportField obj in entity.Legend ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("legend",null,legendIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("legend",null,legendIndex++)) ;
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
 return ((T)provider.get().find(SchemaConstants.ReportLineAndAreaChartConfig,id)) ;
 }
 public void SetDefaults (  T entity ) {
 foreach ( ReportField obj in entity.XAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.YAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.SecondaryYAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.Legend ) {
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
 foreach ( ReportField obj in entity.YAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.SecondaryYAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.Legend ) {
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