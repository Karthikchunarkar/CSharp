namespace helpers ;
 using classes; using d3e.core; using helpers; using models; using store;  public class ReportPieChartConfigEntityHelper < T > :  ReportBaseConfigEntityHelper<T> where T : ReportPieChartConfig { public ReportPieChartConfigEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ReportPieChartConfig() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldType (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  ReportPieChartType type=entity.Type;
 if ( type == null ) {
 validationContext.AddFieldError("type","Type is required.") ;
 }
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 base.ValidateInternal(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldType(entity,validationContext,onCreate,onUpdate) ;
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
  long valuesIndex=0;
 foreach ( ReportField obj in entity.Values ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("values",null,valuesIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("values",null,valuesIndex++)) ;
 }
 }
  long detailsIndex=0;
 foreach ( ReportField obj in entity.Details ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("details",null,detailsIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("details",null,detailsIndex++)) ;
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
 return ((T)provider.get().find(SchemaConstants.ReportPieChartConfig,id)) ;
 }
 public void SetDefaults (  T entity ) {
 foreach ( ReportField obj in entity.Legend ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.Values ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.Details ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.Tooltips ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 }
 public void Compute (  T entity ) {
 foreach ( ReportField obj in entity.Legend ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.Values ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.Details ) {
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