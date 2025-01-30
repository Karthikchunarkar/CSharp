namespace repository.jpa ;
 using d3e.core; using java.util; using models; using store;  public class PushNotificationRepository :  AbstractD3ERepository<PushNotification> { public int getTypeIndex (  ) {
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