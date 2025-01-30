namespace helpers ;
 using models; using store;  public class BulkEventEntityHelper < T > :  EntityHelper<T> where T : BulkEvent { private readonly IEntityMutator mutator ;
 
 private readonly IDFileRepository dFileRepository ;
 
 public BulkEventEntityHelper (  IEntityMutator mutator ) {
  mutator=mutator;
 }
 public T NewInstance (  ) {
 return new BulkEvent() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldEventName (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string eventName=entity.EventName;
 if ( string.IsNullOrEmpty(eventName) ) {
 validationContext.AddFieldError("eventname","EventName is required.") ;
 }
 }
 public void ValidateFieldEventDate (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  DateTime eventDate=entity.EventDate;
 }
 public void ValidateFieldStartTime (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  TimeSpan startTime=entity.StartTime;
 }
 public void ValidateFieldEndTime (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  TimeSpan endTime=entity.EndTime;
 }
 public void ValidateFieldVenue (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string venue=entity.Venue;
 if ( string.IsNullOrEmpty(venue) ) {
 validationContext.AddFieldError("venue","Venue is required.") ;
 }
 }
 public void ValidateFieldCategory (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  Category category=entity.Category;
 if ( category == null ) {
 validationContext.AddFieldError("category","Category is required.") ;
 }
 }
 public void ValidateFieldOrganizer (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  Admin organizer=entity.Organizer;
 if ( organizer == null ) {
 validationContext.AddFieldError("organizer","Organizer is required.") ;
 }
 }
 public void ValidateInternal (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
 ValidateFieldEventName(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldEventDate(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldStartTime(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldEndTime(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldVenue(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldCategory(entity,validationContext,onCreate,onUpdate) ;
 ValidateFieldOrganizer(entity,validationContext,onCreate,onUpdate) ;
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
 public void PerformAction_ScheduleEvent (  BulkEvent entity ) {
 }
 public void PerformOnCreateActions (  BulkEvent entity ) {
 PerformAction_ScheduleEvent(entity) ;
 }
 public bool OnCreate (  T entity, bool internalCall, EntityValidationContext context ) {
 PerformOnCreateActions(entity) ;
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