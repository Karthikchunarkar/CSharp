namespace helpers ;
 using helpers; using models; using store;  public class ReportDataEntityHelper < T > :  EntityHelper<T> where T : ReportData { private readonly IEntityMutator mutator ;
 
 public ReportDataEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ReportData() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  long sectionsIndex=0;
 foreach ( ReportDataSection obj in entity.Sections ) {
  ReportDataSectionEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("sections",null,sectionsIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("sections",null,sectionsIndex++)) ;
 }
 }
  long rowsIndex=0;
 foreach ( ReportDataRow obj in entity.Rows ) {
  ReportDataRowEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("rows",null,rowsIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("rows",null,rowsIndex++)) ;
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
 return null ;
 }
 public void SetDefaults (  T entity ) {
 foreach ( ReportDataSection obj in entity.Sections ) {
  ReportDataSectionEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportDataRow obj in entity.Rows ) {
  ReportDataRowEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 }
 public void Compute (  T entity ) {
 foreach ( ReportDataSection obj in entity.Sections ) {
  ReportDataSectionEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportDataRow obj in entity.Rows ) {
  ReportDataRowEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
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