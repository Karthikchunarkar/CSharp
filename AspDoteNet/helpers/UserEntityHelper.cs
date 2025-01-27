namespace helpers ;
 using BaseUserEntityHelper = helpers.BaseUserEntityHelper; using DateTime = System.DateTime; using EntityValidationContext = store.EntityValidationContext; using IDFileRepository = store.IDFileRepository; using IEntityMutator = store.IEntityMutator; using PasswordEncoder = org.springframework.security.crypto.password.PasswordEncoder; using User = models.User; using UserRepository = repository.jpa.UserRepository; using UserRole = classes.UserRole; using string = System.string;  public class UserEntityHelper < T > :  BaseUserEntityHelper<T> where T : User { private readonly PasswordEncoder passwordEncoder ;
 
 private readonly UserRepository userRepository ;
 
 private readonly IDFileRepository dFileRepository ;
 
 public UserEntityHelper (  IEntityMutator mutator, UserRepository userRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  userRepository=userRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new User() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldFirstName (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string firstName=entity.FirstName;
 if ( string.IsNullOrEmpty(firstName) ) {
 validationContext.AddFieldError("firstname","FirstName is required.") ;
 }
 }
 public void ValidateFieldLastName (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string lastName=entity.LastName;
 if ( string.IsNullOrEmpty(lastName) ) {
 validationContext.AddFieldError("lastname","LastName is required.") ;
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
 ValidateFieldFirstName(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldLastName(entity,validationContext,onCreate,onUpdate) ;
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
 if ( !(userRepository.checkEmailUnique(entity.Id,entity.Email)) ) {
 validationContext.AddFieldError("email","Given email already exists") ;
 }
 }
 public T Clone (  T entity ) {
 return null ;
 }
 public T GetById (  long id ) {
 return id == 0 ? null : ((T)userRepository.GetOne(id)) ;
 }
 public void SetDefaults (  T entity ) {
 }
 public void Compute (  T entity ) {
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 base.OnDelete(entity,internalCall,deletionContext) ;
 return true ;
 }
 public bool OnCreate (  T entity, bool internalCall, EntityValidationContext context ) {
 base.OnCreate(entity,internalCall,context) ;
 entity.CreatedDate = DateTime.Now ;
 if ( !string.IsNullOrEmpty(entity.Password) ) {
 entity.Password = passwordEncoder.Encode(entity.Password) ;
 }
 return true ;
 }
 public bool OnUpdate (  T entity, bool internalCall, EntityValidationContext context ) {
 base.OnUpdate(entity,internalCall,context) ;
 if ( entity.Changes.ContainsKey(nameof(User.PASSWORD)) ) {
 entity.Password = passwordEncoder.Encode(entity.Password) ;
 }
 entity.UpdatedDate = DateTime.Now ;
 return true ;
 }
 public T GetOld (  long id ) {
  T oldEntity=((T)GetById(id));
 return ((T)oldEntity.Clone()) ;
 }
 }