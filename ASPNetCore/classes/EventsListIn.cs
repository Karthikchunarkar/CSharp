namespace classes ;
 using classes;  public class EventsListIn { public int pageSize ;
 
 public int offset ;
 
 public string orderBy ;
 
 public bool ascending ;
 
 public long organizer ;
 
 public bool applyByStatus ;
 
 public EventStatus status ;
 
 public EventsListIn (  ) {
 }
 public EventsListIn (  bool applyByStatus, bool ascending, int offset, string orderBy, long organizer, int pageSize, EventStatus status ) {
 this.pageSize = pageSize ;
 this.offset = offset ;
 this.orderBy = orderBy ;
 this.ascending = ascending ;
 this.organizer = organizer ;
 this.applyByStatus = applyByStatus ;
 this.status = status ;
 }
 }