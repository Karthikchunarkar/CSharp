namespace helpers ;
 using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using ID3EEntityManagerProvider = store.ID3EEntityManagerProvider; using IEntityMutator = store.IEntityMutator; using ReportBaseConfig = models.ReportBaseConfig; using ReportRepository = repository.jpa.ReportRepository; using SchemaConstants = d3e.core.SchemaConstants;  public class ReportBaseConfigEntityHelper < T > :  EntityHelper<T> where T : ReportBaseConfig { private readonly IEntityMutator mutator ;
 
 private readonly ID3EEntityManagerProvider provider ;
 
 private readonly ReportRepository reportRepository ;
 
 public ReportBaseConfigEntityHelper (  IEntityMutator mutator, ReportRepository reportRepository ) {
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
 return ((T)provider.get().find(SchemaConstants.ReportBaseConfig,id)) ;
 }
 public void SetDefaults (  T entity ) {
 }
 public void Compute (  T entity ) {
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 return true ;
 }
 public bool OnCreate (  T entity, bool internalCall, EntityValidationContext context ) {
 return true ;
 }
 public bool OnUpdate (  T entity, bool internalCall, EntityValidationContext context ) {
 return true ;
 }
 }