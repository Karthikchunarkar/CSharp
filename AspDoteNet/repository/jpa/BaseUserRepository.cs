namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using BaseUser = models.BaseUser; using List = java.util.List; using Query = store.Query; using QueryImplUtil = store.QueryImplUtil; using SchemaConstants = d3e.core.SchemaConstants; using UserDevice = models.UserDevice;  public class BaseUserRepository :  AbstractD3ERepository<BaseUser> { public int getTypeIndex (  ) {
 return SchemaConstants.BaseUser ;
 }
 public List < BaseUser > GetByDevices (  UserDevice devices ) {
  String queryStr="SELECT a._id from _base_user a where a._devices_id = :devices";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"devices",devices) ;
 return getAllXsByY(query) ;
 }
 }