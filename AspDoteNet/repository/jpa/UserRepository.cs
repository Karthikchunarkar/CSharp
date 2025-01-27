namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using List = java.util.List; using Query = store.Query; using QueryImplUtil = store.QueryImplUtil; using SchemaConstants = d3e.core.SchemaConstants; using User = models.User; using UserDevice = models.UserDevice; using string = System.string;  public class UserRepository :  AbstractD3ERepository<User> { public int getTypeIndex (  ) {
 return SchemaConstants.User ;
 }
 public List < User > GetByDevices (  UserDevice devices ) {
  String queryStr="SELECT a._id from _user a left join _base_user b on b._id = a._id where b._devices_id = :devices";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"devices",devices) ;
 return getAllXsByY(query) ;
 }
 public boolean checkEmailUnique (  Long id, string email ) {
  String queryStr="SELECT CASE WHEN count(*) > 0 THEN false ELSE true END as _a from _user a where a._email = :email and a._id != :id";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"email",email) ;
 QueryImplUtil.setParameter(query,"id",id) ;
 return checkUnique(query) ;
 }
 public User GetByEmail (  string email ) {
  String queryStr="SELECT a._id from _user a where a._email = :email";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"email",email) ;
 return getXByY(query) ;
 }
 }