namespace repository.jpa ;
 using d3e.core; using java.util; using models; using store;  public class OneTimePasswordRepository :  AbstractD3ERepository<OneTimePassword> { public int getTypeIndex (  ) {
 return SchemaConstants.OneTimePassword ;
 }
 public bool checkTokenUnique (  Long id, string token ) {
  String queryStr="SELECT CASE WHEN count(*) > 0 THEN false ELSE true END as _a from _one_time_password a where a._token = :token and a._id != :id";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"token",token) ;
 QueryImplUtil.setParameter(query,"id",id) ;
 return checkUnique(query) ;
 }
 public OneTimePassword GetByToken (  string token ) {
  String queryStr="SELECT a._id from _one_time_password a where a._token = :token";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"token",token) ;
 return getXByY(query) ;
 }
 public List < OneTimePassword > GetByUser (  BaseUser user ) {
  String queryStr="SELECT a._id from _one_time_password a where a._user_id = :user";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"user",user) ;
 return getAllXsByY(query) ;
 }
 }