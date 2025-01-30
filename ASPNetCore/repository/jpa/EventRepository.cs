namespace repository.jpa ;
 using d3e.core; using java.util; using models; using store;  public class EventRepository :  AbstractD3ERepository<Event> { public int getTypeIndex (  ) {
 return SchemaConstants.Event ;
 }
 public List < Event > GetByCategory (  Category category ) {
  String queryStr="SELECT a._id from _event a where a._category_id = :category";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"category",category) ;
 return getAllXsByY(query) ;
 }
 public List < Event > GetByOrganizer (  Admin organizer ) {
  String queryStr="SELECT a._id from _event a where a._organizer_id = :organizer";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"organizer",organizer) ;
 return getAllXsByY(query) ;
 }
 }