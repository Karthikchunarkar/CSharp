namespace helpers ;
 using d3e.core; using helpers; using models; using store;  public class ReportScatterChartConfigEntityHelper < T > :  ReportBaseConfigEntityHelper<T> where T : ReportScatterChartConfig { public ReportScatterChartConfigEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ReportScatterChartConfig() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 base.ValidateInternal(entity,validationContext,onCreate,onUpdate) ;
  long valuesIndex=0;
 foreach ( ReportField obj in entity.Values ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("values",null,valuesIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("values",null,valuesIndex++)) ;
 }
 }
  long xAxesIndex=0;
 foreach ( ReportField obj in entity.XAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("xAxes",null,xAxesIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("xAxes",null,xAxesIndex++)) ;
 }
 }
  long yAxesIndex=0;
 foreach ( ReportField obj in entity.YAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("yAxes",null,yAxesIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("yAxes",null,yAxesIndex++)) ;
 }
 }
  long sizeIndex=0;
 foreach ( ReportField obj in entity.Size ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("size",null,sizeIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("size",null,sizeIndex++)) ;
 }
 }
  long legendsIndex=0;
 foreach ( ReportField obj in entity.Legends ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("legends",null,legendsIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("legends",null,legendsIndex++)) ;
 }
 }
  long playAxisIndex=0;
 foreach ( ReportField obj in entity.PlayAxis ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("playAxis",null,playAxisIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("playAxis",null,playAxisIndex++)) ;
 }
 }
  long tooltipsIndex=0;
 foreach ( ReportField obj in entity.Tooltips ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("tooltips",null,tooltipsIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("tooltips",null,tooltipsIndex++)) ;
 }
 }
 }
 public void ValidateOnCreate (  T entity, EntityValidationContext validationContext ) {
 base.ValidateOnCreate(entity,validationContext) ;
 }
 public void ValidateOnUpdate (  T entity, EntityValidationContext validationContext ) {
 base.ValidateOnUpdate(entity,validationContext) ;
 }
 public T Clone (  T entity ) {
 return null ;
 }
 public T GetById (  long id ) {
 return ((T)provider.get().find(SchemaConstants.ReportScatterChartConfig,id)) ;
 }
 public void SetDefaults (  T entity ) {
 foreach ( ReportField obj in entity.Values ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.XAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.YAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.Size ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.Legends ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.PlayAxis ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.Tooltips ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 }
 public void Compute (  T entity ) {
 foreach ( ReportField obj in entity.Values ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.XAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.YAxes ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.Size ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.Legends ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.PlayAxis ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.Tooltips ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 }
 public bool OnDelete (  T entity, bool internalCall, EntityValidationContext deletionContext ) {
 base.OnDelete(entity,internalCall,deletionContext) ;
 return true ;
 }
 public bool OnCreate (  T entity, bool internalCall, EntityValidationContext context ) {
 base.OnCreate(entity,internalCall,context) ;
 return true ;
 }
 public bool OnUpdate (  T entity, bool internalCall, EntityValidationContext context ) {
 base.OnUpdate(entity,internalCall,context) ;
 return true ;
 }
 }