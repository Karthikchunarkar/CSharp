namespace helpers ;
 using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using IDFileRepository = store.IDFileRepository; using IEntityMutator = store.IEntityMutator; using VerificationData = models.VerificationData; using VerificationDataRepository = repository.jpa.VerificationDataRepository; using string = System.string;  public class VerificationDataEntityHelper < T > :  EntityHelper<T> where T : VerificationData { private readonly IEntityMutator mutator ;
 
 private readonly VerificationDataRepository verificationDataRepository ;
 
 public VerificationDataEntityHelper (  IEntityMutator mutator, VerificationDataRepository verificationDataRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  verificationDataRepository=verificationDataRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new VerificationData() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldMethod (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string method=entity.Method;
 if ( string.IsNullOrEmpty(method) ) {
 validationContext.AddFieldError("method","Method is required.") ;
 }
 }
 public void ValidateFieldContext (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string context=entity.Context;
 if ( string.IsNullOrEmpty(context) ) {
 validationContext.AddFieldError("context","Context is required.") ;
 }
 }
 public void ValidateFieldBody (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string body=entity.Body;
 if ( string.IsNullOrEmpty(body) ) {
 validationContext.AddFieldError("body","Body is required.") ;
 }
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 ValidateFieldMethod(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldContext(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldBody(entity,validationContext,onCreate,onUpdate) ;
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
 return id == 0 ? null : ((T)verificationDataRepository.GetOne(id)) ;
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
 public T GetOld (  long id ) {
  T oldEntity=((T)GetById(id));
 return ((T)oldEntity.Clone()) ;
 }
 }