namespace helpers ;
 using classes; using models; using repository.jpa; using store;  public class SignUpRequestEntityHelper < T > :  EntityHelper<T> where T : SignUpRequest { private readonly IEntityMutator mutator ;
 
 private readonly SignUpRequestRepository signUpRequestRepository ;
 
 public SignUpRequestEntityHelper (  IEntityMutator mutator, SignUpRequestRepository signUpRequestRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  signUpRequestRepository=signUpRequestRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new SignUpRequest() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldEmail (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string email=entity.Email;
 if ( string.IsNullOrEmpty(email) ) {
 validationContext.AddFieldError("email","Email is required.") ;
 }
 }
 public void ValidateFieldPassword (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string password=entity.Password;
 if ( string.IsNullOrEmpty(password) ) {
 validationContext.AddFieldError("password","Password is required.") ;
 }
 }
 public void ValidateFieldRole (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  UserRole role=entity.Role;
 if ( role == null ) {
 validationContext.AddFieldError("role","Role is required.") ;
 }
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 ValidateFieldEmail(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldPassword(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldRole(entity,validationContext,onCreate,onUpdate) ;
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
 return id == 0 ? null : ((T)signUpRequestRepository.GetOne(id)) ;
 }
 public void SetDefaults (  T entity ) {
 }
 public void Compute (  T entity ) {
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 return true ;
 }
 public void PerformAction_OnCreate (  SignUpRequest entity ) {
 }
 public void PerformOnCreateActions (  SignUpRequest entity ) {
 PerformAction_OnCreate(entity) ;
 }
 public bool OnCreate (  T entity, bool internalCall, EntityValidationContext context ) {
 PerformOnCreateActions(entity) ;
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