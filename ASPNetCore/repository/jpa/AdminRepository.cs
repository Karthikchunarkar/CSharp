namespace repository.jpa ;
 using d3e.core; using java.util; using models; using store;  public class AdminRepository :  AbstractD3ERepository<Admin> { public int getTypeIndex (  ) {
 return SchemaConstants.Admin ;
 }
 public List < Admin > GetByDevices (  UserDevice devices ) {
  String queryStr="SELECT a._id from _admin a left join _base_user b on b._id = a._id where b._devices_id = :devices";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"devices",devices) ;
 return getAllXsByY(query) ;
 }
 public bool checkEmailUnique (  Long id, string email ) {
  String queryStr="SELECT CASE WHEN count(*) > 0 THEN false ELSE true END as _a from _admin a where a._email = :email and a._id != :id";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"email",email) ;
 QueryImplUtil.setParameter(query,"id",id) ;
 return checkUnique(query) ;
 }
 public Admin GetByEmail (  string email ) {
  String queryStr="SELECT a._id from _admin a where a._email = :email";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"email",email) ;
 return getXByY(query) ;
 }
 }