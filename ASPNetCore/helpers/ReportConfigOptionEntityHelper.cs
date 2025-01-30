namespace helpers ;
 using models; using repository.jpa; using store;  public class ReportConfigOptionEntityHelper < T > :  EntityHelper<T> where T : ReportConfigOption { private readonly IEntityMutator mutator ;
 
 private readonly ReportConfigOptionRepository reportConfigOptionRepository ;
 
 private readonly ReportConfigRepository reportConfigRepository ;
 
 public ReportConfigOptionEntityHelper (  IEntityMutator mutator, ReportConfigOptionRepository reportConfigOptionRepository, ReportConfigRepository reportConfigRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  reportConfigOptionRepository=reportConfigOptionRepository;
  reportConfigRepository=reportConfigRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new ReportConfigOption() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldIdentity (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string identity=entity.Identity;
 if ( string.IsNullOrEmpty(identity) ) {
 validationContext.AddFieldError("identity","Identity is required.") ;
 }
 }
 public void ValidateFieldValue (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string value=entity.Value;
 if ( string.IsNullOrEmpty(value) ) {
 validationContext.AddFieldError("value","Value is required.") ;
 }
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 ValidateFieldIdentity(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldValue(entity,validationContext,onCreate,onUpdate) ;
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
 return id == 0 ? null : ((T)reportConfigOptionRepository.GetOne(id)) ;
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