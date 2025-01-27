namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using BaseUser = models.BaseUser; using List = java.util.List; using PushNotification = models.PushNotification; using Query = store.Query; using QueryImplUtil = store.QueryImplUtil; using SchemaConstants = d3e.core.SchemaConstants; using UserDevice = models.UserDevice;  public class PushNotificationRepository :  AbstractD3ERepository<PushNotification> { public int getTypeIndex (  ) {
 return SchemaConstants.PushNotification ;
 }
 public List < PushNotification > FindByUsers (  BaseUser users ) {
  String queryStr="select a._push_notification_id from _push_notification_users_fab88b a where a._users_id = :users";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"users",users) ;
 return getAllXsByY(query) ;
 }
 public List < PushNotification > FindByFailedDevices (  UserDevice failedDevices ) {
  String queryStr="select a._push_notification_id from _push_notification_failed_devices_d63aa4 a where a._failed_devices_id = :failedDevices";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"failedDevices",failedDevices) ;
 return getAllXsByY(query) ;
 }
 }