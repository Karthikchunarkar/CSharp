namespace helpers ;
 using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using IDFileRepository = store.IDFileRepository; using IEntityMutator = store.IEntityMutator; using Notification = models.Notification; using NotificationRepository = repository.jpa.NotificationRepository; using ResourceData = models.ResourceData; using ResourceDataRepository = repository.jpa.ResourceDataRepository;  public class ResourceDataEntityHelper < T > :  EntityHelper<T> where T : ResourceData { private readonly IEntityMutator mutator ;
 
 private readonly ResourceDataRepository resourceDataRepository ;
 
 private readonly NotificationRepository notificationRepository ;
 
 public ResourceDataEntityHelper (  IEntityMutator mutator, ResourceDataRepository resourceDataRepository, NotificationRepository notificationRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  resourceDataRepository=resourceDataRepository;
  notificationRepository=notificationRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new ResourceData() ;
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
 return id == 0 ? null : ((T)resourceDataRepository.GetOne(id)) ;
 }
 public void SetDefaults (  T entity ) {
 }
 public void Compute (  T entity ) {
 }
 private void DeleteResourceDataInNotification (  T entity, EntityValidationContext deletionContext ) {
 if ( EntityHelper.HaveUnDeleted(notificationRepository.GetByResourceData(entity)) ) {
 deletionContext.AddEntityError("ResourceData cannot be deleted as it is being referred to by Notification.") ;
 }
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 return true ;
 }
 public void ValidateOnDelete (  T entity, EntityValidationContext deletionContext ) {
 DeleteResourceDataInNotification(entity,deletionContext) ;
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