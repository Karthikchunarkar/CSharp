namespace helpers ;
 using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using ID3EEntityManagerProvider = store.ID3EEntityManagerProvider; using IEntityMutator = store.IEntityMutator; using ReportCell = models.ReportCell; using ReportCellStyleEntityHelper = helpers.ReportCellStyleEntityHelper; using ReportRepository = repository.jpa.ReportRepository; using SchemaConstants = d3e.core.SchemaConstants;  public class ReportCellEntityHelper < T > :  EntityHelper<T> where T : ReportCell { private readonly IEntityMutator mutator ;
 
 private readonly ID3EEntityManagerProvider provider ;
 
 private readonly ReportRepository reportRepository ;
 
 public ReportCellEntityHelper (  IEntityMutator mutator, ReportRepository reportRepository ) {
  mutator=mutator;
  reportRepository=reportRepository;
 }
 public T NewInstance (  ) {
 return new ReportCell() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 if ( entity.Style != null ) {
  ReportCellStyleEntityHelper helper=mutator.GetHelperByInstance(entity.Style);
 if ( onCreate ) {
 helper.ValidateOnCreate(entity.Style,validationContext.Child("style",null,-1)) ;
 }
 else {
 helper.ValidateOnUpdate(entity.Style,validationContext.Child("style",null,-1)) ;
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
 return ((T)provider.get().find(SchemaConstants.ReportCell,id)) ;
 }
 public void SetDefaults (  T entity ) {
 if ( entity.Style != null ) {
  ReportCellStyleEntityHelper helper=mutator.GetHelperByInstance(entity.Style);
 helper.SetDefaults(entity.Style) ;
 }
 }
 public void Compute (  T entity ) {
 if ( entity.Style != null ) {
  ReportCellStyleEntityHelper helper=mutator.GetHelperByInstance(entity.Style);
 helper.compute(entity.Style) ;
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