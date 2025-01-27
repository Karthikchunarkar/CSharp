namespace helpers ;
 using EntityHelper = store.EntityHelper; using EntityValidationContext = store.EntityValidationContext; using IDFileRepository = store.IDFileRepository; using IEntityMutator = store.IEntityMutator; using Report = models.Report; using ReportBaseConfigEntityHelper = helpers.ReportBaseConfigEntityHelper; using ReportCellEntityHelper = helpers.ReportCellEntityHelper; using ReportFilter = models.ReportFilter; using ReportFilterEntityHelper = helpers.ReportFilterEntityHelper; using ReportRepository = repository.jpa.ReportRepository; using ReportRuleSetEntityHelper = helpers.ReportRuleSetEntityHelper; using string = System.string;  public class ReportEntityHelper < T > :  EntityHelper<T> where T : Report { private readonly IEntityMutator mutator ;
 
 private readonly ReportRepository reportRepository ;
 
 public ReportEntityHelper (  IEntityMutator mutator, ReportRepository reportRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  reportRepository=reportRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new Report() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldModel (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string model=entity.Model;
 if ( string.IsNullOrEmpty(model) ) {
 validationContext.AddFieldError("model","Model is required.") ;
 }
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 ValidateFieldModel(entity,validationContext,onCreate,onUpdate) ;
 if ( entity.Cells != null ) {
  ReportCellEntityHelper helper=mutator.GetHelperByInstance(entity.Cells);
 if ( onCreate ) {
 helper.ValidateOnCreate(entity.Cells,validationContext.Child("cells",null,-1)) ;
 }
 else {
 helper.ValidateOnUpdate(entity.Cells,validationContext.Child("cells",null,-1)) ;
 }
 }
 if ( entity.Config != null ) {
  ReportBaseConfigEntityHelper helper=mutator.GetHelperByInstance(entity.Config);
 if ( onCreate ) {
 helper.ValidateOnCreate(entity.Config,validationContext.Child("config",null,-1)) ;
 }
 else {
 helper.ValidateOnUpdate(entity.Config,validationContext.Child("config",null,-1)) ;
 }
 }
  long filtersIndex=0;
 foreach ( ReportFilter obj in entity.Filters ) {
  ReportFilterEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("filters",null,filtersIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("filters",null,filtersIndex++)) ;
 }
 }
 if ( entity.Criteria != null ) {
  ReportRuleSetEntityHelper helper=mutator.GetHelperByInstance(entity.Criteria);
 if ( onCreate ) {
 helper.ValidateOnCreate(entity.Criteria,validationContext.Child("criteria",null,-1)) ;
 }
 else {
 helper.ValidateOnUpdate(entity.Criteria,validationContext.Child("criteria",null,-1)) ;
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
 return id == 0 ? null : ((T)reportRepository.GetOne(id)) ;
 }
 public void SetDefaults (  T entity ) {
 if ( entity.Cells != null ) {
  ReportCellEntityHelper helper=mutator.GetHelperByInstance(entity.Cells);
 helper.SetDefaults(entity.Cells) ;
 }
 if ( entity.Config != null ) {
  ReportBaseConfigEntityHelper helper=mutator.GetHelperByInstance(entity.Config);
 helper.SetDefaults(entity.Config) ;
 }
 foreach ( ReportFilter obj in entity.Filters ) {
  ReportFilterEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 if ( entity.Criteria != null ) {
  ReportRuleSetEntityHelper helper=mutator.GetHelperByInstance(entity.Criteria);
 helper.SetDefaults(entity.Criteria) ;
 }
 }
 public void Compute (  T entity ) {
 if ( entity.Cells != null ) {
  ReportCellEntityHelper helper=mutator.GetHelperByInstance(entity.Cells);
 helper.compute(entity.Cells) ;
 }
 if ( entity.Config != null ) {
  ReportBaseConfigEntityHelper helper=mutator.GetHelperByInstance(entity.Config);
 helper.compute(entity.Config) ;
 }
 foreach ( ReportFilter obj in entity.Filters ) {
  ReportFilterEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 if ( entity.Criteria != null ) {
  ReportRuleSetEntityHelper helper=mutator.GetHelperByInstance(entity.Criteria);
 helper.compute(entity.Criteria) ;
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
 public T GetOld (  long id ) {
  T oldEntity=((T)GetById(id));
 return ((T)oldEntity.Clone()) ;
 }
 }