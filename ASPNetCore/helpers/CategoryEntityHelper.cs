namespace helpers ;
 using models; using repository.jpa; using store;  public class CategoryEntityHelper < T > :  EntityHelper<T> where T : Category { private readonly IEntityMutator mutator ;
 
 private readonly CategoryRepository categoryRepository ;
 
 private readonly EventRepository eventRepository ;
 
 public CategoryEntityHelper (  IEntityMutator mutator, CategoryRepository categoryRepository, EventRepository eventRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  categoryRepository=categoryRepository;
  eventRepository=eventRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new Category() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldName (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string name=entity.Name;
 if ( string.IsNullOrEmpty(name) ) {
 validationContext.AddFieldError("name","Name is required.") ;
 }
 }
 public void ValidateFieldColor (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string color=entity.Color;
 if ( string.IsNullOrEmpty(color) ) {
 validationContext.AddFieldError("color","Color is required.") ;
 }
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 ValidateFieldName(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldColor(entity,validationContext,onCreate,onUpdate) ;
 }
 public void ValidateOnCreate (  T entity, EntityValidationContext validationContext ) {
 ValidateInternal(entity,validationContext,true,false) ;
 }
 public void ValidateOnUpdate (  T entity, EntityValidationContext validationContext ) {
 ValidateInternal(entity,validationContext,false,true) ;
 }
 public void SetDefaultColor (  T entity ) {
 if ( entity.Color != null && !(entity.Color.IsEmpty()) ) {
 return ;
 }
 entity.Color("ffe91e63") ;
 }
 public T Clone (  T entity ) {
 return null ;
 }
 public T GetById (  long id ) {
 return id == 0 ? null : ((T)categoryRepository.GetOne(id)) ;
 }
 public void SetDefaults (  T entity ) {
 this.SetDefaultColor(entity) ;
 }
 public void Compute (  T entity ) {
 }
 private void DeleteCategoryInEvent (  T entity, EntityValidationContext deletionContext ) {
 if ( EntityHelper.HaveUnDeleted(eventRepository.GetByCategory(entity)) ) {
 deletionContext.AddEntityError("Category cannot be deleted as it is being referred to by Event.") ;
 }
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 return true ;
 }
 public void ValidateOnDelete (  T entity, EntityValidationContext deletionContext ) {
 DeleteCategoryInEvent(entity,deletionContext) ;
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