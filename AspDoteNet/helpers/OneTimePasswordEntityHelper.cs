namespace helpers ;
 using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using IDFileRepository = store.IDFileRepository; using IEntityMutator = store.IEntityMutator; using OneTimePassword = models.OneTimePassword; using OneTimePasswordRepository = repository.jpa.OneTimePasswordRepository; using bool = System.bool; using string = System.string;  public class OneTimePasswordEntityHelper < T > :  EntityHelper<T> where T : OneTimePassword { private readonly IEntityMutator mutator ;
 
 private readonly OneTimePasswordRepository oneTimePasswordRepository ;
 
 public OneTimePasswordEntityHelper (  IEntityMutator mutator, OneTimePasswordRepository oneTimePasswordRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  oneTimePasswordRepository=oneTimePasswordRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new OneTimePassword() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldInput (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string input=entity.Input;
 if ( string.IsNullOrEmpty(input) ) {
 validationContext.AddFieldError("input","Input is required.") ;
 }
 }
 public void ValidateFieldInputType (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string inputType=entity.InputType;
 if ( string.IsNullOrEmpty(inputType) ) {
 validationContext.AddFieldError("inputtype","InputType is required.") ;
 }
 }
 public void ValidateFieldUserType (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string userType=entity.UserType;
 if ( string.IsNullOrEmpty(userType) ) {
 validationContext.AddFieldError("usertype","UserType is required.") ;
 }
 }
 public void ValidateFieldSuccess (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  bool isSuccess=entity.isSuccess;
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 ValidateFieldInput(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldInputType(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldUserType(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldSuccess(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldTokenUnique(entity,validationContext) ;
 isErrorMsgExists(entity) ;
 }
 public void ValidateOnCreate (  T entity, EntityValidationContext validationContext ) {
 ValidateInternal(entity,validationContext,true,false) ;
 }
 public void ValidateOnUpdate (  T entity, EntityValidationContext validationContext ) {
 ValidateInternal(entity,validationContext,false,true) ;
 }
 public bool isErrorMsgExists (  T entity ) {
 try {
 if ( null ) {
 return true ;
 }
 else {
 entity.ErrorMsg = "" ;
 return false ;
 }
 }
 catch ( Exception e ) {
 return false ;
 }
 }
 public void ValidateFieldTokenUnique (  T entity, EntityValidationContext validationContext ) {
 if ( !(oneTimePasswordRepository.checkTokenUnique(entity.Id,entity.Token)) ) {
 validationContext.AddFieldError("token","Given token already exists") ;
 }
 }
 public T Clone (  T entity ) {
 return null ;
 }
 public T GetById (  long id ) {
 return id == 0 ? null : ((T)oneTimePasswordRepository.GetOne(id)) ;
 }
 public void SetDefaults (  T entity ) {
 }
 public void Compute (  T entity ) {
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 return true ;
 }
 public void performAction_Create (  OneTimePassword entity ) {
 }
 public void PerformOnCreateActions (  OneTimePassword entity ) {
 performAction_Create(entity) ;
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