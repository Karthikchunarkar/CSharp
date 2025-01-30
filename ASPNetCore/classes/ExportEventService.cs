namespace classes ;
 using classes;  public class ExportEventService { public ExportEventService (  ) {
 }
 public static string exportURL (  List<string> categories ) {
 return CalendarController.prepareLink(categories) ;
 }
 }