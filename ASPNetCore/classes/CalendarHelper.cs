namespace classes ;
 using classes;  public class CalendarHelper { public List<CalendarEvent> Events { get; set; } 
 public CalendarHelper (  List<CalendarEvent> events ) {
 this.Events = events ;
 }
 public List<CalendarEvent> getEventsForDate (  DateTime date, List<string> categories ) {
 if ( categories.getIsEmpty() ) {
 return this.events.where((  eventValue ) => {
 return eventValue.getDate().getYear() == date.getYear() && eventValue.getDate().getMonth() == date.getMonth() && eventValue.getDate().getDay() == date.getDay() ;
 }
).toList(false) ;
 }
 else {
 return this.events.where((  eventValue ) => {
 return eventValue.getDate().getYear() == date.getYear() && eventValue.getDate().getMonth() == date.getMonth() && eventValue.getDate().getDay() == date.getDay() && categories.contains(eventValue.getCategory()) ;
 }
).toList(false) ;
 }
 }
 public List<CalendarEvent> getEventsForMonth (  DateTime date, List<string> categories ) {
 if ( categories.getIsEmpty() ) {
 return this.events.where((  eventValue ) => {
 return eventValue.getDate().getYear() == date.getYear() && eventValue.getDate().getMonth() == date.getMonth() ;
 }
).toList(false) ;
 }
 else {
 return this.events.where((  eventValue ) => {
 return eventValue.getDate().getYear() == date.getYear() && eventValue.getDate().getMonth() == date.getMonth() && categories.contains(eventValue.getCategory()) ;
 }
).toList(false) ;
 }
 }
 public static List<int> to (  int n ) {
  List<int> res=null;
 for (  int x=1;
 x <= n ; null ) {
 res.add(x) ;
 }
 return res ;
 }
 public List<CalendarEvent> getAllDayEventsForDate (  DateTime date, List<string> categories ) {
 if ( categories.getIsEmpty() ) {
 return this.events.where((  eventValue ) => {
 return eventValue.getDate().getYear() == date.getYear() && eventValue.getDate().getMonth() == date.getMonth() && eventValue.getDate().getDay() == date.getDay() && eventValue.isAllDay() ;
 }
).toList(false) ;
 }
 else {
 return this.events.where((  eventValue ) => {
 return eventValue.getDate().getYear() == date.getYear() && eventValue.getDate().getMonth() == date.getMonth() && eventValue.getDate().getDay() == date.getDay() && eventValue.isAllDay() && categories.contains(eventValue.getCategory()) ;
 }
).toList(false) ;
 }
 }
 public static string formatHour (  int hour ) {
 if ( hour == 0 ) {
 return "12AM" ;
 }
 else if ( hour < 12 ) {
 return hour.toString() + "AM" ;
 }
 else if ( hour == 12 ) {
 return "12PM" ;
 }
 else {
 return toString() + "PM" ;
 }
 }
 public List<CalendarEvent> sortEvents (  List<CalendarEvent> events ) {
  List<CalendarEvent> allDayEvents=events.where((  eventValue ) => {
 return ;
 }
).toList(false);
 allDayEvents.sort((  a, b ) => {
 return a.getStartTime().compareTo(b.getStartTime()) ;
 }
) ;
 return allDayEvents ;
 }
 public Double getEventHeight (  CalendarEvent eventValue ) {
  Double height=eventValue.getEndTime().difference(eventValue.getStartTime()).getInMinutes().toDouble();
 //  if 60 minutes equals 100 px height, then find out height of above
 height = height * 100 / 60 ;
 if ( height < 25.0d ) {
 height = 25.0d ;
 }
 return height ;
 }
 public Double getEventTop (  CalendarEvent eventValue ) {
  Double top=eventValue.getStartTime().getMinute().toDouble();
 top = top * 100 / 60 ;
 return top ;
 }
 public static string getWeekDay (  DateTime date ) {
  int dayOfWeek=date.getDayOfWeek();
  string weekday=null;
 switch ( ((int)dayOfWeek) ) { case ((int)0): {
 weekday = "Sunday" ;
 break; }
 case ((int)1): {
 weekday = "Monday" ;
 break; }
 case ((int)2): {
 weekday = "Tuesday" ;
 break; }
 case ((int)3): {
 weekday = "Wednesday" ;
 break; }
 case ((int)4): {
 weekday = "Thursday" ;
 break; }
 case ((int)5): {
 weekday = "Friday" ;
 break; }
 case ((int)6): {
 weekday = "Saturday" ;
 break; }
 default: {
 }
 } return weekday ;
 }
 public static List<DateTime> getWeekDayDates (  DateTime dateTime ) {
  List<DateTime> weekDayDates=null;
 //  Calculate the start of the current week (Monday)
 //  dateTime.weekday gives 1 for Monday, 2 for Tuesday, ... 7 for Sunday
 //  To find the previous Monday, we subtract (dateTime.weekday - 1) days
  DateTime monday=dateTime.subtract(new TimeSpan(dateTime.getWeekday() - 1,0,0,0,0,0));
 //  Loop to add the dates of the week
 for (  int i=0;
 i < 7 ; null ) {
 weekDayDates.add(monday.add(new TimeSpan(i,0,0,0,0,0))) ;
 }
 return weekDayDates ;
 }
 public List<CalendarEvent> getEvents (  List<string> categories ) {
 if ( categories.getIsEmpty() ) {
 return ;
 }
 else {
 return this.events.where((  eventValue ) => {
 return categories.contains(eventValue.getCategory()) ;
 }
).toList(false) ;
 }
 }
 public static Map<string,string> getCategoryColors (  ) {
  Map<string,string> colorWithName=new Map<string,string>();
 colorWithName.set("Red","FFD32F2F") ;
 colorWithName.set("Blue","FF1976D2") ;
 colorWithName.set("Green","FF388E3C") ;
 colorWithName.set("Yellow","FFFFA000") ;
 colorWithName.set("Orange","FFFF6D00") ;
 colorWithName.set("Purple","FF7E57C2") ;
 colorWithName.set("Pink","FFC2185B") ;
 colorWithName.set("Brown","FF5D4037") ;
 colorWithName.set("Grey","FF616161") ;
 colorWithName.set("Black","FF000000") ;
 return colorWithName ;
 }
 }