namespace helpers ;
 using d3e.core; using helpers; using models; using store;  public class ReportNamedConditionEntityHelper < T > :  EntityHelper<T> where T : ReportNamedCondition { private readonly IEntityMutator mutator ;
 
 private readonly ID3EEntityManagerProvider provider ;
 
 public ReportNamedConditionEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ReportNamedCondition() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 if ( entity.Condition != null ) {
  ReportRuleEntityHelper helper=mutator.GetHelperByInstance(entity.Condition);
 if ( onCreate ) {
 helper.ValidateOnCreate(entity.Condition,validationContext.Child("condition",null,-1)) ;
 }
 else {
 helper.ValidateOnUpdate(entity.Condition,validationContext.Child("condition",null,-1)) ;
 }
 }
 }
 public void ValidateOnCreate (  T entity, EntityValidationContext validationContext ) {
 ValidateInternal(entity,validationContext,true,false) ;
 }
 public void ValidateOnUpdate (  T entity, EntityValidationContext validationContext ) {
 ValidateInternal(entity,validationContext,false,true) ;
 }
 public T Clone (  T entity ) {
 return null ;
 }
 public T GetById (  long id ) {
 return ((T)provider.get().find(SchemaConstants.ReportNamedCondition,id)) ;
 }
 public void SetDefaults (  T entity ) {
 if ( entity.Condition != null ) {
  ReportRuleEntityHelper helper=mutator.GetHelperByInstance(entity.Condition);
 helper.SetDefaults(entity.Condition) ;
 }
 }
 public void Compute (  T entity ) {
 if ( entity.Condition != null ) {
  ReportRuleEntityHelper helper=mutator.GetHelperByInstance(entity.Condition);
 helper.compute(entity.Condition) ;
 }
 }
 private void DeleteConditionsInReportNamedConditionFilter (  T entity, EntityValidationContext deletionContext ) {
 // TODO: ReportNamedConditionFilter is a document model. Need to figure out how to check ReportNamedConditionFilter by conditions for this method.
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 return true ;
 }
 public void ValidateOnDelete (  T entity, EntityValidationContext deletionContext ) {
 DeleteConditionsInReportNamedConditionFilter(entity,deletionContext) ;
 }
 public bool OnCreate (  T entity, bool internalCall, EntityValidationContext context ) {
 return true ;
 }
 public bool OnUpdate (  T entity, bool internalCall, EntityValidationContext context ) {
 return true ;
 }
 }