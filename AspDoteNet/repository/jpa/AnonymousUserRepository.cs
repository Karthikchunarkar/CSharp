namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using AnonymousUser = models.AnonymousUser; using List = java.util.List; using Query = store.Query; using QueryImplUtil = store.QueryImplUtil; using SchemaConstants = d3e.core.SchemaConstants; using UserDevice = models.UserDevice;  public class AnonymousUserRepository :  AbstractD3ERepository<AnonymousUser> { public int getTypeIndex (  ) {
 return SchemaConstants.AnonymousUser ;
 }
 public List < AnonymousUser > GetByDevices (  UserDevice devices ) {
  String queryStr="SELECT a._id from _anonymous_user a left join _base_user b on b._id = a._id where b._devices_id = :devices";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"devices",devices) ;
 return getAllXsByY(query) ;
 }
 }