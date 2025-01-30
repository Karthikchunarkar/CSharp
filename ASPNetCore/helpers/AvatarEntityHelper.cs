namespace helpers ;
 using helpers; using models; using repository.jpa; using store;  public class AvatarEntityHelper < T > :  EntityHelper<T> where T : Avatar { private readonly IEntityMutator mutator ;
 
 private readonly AvatarRepository avatarRepository ;
 
 public AvatarEntityHelper (  IEntityMutator mutator, AvatarRepository avatarRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  avatarRepository=avatarRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new Avatar() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 if ( entity.Image != null ) {
  D3EImageEntityHelper helper=mutator.GetHelperByInstance(entity.Image);
 if ( onCreate ) {
 helper.ValidateOnCreate(entity.Image,validationContext.Child("image",null,-1)) ;
 }
 else {
 helper.ValidateOnUpdate(entity.Image,validationContext.Child("image",null,-1)) ;
 }
 }
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
 return id == 0 ? null : ((T)avatarRepository.GetOne(id)) ;
 }
 public void SetDefaults (  T entity ) {
 if ( entity.Image != null ) {
  D3EImageEntityHelper helper=mutator.GetHelperByInstance(entity.Image);
 helper.SetDefaults(entity.Image) ;
 }
 }
 public void Compute (  T entity ) {
 if ( entity.Image != null ) {
  D3EImageEntityHelper helper=mutator.GetHelperByInstance(entity.Image);
 helper.compute(entity.Image) ;
 }
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
 }