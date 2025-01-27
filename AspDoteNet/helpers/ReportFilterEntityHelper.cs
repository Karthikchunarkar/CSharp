namespace helpers ;
 using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using ID3EEntityManagerProvider = store.ID3EEntityManagerProvider; using IEntityMutator = store.IEntityMutator; using ReportFilter = models.ReportFilter; using ReportRepository = repository.jpa.ReportRepository; using SchemaConstants = d3e.core.SchemaConstants;  public class ReportFilterEntityHelper < T > :  EntityHelper<T> where T : ReportFilter { private readonly IEntityMutator mutator ;
 
 private readonly ID3EEntityManagerProvider provider ;
 
 private readonly ReportRepository reportRepository ;
 
 public ReportFilterEntityHelper (  IEntityMutator mutator, ReportRepository reportRepository ) {
  mutator=mutator;
  reportRepository=reportRepository;
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
 return ((T)provider.get().find(SchemaConstants.ReportFilter,id)) ;
 }
 public void SetDefaults (  T entity ) {
 }
 public void Compute (  T entity ) {
 }
 private void DeleteFilterInReportSingleRule (  T entity, EntityValidationContext deletionContext ) {
 // TODO: ReportSingleRule is a document model. Need to figure out how to check ReportSingleRule by filter for this method.
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 return true ;
 }
 public void ValidateOnDelete (  T entity, EntityValidationContext deletionContext ) {
 DeleteFilterInReportSingleRule(entity,deletionContext) ;
 }
 public bool OnCreate (  T entity, bool internalCall, EntityValidationContext context ) {
 return true ;
 }
 public bool OnUpdate (  T entity, bool internalCall, EntityValidationContext context ) {
 return true ;
 }
 }