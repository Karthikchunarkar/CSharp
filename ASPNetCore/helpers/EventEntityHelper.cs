namespace helpers ;
 using models; using repository.jpa; using store;  public class EventEntityHelper < T > :  EntityHelper<T> where T : Event { private readonly IEntityMutator mutator ;
 
 private readonly EventRepository eventRepository ;
 
 private readonly IDFileRepository dFileRepository ;
 
 public EventEntityHelper (  IEntityMutator mutator, EventRepository eventRepository, IDFileRepository dFileRepository ) {
  mutator=mutator;
  eventRepository=eventRepository;
  dFileRepository=dFileRepository;
 }
 public T NewInstance (  ) {
 return new Event() ;
 }
 public void ReferenceFromValidations (  T entity, EntityValidationContext validationContext ) {
 }
 public void ValidateFieldEventName (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  string eventName=entity.EventName;
 if ( string.IsNullOrEmpty(eventName) ) {
 validationContext.AddFieldError("eventname","EventName is required.") ;
 }
 }
 public void ValidateFieldStartTime (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  DateTime startTime=entity.StartTime;
 }
 public void ValidateFieldEndTime (  T entity, EntityValidationContext validationContext, bool onCreate, bool onUpdate ) {
  DateTime endTime=entity.EndTime;
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
 return id == 0 ? null : ((T)eventRepository.GetOne(id)) ;
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