namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using BaseUser = models.BaseUser; using List = java.util.List; using Query = store.Query; using QueryImplUtil = store.QueryImplUtil; using SchemaConstants = d3e.core.SchemaConstants; using UserDevice = models.UserDevice;  public class UserDeviceRepository :  AbstractD3ERepository<UserDevice> { public int getTypeIndex (  ) {
 return SchemaConstants.UserDevice ;
 }
 public List < UserDevice > GetByUser (  BaseUser user ) {
  String queryStr="SELECT a._id from _user_device a where a._user_id = :user";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"user",user) ;
 return getAllXsByY(query) ;
 }
 }