namespace helpers ;
 using classes; using helpers; using models; using org.springframework.security.crypto.password; using repository.jpa; using store;  public class AdminEntityHelper < T > :  BaseUserEntityHelper<T> where T : Admin { private readonly PasswordEncoder passwordEncoder ;
 
 private readonly AdminRepository adminRepository ;
 
 private readonly EventRepository eventRepository ;
 
 public AdminEntityHelper (  IEntityMutator mutator, AdminRepository adminRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  adminRepository=adminRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new Admin() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldFullName (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string fullName=entity.FullName;
 if ( string.IsNullOrEmpty(fullName) ) {
 validationContext.AddFieldError("fullname","FullName is required.") ;
 }
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
 base.ValidateInternal(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldFullName(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldEmail(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldPassword(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldRole(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldEmailUnique(entity,validationContext) ;
 }
 public void ValidateOnCreate (  T entity, EntityValidationContext validationContext ) {
 base.ValidateOnCreate(entity,validationContext) ;
 }
 public void ValidateOnUpdate (  T entity, EntityValidationContext validationContext ) {
 base.ValidateOnUpdate(entity,validationContext) ;
 }
 public void ValidateFieldEmailUnique (  T entity, EntityValidationContext validationContext ) {
 if ( !(adminRepository.checkEmailUnique(entity.Id,entity.Email)) ) {
 validationContext.AddFieldError("email","Given email already exists") ;
 }
 }
 public T Clone (  T entity ) {
 return null ;
 }
 public T GetById (  long id ) {
 return id == 0 ? null : ((T)adminRepository.GetOne(id)) ;
 }
 public void SetDefaults (  T entity ) {
 }
 public void Compute (  T entity ) {
 }
 private void DeleteOrganizerInEvent (  T entity, EntityValidationContext deletionContext ) {
 if ( EntityHelper.HaveUnDeleted(eventRepository.GetByOrganizer(entity)) ) {
 deletionContext.AddEntityError("Admin cannot be deleted as it is being referred to by Event.") ;
 }
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 base.OnDelete(entity,internalCall,deletionContext) ;
 return true ;
 }
 public void ValidateOnDelete (  T entity, EntityValidationContext deletionContext ) {
 DeleteOrganizerInEvent(entity,deletionContext) ;
 }
 public bool OnCreate (  T entity, bool internalCall, EntityValidationContext context ) {
 base.OnCreate(entity,internalCall,context) ;
 if ( !string.IsNullOrEmpty(entity.Password) ) {
 entity.Password = passwordEncoder.Encode(entity.Password) ;
 }
 return true ;
 }
 public bool OnUpdate (  T entity, bool internalCall, EntityValidationContext context ) {
 base.OnUpdate(entity,internalCall,context) ;
 if ( entity.Changes.ContainsKey(nameof(Admin.PASSWORD)) ) {
 entity.Password = passwordEncoder.Encode(entity.Password) ;
 }
 return true ;
 }
 public T GetOld (  long id ) {
  T oldEntity=((T)GetById(id));
 return ((T)oldEntity.Clone()) ;
 }
 }