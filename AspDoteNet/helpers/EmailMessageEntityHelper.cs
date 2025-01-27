namespace helpers ;
 using EmailMessage = models.EmailMessage; using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using IDFileRepository = store.IDFileRepository; using IEntityMutator = store.IEntityMutator;  public class EmailMessageEntityHelper < T > :  EntityHelper<T> where T : EmailMessage { private readonly IEntityMutator mutator ;
 
 private readonly IDFileRepository dFileRepository ;
 
 public EmailMessageEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new EmailMessage() ;
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
 public void setDefaultCreatedOn (  T entity ) {
 if ( entity.CreatedOn != null ) {
 return ;
 }
 entity.CreatedOn(null) ;
 }
 public T Clone (  T entity ) {
 return null ;
 }
 public T GetById (  long id ) {
 return null ;
 }
 public void SetDefaults (  T entity ) {
 this.setDefaultCreatedOn(entity) ;
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