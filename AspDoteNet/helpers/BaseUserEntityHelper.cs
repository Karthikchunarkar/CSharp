namespace helpers ;
 using BaseUser = models.BaseUser; using BaseUserRepository = repository.jpa.BaseUserRepository; using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using IDFileRepository = store.IDFileRepository; using IEntityMutator = store.IEntityMutator; using OneTimePassword = models.OneTimePassword; using OneTimePasswordRepository = repository.jpa.OneTimePasswordRepository; using PushNotification = models.PushNotification; using PushNotificationRepository = repository.jpa.PushNotificationRepository; using UserDevice = models.UserDevice; using UserDeviceRepository = repository.jpa.UserDeviceRepository; using UserLoginRecord = models.UserLoginRecord; using UserLoginRecordRepository = repository.jpa.UserLoginRecordRepository;  public class BaseUserEntityHelper < T > :  EntityHelper<T> where T : BaseUser { private readonly IEntityMutator mutator ;
 
 private readonly BaseUserRepository baseUserRepository ;
 
 private readonly OneTimePasswordRepository oneTimePasswordRepository ;
 
 private readonly PushNotificationRepository pushNotificationRepository ;
 
 private readonly UserDeviceRepository userDeviceRepository ;
 
 private readonly UserLoginRecordRepository userLoginRecordRepository ;
 
 public BaseUserEntityHelper (  IEntityMutator mutator, BaseUserRepository baseUserRepository, OneTimePasswordRepository oneTimePasswordRepository, PushNotificationRepository pushNotificationRepository, UserDeviceRepository userDeviceRepository, UserLoginRecordRepository userLoginRecordRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  baseUserRepository=baseUserRepository;
  oneTimePasswordRepository=oneTimePasswordRepository;
  pushNotificationRepository=pushNotificationRepository;
  userDeviceRepository=userDeviceRepository;
  userLoginRecordRepository=userLoginRecordRepository;
  dFileRepository=dFileRepository;
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
 return id == 0 ? null : ((T)baseUserRepository.GetOne(id)) ;
 }
 public void SetDefaults (  T entity ) {
 }
 public void Compute (  T entity ) {
 }
 private void DeleteUserInOneTimePassword (  T entity, EntityValidationContext deletionContext ) {
 foreach ( OneTimePassword obj in oneTimePasswordRepository.GetByUser(entity) ) {
 mutator.Delete(obj,true) ;
 }
 }
 private void DeleteUsersInPushNotification (  T entity, EntityValidationContext deletionContext ) {
 if ( EntityHelper.HaveUnDeleted(pushNotificationRepository.FindByUsers(entity)) ) {
 deletionContext.AddEntityError("BaseUser cannot be deleted as it is being referred to by PushNotification.") ;
 }
 }
 private void DeleteUserInUserDevice (  T entity, EntityValidationContext deletionContext ) {
 if ( EntityHelper.HaveUnDeleted(userDeviceRepository.GetByUser(entity)) ) {
 deletionContext.AddEntityError("BaseUser cannot be deleted as it is being referred to by UserDevice.") ;
 }
 }
 private void DeleteUserInUserLoginRecord (  T entity, EntityValidationContext deletionContext ) {
 if ( EntityHelper.HaveUnDeleted(userLoginRecordRepository.GetByUser(entity)) ) {
 deletionContext.AddEntityError("BaseUser cannot be deleted as it is being referred to by UserLoginRecord.") ;
 }
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 this.DeleteUserInOneTimePassword(entity,deletionContext) ;
 return true ;
 }
 public void ValidateOnDelete (  T entity, EntityValidationContext deletionContext ) {
 DeleteUsersInPushNotification(entity,deletionContext) ;
 DeleteUserInUserDevice(entity,deletionContext) ;
 DeleteUserInUserLoginRecord(entity,deletionContext) ;
 }
 public void performAction_CreateDevice (  BaseUser entity ) {
 }
 public void PerformOnCreateActions (  BaseUser entity ) {
 PerformOnCreateAndUpdateActions(entity) ;
 }
 public void PerformOnUpdateActions (  BaseUser entity ) {
 PerformOnCreateAndUpdateActions(entity) ;
 }
 public void PerformOnCreateAndUpdateActions (  BaseUser entity ) {
 performAction_CreateDevice(entity) ;
 }
 public bool OnCreate (  T entity, bool internalCall, EntityValidationContext context ) {
 PerformOnCreateActions(entity) ;
 return true ;
 }
 public bool OnUpdate (  T entity, bool internalCall, EntityValidationContext context ) {
 PerformOnUpdateActions(entity) ;
 return true ;
 }
 public T GetOld (  long id ) {
  T oldEntity=((T)GetById(id));
 return ((T)oldEntity.Clone()) ;
 }
 }