namespace helpers ;
 using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using IDFileRepository = store.IDFileRepository; using IEntityMutator = store.IEntityMutator; using MicroSoftAcc = models.MicroSoftAcc; using MicroSoftAccRepository = repository.jpa.MicroSoftAccRepository;  public class MicroSoftAccEntityHelper < T > :  EntityHelper<T> where T : MicroSoftAcc { private readonly IEntityMutator mutator ;
 
 private readonly MicroSoftAccRepository microSoftAccRepository ;
 
 public MicroSoftAccEntityHelper (  IEntityMutator mutator, MicroSoftAccRepository microSoftAccRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  microSoftAccRepository=microSoftAccRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new MicroSoftAcc() ;
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
 return id == 0 ? null : ((T)microSoftAccRepository.GetOne(id)) ;
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
 public T GetOld (  long id ) {
  T oldEntity=((T)GetById(id));
 return ((T)oldEntity.Clone()) ;
 }
 }