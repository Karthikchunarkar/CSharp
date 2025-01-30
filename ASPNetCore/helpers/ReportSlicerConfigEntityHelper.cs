namespace helpers ;
 using d3e.core; using helpers; using models; using store;  public class ReportSlicerConfigEntityHelper < T > :  ReportBaseConfigEntityHelper<T> where T : ReportSlicerConfig { public ReportSlicerConfigEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ReportSlicerConfig() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 base.ValidateInternal(entity,validationContext,onCreate,onUpdate) ;
  long fieldsIndex=0;
 foreach ( ReportField obj in entity.Fields ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("fields",null,fieldsIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("fields",null,fieldsIndex++)) ;
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
 return ((T)provider.get().find(SchemaConstants.ReportSlicerConfig,id)) ;
 }
 public void SetDefaults (  T entity ) {
 foreach ( ReportField obj in entity.Fields ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 }
 public void Compute (  T entity ) {
 foreach ( ReportField obj in entity.Fields ) {
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