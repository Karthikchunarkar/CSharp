namespace helpers ;
 using models; using store;  public class ChangePasswordRequestEntityHelper < T > :  EntityHelper<T> where T : ChangePasswordRequest { private readonly IEntityMutator mutator ;
 
 public ChangePasswordRequestEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ChangePasswordRequest() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldNewPassword (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string newPassword=entity.NewPassword;
 if ( string.IsNullOrEmpty(newPassword) ) {
 validationContext.AddFieldError("newpassword","NewPassword is required.") ;
 }
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 ValidateFieldNewPassword(entity,validationContext,onCreate,onUpdate) ;
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
 public void PerformAction_SetNewPassword (  ChangePasswordRequest entity ) {
 }
 public void PerformOnCreateActions (  ChangePasswordRequest entity ) {
 PerformAction_SetNewPassword(entity) ;
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