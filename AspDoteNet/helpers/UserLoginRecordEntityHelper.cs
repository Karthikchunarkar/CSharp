namespace helpers ;
 using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using IDFileRepository = store.IDFileRepository; using IEntityMutator = store.IEntityMutator; using UserLoginRecord = models.UserLoginRecord; using UserLoginRecordRepository = repository.jpa.UserLoginRecordRepository;  public class UserLoginRecordEntityHelper < T > :  EntityHelper<T> where T : UserLoginRecord { private readonly IEntityMutator mutator ;
 
 private readonly UserLoginRecordRepository userLoginRecordRepository ;
 
 public UserLoginRecordEntityHelper (  IEntityMutator mutator, UserLoginRecordRepository userLoginRecordRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  userLoginRecordRepository=userLoginRecordRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new UserLoginRecord() ;
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
 return id == 0 ? null : ((T)userLoginRecordRepository.GetOne(id)) ;
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