namespace helpers ;
 using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using IEntityMutator = store.IEntityMutator; using UserDevicesRequest = models.UserDevicesRequest;  public class UserDevicesRequestEntityHelper < T > :  EntityHelper<T> where T : UserDevicesRequest { private readonly IEntityMutator mutator ;
 
 public UserDevicesRequestEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new UserDevicesRequest() ;
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
 return null ;
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