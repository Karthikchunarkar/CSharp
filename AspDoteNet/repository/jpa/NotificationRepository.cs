namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using List = java.util.List; using Notification = models.Notification; using Query = store.Query; using QueryImplUtil = store.QueryImplUtil; using ResourceData = models.ResourceData; using SchemaConstants = d3e.core.SchemaConstants;  public class NotificationRepository :  AbstractD3ERepository<Notification> { public int getTypeIndex (  ) {
 return SchemaConstants.Notification ;
 }
 public List < Notification > GetByResourceData (  ResourceData resourceData ) {
  String queryStr="SELECT a._id from _notification a where a._resource_data_id = :resourceData";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"resourceData",resourceData) ;
 return getAllXsByY(query) ;
 }
 }