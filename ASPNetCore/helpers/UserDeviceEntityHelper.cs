namespace helpers ;
 using models; using repository.jpa; using store;  public class UserDeviceEntityHelper < T > :  EntityHelper<T> where T : UserDevice { private readonly IEntityMutator mutator ;
 
 private readonly UserDeviceRepository userDeviceRepository ;
 
 private readonly BaseUserRepository baseUserRepository ;
 
 private readonly PushNotificationRepository pushNotificationRepository ;
 
 public UserDeviceEntityHelper (  IEntityMutator mutator, UserDeviceRepository userDeviceRepository, BaseUserRepository baseUserRepository, PushNotificationRepository pushNotificationRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  userDeviceRepository=userDeviceRepository;
  baseUserRepository=baseUserRepository;
  pushNotificationRepository=pushNotificationRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new UserDevice() ;
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
 return id == 0 ? null : ((T)userDeviceRepository.GetOne(id)) ;
 }
 public void SetDefaults (  T entity ) {
 }
 public void Compute (  T entity ) {
 }
 private void DeleteFailedDevicesInPushNotification (  T entity, EntityValidationContext deletionContext ) {
 if ( EntityHelper.HaveUnDeleted(pushNotificationRepository.FindByFailedDevices(entity)) ) {
 deletionContext.AddEntityError("UserDevice cannot be deleted as it is being referred to by PushNotification.") ;
 }
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 if ( entity.User != null ) {
 entity.User.SetDevices(null) ;
 }
 return true ;
 }
 public void ValidateOnDelete (  T entity, EntityValidationContext deletionContext ) {
 DeleteFailedDevicesInPushNotification(entity,deletionContext) ;
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