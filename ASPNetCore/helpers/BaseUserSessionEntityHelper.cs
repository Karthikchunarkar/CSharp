namespace helpers ;
 using models; using repository.jpa; using store;  public class BaseUserSessionEntityHelper < T > :  EntityHelper<T> where T : BaseUserSession { private readonly IEntityMutator mutator ;
 
 private readonly BaseUserSessionRepository baseUserSessionRepository ;
 
 public BaseUserSessionEntityHelper (  IEntityMutator mutator, BaseUserSessionRepository baseUserSessionRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  baseUserSessionRepository=baseUserSessionRepository;
  dFileRepository=dFileRepository;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldUserSessionId (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string userSessionId=entity.UserSessionId;
 if ( string.IsNullOrEmpty(userSessionId) ) {
 validationContext.AddFieldError("usersessionid","UserSessionId is required.") ;
 }
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 ValidateFieldUserSessionId(entity,validationContext,onCreate,onUpdate) ;
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
 return id == 0 ? null : ((T)baseUserSessionRepository.GetOne(id)) ;
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