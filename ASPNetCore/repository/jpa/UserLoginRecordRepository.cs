namespace repository.jpa ;
 using d3e.core; using java.util; using models; using store;  public class UserLoginRecordRepository :  AbstractD3ERepository<UserLoginRecord> { public int getTypeIndex (  ) {
 return SchemaConstants.UserLoginRecord ;
 }
 public List < UserLoginRecord > GetByUser (  BaseUser user ) {
  String queryStr="SELECT a._id from _user_login_record a where a._user_id = :user";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"user",user) ;
 return getAllXsByY(query) ;
 }
 }