namespace helpers ;
 using classes; using d3e.core; using helpers; using models; using store;  public class ReportBarChartConfigEntityHelper < T > :  ReportBaseConfigEntityHelper<T> where T : ReportBarChartConfig { public ReportBarChartConfigEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ReportBarChartConfig() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldType (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  ReportBarChartType type=entity.Type;
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
 if ( entity.Legend != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Legend);
 if ( onCreate ) {
 helper.ValidateOnCreate(entity.Legend,validationContext.Child("legend",null,-1)) ;
 }
 else {
 helper.ValidateOnUpdate(entity.Legend,validationContext.Child("legend",null,-1)) ;
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
 return ((T)provider.get().find(SchemaConstants.ReportBarChartConfig,id)) ;
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
 if ( entity.Legend != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Legend);
 helper.SetDefaults(entity.Legend) ;
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
 if ( entity.Legend != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Legend);
 helper.compute(entity.Legend) ;
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