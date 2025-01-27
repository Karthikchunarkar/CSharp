namespace helpers ;
 using DefaultUser = models.DefaultUser; using DefaultUserRepository = repository.jpa.DefaultUserRepository; using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using IDFileRepository = store.IDFileRepository; using IEntityMutator = store.IEntityMutator;  public class DefaultUserEntityHelper < T > :  EntityHelper<T> where T : DefaultUser { private readonly IEntityMutator mutator ;
 
 private readonly DefaultUserRepository defaultUserRepository ;
 
 public DefaultUserEntityHelper (  IEntityMutator mutator, DefaultUserRepository defaultUserRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  defaultUserRepository=defaultUserRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new DefaultUser() ;
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
 return id == 0 ? null : ((T)defaultUserRepository.GetOne(id)) ;
 }
 public void SetDefaults (  T entity ) {
 }
 public void Compute (  T entity ) {
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 return true ;
 }
 public void performAction_CreateAdmin (  DefaultUser entity ) {
 }
 public void PerformOnCreateActions (  DefaultUser entity ) {
 performAction_CreateAdmin(entity) ;
 }
 public bool OnCreate (  T entity, bool internalCall, EntityValidationContext context ) {
 DefaultUserSingletonHelper.get().Set(entity) ;
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