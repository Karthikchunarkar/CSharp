namespace helpers ;
 using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using IDFileRepository = store.IDFileRepository; using IEntityMutator = store.IEntityMutator; using ReportConfig = models.ReportConfig; using ReportConfigOption = models.ReportConfigOption; using ReportConfigOptionEntityHelper = helpers.ReportConfigOptionEntityHelper; using ReportConfigRepository = repository.jpa.ReportConfigRepository; using string = System.string;  public class ReportConfigEntityHelper < T > :  EntityHelper<T> where T : ReportConfig { private readonly IEntityMutator mutator ;
 
 private readonly ReportConfigRepository reportConfigRepository ;
 
 public ReportConfigEntityHelper (  IEntityMutator mutator, ReportConfigRepository reportConfigRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  reportConfigRepository=reportConfigRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new ReportConfig() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldIdentity (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string identity=entity.Identity;
 if ( string.IsNullOrEmpty(identity) ) {
 validationContext.AddFieldError("identity","Identity is required.") ;
 }
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 ValidateFieldIdentity(entity,validationContext,onCreate,onUpdate) ;
  long valuesIndex=0;
 foreach ( ReportConfigOption obj in entity.Values ) {
  ReportConfigOptionEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("values",obj.(),valuesIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("values",obj.(),valuesIndex++)) ;
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
 return id == 0 ? null : ((T)reportConfigRepository.GetOne(id)) ;
 }
 public void SetDefaults (  T entity ) {
 foreach ( ReportConfigOption obj in entity.Values ) {
  ReportConfigOptionEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 }
 public void Compute (  T entity ) {
 foreach ( ReportConfigOption obj in entity.Values ) {
  ReportConfigOptionEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
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