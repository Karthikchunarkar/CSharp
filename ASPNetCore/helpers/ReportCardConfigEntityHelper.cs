namespace helpers ;
 using d3e.core; using helpers; using models; using store;  public class ReportCardConfigEntityHelper < T > :  ReportBaseConfigEntityHelper<T> where T : ReportCardConfig { public ReportCardConfigEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ReportCardConfig() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 base.ValidateInternal(entity,validationContext,onCreate,onUpdate) ;
 if ( entity.Value != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Value);
 if ( onCreate ) {
 helper.ValidateOnCreate(entity.Value,validationContext.Child("value",null,-1)) ;
 }
 else {
 helper.ValidateOnUpdate(entity.Value,validationContext.Child("value",null,-1)) ;
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
 return ((T)provider.get().find(SchemaConstants.ReportCardConfig,id)) ;
 }
 public void SetDefaults (  T entity ) {
 if ( entity.Value != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Value);
 helper.SetDefaults(entity.Value) ;
 }
 }
 public void Compute (  T entity ) {
 if ( entity.Value != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Value);
 helper.compute(entity.Value) ;
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