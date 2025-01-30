namespace helpers ;
 using models; using repository.jpa; using store;  public class D3EImageEntityHelper < T > :  EntityHelper<T> where T : D3EImage { private readonly IEntityMutator mutator ;
 
 private readonly AvatarRepository avatarRepository ;
 
 private readonly IDFileRepository dFileRepository ;
 
 public D3EImageEntityHelper (  IEntityMutator mutator, AvatarRepository avatarRepository ) {
  mutator=mutator;
  avatarRepository=avatarRepository;
 }
 public T NewInstance (  ) {
 return new D3EImage() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldSize (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  int size=entity.Size;
 }
 public void ValidateFieldWidth (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  int width=entity.Width;
 }
 public void ValidateFieldHeight (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  int height=entity.Height;
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 ValidateFieldSize(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldWidth(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldHeight(entity,validationContext,onCreate,onUpdate) ;
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
 }