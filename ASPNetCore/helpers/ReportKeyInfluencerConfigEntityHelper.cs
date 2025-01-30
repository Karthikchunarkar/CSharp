namespace helpers ;
 using d3e.core; using helpers; using models; using store;  public class ReportKeyInfluencerConfigEntityHelper < T > :  ReportBaseConfigEntityHelper<T> where T : ReportKeyInfluencerConfig { public ReportKeyInfluencerConfigEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ReportKeyInfluencerConfig() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 base.ValidateInternal(entity,validationContext,onCreate,onUpdate) ;
 if ( entity.Analyze != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Analyze);
 if ( onCreate ) {
 helper.ValidateOnCreate(entity.Analyze,validationContext.Child("analyze",null,-1)) ;
 }
 else {
 helper.ValidateOnUpdate(entity.Analyze,validationContext.Child("analyze",null,-1)) ;
 }
 }
  long eexplainByIndex=0;
 foreach ( ReportField obj in entity.EexplainBy ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("eexplainBy",null,eexplainByIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("eexplainBy",null,eexplainByIndex++)) ;
 }
 }
  long expandByIndex=0;
 foreach ( ReportField obj in entity.ExpandBy ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("expandBy",null,expandByIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("expandBy",null,expandByIndex++)) ;
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
 return ((T)provider.get().find(SchemaConstants.ReportKeyInfluencerConfig,id)) ;
 }
 public void SetDefaults (  T entity ) {
 if ( entity.Analyze != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Analyze);
 helper.SetDefaults(entity.Analyze) ;
 }
 foreach ( ReportField obj in entity.EexplainBy ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.ExpandBy ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 }
 public void Compute (  T entity ) {
 if ( entity.Analyze != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Analyze);
 helper.compute(entity.Analyze) ;
 }
 foreach ( ReportField obj in entity.EexplainBy ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.ExpandBy ) {
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