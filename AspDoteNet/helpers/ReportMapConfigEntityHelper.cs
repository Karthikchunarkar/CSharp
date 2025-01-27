namespace helpers ;
 using EntityValidationContext = store.EntityValidationContext; using IEntityMutator = store.IEntityMutator; using ReportBaseConfigEntityHelper = helpers.ReportBaseConfigEntityHelper; using ReportField = models.ReportField; using ReportFieldEntityHelper = helpers.ReportFieldEntityHelper; using ReportMapConfig = models.ReportMapConfig; using ReportMapType = classes.ReportMapType; using SchemaConstants = d3e.core.SchemaConstants;  public class ReportMapConfigEntityHelper < T > :  ReportBaseConfigEntityHelper<T> where T : ReportMapConfig { public ReportMapConfigEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new ReportMapConfig() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldType (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  ReportMapType type=entity.Type;
 if ( type == null ) {
 validationContext.AddFieldError("type","Type is required.") ;
 }
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 base.ValidateInternal(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldType(entity,validationContext,onCreate,onUpdate) ;
  long locationIndex=0;
 foreach ( ReportField obj in entity.Location ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("location",null,locationIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("location",null,locationIndex++)) ;
 }
 }
  long legendIndex=0;
 foreach ( ReportField obj in entity.Legend ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 if ( onCreate ) {
 helper.ValidateOnCreate(obj,validationContext.Child("legend",null,legendIndex++)) ;
 }
 else {
 helper.ValidateOnUpdate(obj,validationContext.Child("legend",null,legendIndex++)) ;
 }
 }
 if ( entity.Latitude != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Latitude);
 if ( onCreate ) {
 helper.ValidateOnCreate(entity.Latitude,validationContext.Child("latitude",null,-1)) ;
 }
 else {
 helper.ValidateOnUpdate(entity.Latitude,validationContext.Child("latitude",null,-1)) ;
 }
 }
 if ( entity.Longitude != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Longitude);
 if ( onCreate ) {
 helper.ValidateOnCreate(entity.Longitude,validationContext.Child("longitude",null,-1)) ;
 }
 else {
 helper.ValidateOnUpdate(entity.Longitude,validationContext.Child("longitude",null,-1)) ;
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
 return ((T)provider.get().find(SchemaConstants.ReportMapConfig,id)) ;
 }
 public void SetDefaults (  T entity ) {
 foreach ( ReportField obj in entity.Location ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.Legend ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 if ( entity.Latitude != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Latitude);
 helper.SetDefaults(entity.Latitude) ;
 }
 if ( entity.Longitude != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Longitude);
 helper.SetDefaults(entity.Longitude) ;
 }
 foreach ( ReportField obj in entity.Size ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 foreach ( ReportField obj in entity.Tooltips ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.SetDefaults(obj) ;
 }
 }
 public void Compute (  T entity ) {
 foreach ( ReportField obj in entity.Location ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 foreach ( ReportField obj in entity.Legend ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(obj);
 helper.compute(obj) ;
 }
 if ( entity.Latitude != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Latitude);
 helper.compute(entity.Latitude) ;
 }
 if ( entity.Longitude != null ) {
  ReportFieldEntityHelper helper=mutator.GetHelperByInstance(entity.Longitude);
 helper.compute(entity.Longitude) ;
 }
 foreach ( ReportField obj in entity.Size ) {
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