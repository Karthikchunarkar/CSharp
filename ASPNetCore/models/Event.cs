namespace models ;
 using classes; using d3e.core; using java.util; using java.util.function; using store;  public class Event :  CreatableObject { public static int REFERENCEID = 0 ;
 
 public static int EVENTNAME = 1 ;
 
 public static int STARTTIME = 2 ;
 
 public static int ENDTIME = 3 ;
 
 public static int VENUE = 4 ;
 
 public static int DESCRIPTION = 5 ;
 
 public static int IMAGES = 6 ;
 
 public static int STATUS = 7 ;
 
 public static int CATEGORY = 8 ;
 
 public static int ORGANIZER = 9 ;
 
 public static int SCHEDULETYPE = 10 ;
 
 public static int REPEATSPAN = 11 ;
 
 private string referenceID { get; set; } 
 private string eventName { get; set; } 
 private DateTime startTime { get; set; } 
 private DateTime endTime { get; set; } 
 private string venue { get; set; } 
 private string description { get; set; } 
 private List<DFile> images { get; set; } = D3EPersistanceList.primitive(IMAGES) ;
 
 private EventStatus status { get; set; } = EventStatus.Approved ;
 
 private Category category { get; set; } 
 private Admin organizer { get; set; } 
 private EventScheduleType scheduleType { get; set; } = EventScheduleType.NoRepeat ;
 
 private int repeatSpan { get; set; } = 0 ;
 
 private Event Old { get; set; } 
 public Event (  ) {
 }
 public int TypeIdx (  ) {
 return SchemaConstants.Event ;
 }
 public string Type (  ) {
 return "Event" ;
 }
 public int FieldsCount (  ) {
 return 12 ;
 }
 public void AddToImages (  DFile val, long index ) {
 if ( index == -1 ) {
 images.Add(val) ;
 }
 else {
 images.Add(((int)index),val) ;
 }
 }
 public void RemoveFromImages (  DFile val ) {
 images.Remove(val) ;
 }
 public void UpdateMasters (  Consumer < DatabaseObject > visitor ) {
 base.UpdateMasters(visitor) ;
 }
 public void VisitChildren (  Consumer < DBObject > visitor ) {
 base.VisitChildren(visitor) ;
 }
 public string ReferenceID (  ) {
 _CheckProxy() ;
 return this.referenceID ;
 }
 public void ReferenceID (  string referenceID ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.referenceID,referenceID) ) {
 return ;
 }
 fieldChanged(REFERENCEID,this.referenceID,referenceID) ;
 this.referenceID = referenceID ;
 }
 public string EventName (  ) {
 _CheckProxy() ;
 return this.eventName ;
 }
 public void EventName (  string eventName ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.eventName,eventName) ) {
 return ;
 }
 fieldChanged(EVENTNAME,this.eventName,eventName) ;
 this.eventName = eventName ;
 }
 public DateTime StartTime (  ) {
 _CheckProxy() ;
 return this.startTime ;
 }
 public void StartTime (  DateTime startTime ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.startTime,startTime) ) {
 return ;
 }
 fieldChanged(STARTTIME,this.startTime,startTime) ;
 this.startTime = startTime ;
 }
 public DateTime EndTime (  ) {
 _CheckProxy() ;
 return this.endTime ;
 }
 public void EndTime (  DateTime endTime ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.endTime,endTime) ) {
 return ;
 }
 fieldChanged(ENDTIME,this.endTime,endTime) ;
 this.endTime = endTime ;
 }
 public string Venue (  ) {
 _CheckProxy() ;
 return this.venue ;
 }
 public void Venue (  string venue ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.venue,venue) ) {
 return ;
 }
 fieldChanged(VENUE,this.venue,venue) ;
 this.venue = venue ;
 }
 public string Description (  ) {
 _CheckProxy() ;
 return this.description ;
 }
 public void Description (  string description ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.description,description) ) {
 return ;
 }
 fieldChanged(DESCRIPTION,this.description,description) ;
 this.description = description ;
 }
 public List<DFile> Images (  ) {
 return this.images ;
 }
 public void Images (  List<DFile> images ) {
 if ( Objects.Equals(this.images,images) ) {
 return ;
 }
 ((D3EPersistanceList < DFile >)this.images).SetAll(images) ;
 }
 public EventStatus Status (  ) {
 _CheckProxy() ;
 return this.status ;
 }
 public void Status (  EventStatus status ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.status,status) ) {
 return ;
 }
 fieldChanged(STATUS,this.status,status) ;
 this.status = status ;
 }
 public Category Category (  ) {
 _CheckProxy() ;
 return this.category ;
 }
 public void Category (  Category category ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.category,category) ) {
 return ;
 }
 fieldChanged(CATEGORY,this.category,category) ;
 this.category = category ;
 }
 public Admin Organizer (  ) {
 _CheckProxy() ;
 return this.organizer ;
 }
 public void Organizer (  Admin organizer ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.organizer,organizer) ) {
 return ;
 }
 fieldChanged(ORGANIZER,this.organizer,organizer) ;
 this.organizer = organizer ;
 }
 public EventScheduleType ScheduleType (  ) {
 _CheckProxy() ;
 return this.scheduleType ;
 }
 public void ScheduleType (  EventScheduleType scheduleType ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.scheduleType,scheduleType) ) {
 return ;
 }
 fieldChanged(SCHEDULETYPE,this.scheduleType,scheduleType) ;
 this.scheduleType = scheduleType ;
 }
 public int RepeatSpan (  ) {
 _CheckProxy() ;
 return this.repeatSpan ;
 }
 public void RepeatSpan (  int repeatSpan ) {
 _CheckProxy() ;
 if ( Objects.Equals(this.repeatSpan,repeatSpan) ) {
 return ;
 }
 fieldChanged(REPEATSPAN,this.repeatSpan,repeatSpan) ;
 this.repeatSpan = repeatSpan ;
 }
 public Event getOld (  ) {
 return this.Old ;
 }
 public void setOld (  DatabaseObject old ) {
 this.Old = ((Event)old) ;
 }
 public string DisplayName (  ) {
 return "Event" ;
 }
 public bool equals (  Object a ) {
 return a is Event && base.Equals(a) ;
 }
 public Event DeepClone (  bool clearId ) {
  CloneContext ctx=new CloneContext(clearId);
 return ctx.StartClone(this) ;
 }
 public void DeepCloneIntoObj (  ICloneable dbObj, CloneContext ctx ) {
 base.DeepCloneIntoObj(dbObj,ctx) ;
  Event _obj=((Event)dbObj);
 _obj.ReferenceID(referenceID) ;
 _obj.EventName(eventName) ;
 _obj.StartTime(startTime) ;
 _obj.EndTime(endTime) ;
 _obj.Venue(venue) ;
 _obj.Description(description) ;
 _obj.Images(images) ;
 _obj.Status(status) ;
 _obj.Category(category) ;
 _obj.Organizer(organizer) ;
 _obj.ScheduleType(scheduleType) ;
 _obj.RepeatSpan(repeatSpan) ;
 }
 public Event CloneInstance (  Event cloneObj ) {
 if ( cloneObj == null ) {
 cloneObj = new Event() ;
 }
 base.CloneInstance(cloneObj) ;
 cloneObj.ReferenceID(this.ReferenceID()) ;
 cloneObj.EventName(this.EventName()) ;
 cloneObj.StartTime(this.StartTime()) ;
 cloneObj.EndTime(this.EndTime()) ;
 cloneObj.Venue(this.Venue()) ;
 cloneObj.Description(this.Description()) ;
 cloneObj.Images(new ArrayList<>(Images())) ;
 cloneObj.Status(this.Status()) ;
 cloneObj.Category(this.Category()) ;
 cloneObj.Organizer(this.Organizer()) ;
 cloneObj.ScheduleType(this.ScheduleType()) ;
 cloneObj.RepeatSpan(this.RepeatSpan()) ;
 return cloneObj ;
 }
 public Event CreateNewInstance (  ) {
 return new Event() ;
 }
 public bool NeedOldObject (  ) {
 return true ;
 }
 public void CollectCreatableReferences (  java.util.List < Object > _refs ) {
 base.CollectCreatableReferences(_refs) ;
 _refs.AddAll(this.images) ;
 _refs.Add(this.category) ;
 _refs.Add(this.organizer) ;
 }
 public bool IsEntity (  ) {
 return true ;
 }
 }