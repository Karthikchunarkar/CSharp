namespace classes ;
 using d3e.core; using store;  public class CalendarEvent :  DBObject { public static int _DATE = 0 ;
 
 public static int _TITLE = 1 ;
 
 public static int _CATEGORY = 2 ;
 
 public static int _ALLDAY = 3 ;
 
 public static int _STARTTIME = 4 ;
 
 public static int _ENDTIME = 5 ;
 
 public static int _DESCRIPTION = 6 ;
 
 public static int _BACKGROUNDCOLOR = 7 ;
 
 private long id 
 private DateTime date 
 private string title 
 private string category 
 private bool allDay 
 private DateTime startTime 
 private DateTime endTime 
 private string description 
 private string backgroundColor 
 public CalendarEvent (  ) {
 }
 public CalendarEvent (  bool allDay, string backgroundColor, string category, DateTime date, string description, DateTime endTime, DateTime startTime, string title ) {
 this.AllDay = allDay ;
 this.BackgroundColor = backgroundColor ;
 this.Category = category ;
 this.Date = date ;
 this.Description = description ;
 this.EndTime = endTime ;
 this.StartTime = startTime ;
 this.Title = title ;
 }
 public long getId (  ) {
 return id ;
 }
 public void setId (  long id ) {
 this.id = id ;
 }
 public DateTime getDate (  ) {
 return date ;
 }
 public void setDate (  DateTime date ) {
 fieldChanged(_DATE,this.date,date) ;
 this.date = date ;
 }
 public string getTitle (  ) {
 return title ;
 }
 public void setTitle (  string title ) {
 fieldChanged(_TITLE,this.title,title) ;
 this.title = title ;
 }
 public string getCategory (  ) {
 return category ;
 }
 public void setCategory (  string category ) {
 fieldChanged(_CATEGORY,this.category,category) ;
 this.category = category ;
 }
 public bool getAllDay (  ) {
 return allDay ;
 }
 public void setAllDay (  bool allDay ) {
 fieldChanged(_ALLDAY,this.allDay,allDay) ;
 this.allDay = allDay ;
 }
 public DateTime getStartTime (  ) {
 return startTime ;
 }
 public void setStartTime (  DateTime startTime ) {
 fieldChanged(_STARTTIME,this.startTime,startTime) ;
 this.startTime = startTime ;
 }
 public DateTime getEndTime (  ) {
 return endTime ;
 }
 public void setEndTime (  DateTime endTime ) {
 fieldChanged(_ENDTIME,this.endTime,endTime) ;
 this.endTime = endTime ;
 }
 public string getDescription (  ) {
 return description ;
 }
 public void setDescription (  string description ) {
 fieldChanged(_DESCRIPTION,this.description,description) ;
 this.description = description ;
 }
 public string getBackgroundColor (  ) {
 return backgroundColor ;
 }
 public void setBackgroundColor (  string backgroundColor ) {
 fieldChanged(_BACKGROUNDCOLOR,this.backgroundColor,backgroundColor) ;
 this.backgroundColor = backgroundColor ;
 }
 public int _typeIdx (  ) {
 return SchemaConstants.CalendarEvent ;
 }
 public String _type (  ) {
 return "CalendarEvent" ;
 }
 public int _fieldsCount (  ) {
 return 8 ;
 }
 public void _convertToObjectRef (  ) {
 }
 }