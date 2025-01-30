namespace helpers ;
 using d3e.core; using models; using store;  public class ReportRuleEntityHelper < T > :  EntityHelper<T> where T : ReportRule { private readonly IEntityMutator mutator ;
 
 private readonly ID3EEntityManagerProvider provider ;
 
 public ReportRuleEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
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
 return ((T)provider.get().find(SchemaConstants.ReportRule,id)) ;
 }
 public void SetDefaults (  T entity ) {
 }
 public void Compute (  T entity ) {
 }
 private void DeleteParentInReportRule (  T entity, EntityValidationContext deletionContext ) {
 // TODO: ReportRule is a document model. Need to figure out how to check ReportRule by parent for this method.
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 return true ;
 }
 public void ValidateOnDelete (  T entity, EntityValidationContext deletionContext ) {
 DeleteParentInReportRule(entity,deletionContext) ;
 }
 public bool OnCreate (  T entity, bool internalCall, EntityValidationContext context ) {
 return true ;
 }
 public bool OnUpdate (  T entity, bool internalCall, EntityValidationContext context ) {
 return true ;
 }
 }