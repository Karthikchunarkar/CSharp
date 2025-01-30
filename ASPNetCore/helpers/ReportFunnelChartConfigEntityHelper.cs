namespace helpers ;
 using d3e.core; using helpers; using models; using store;  public class ReportFunnelChartConfigEntityHelper < T > :  ReportBaseConfigEntityHelper<T> where T : ReportFunnelChartConfig { public ReportFunnelChartConfigEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ReportFunnelChartConfig() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 base.ValidateInternal(entity,validationContext,onCreate,onUpdate) ;
  long categoryFieldsIndex=0;
 foreach ( ReportField obj in entity.CategoryFields ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("categoryFields",null,categoryFieldsIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("categoryFields",null,categoryFieldsIndex++)) ;
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
 return ((T)provider.get().find(SchemaConstants.ReportFunnelChartConfig,id)) ;
 }
 public void SetDefaults (  T entity ) {
 foreach ( ReportField obj in entity.CategoryFields ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.Values ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.Tooltips ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 }
 public void Compute (  T entity ) {
 foreach ( ReportField obj in entity.CategoryFields ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.Values ) {
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