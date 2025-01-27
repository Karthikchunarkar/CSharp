namespace repository.jpa ;
 using AbstractD3ERepository = repository.jpa.AbstractD3ERepository; using BaseUser = models.BaseUser; using List = java.util.List; using Query = store.Query; using QueryImplUtil = store.QueryImplUtil; using SchemaConstants = d3e.core.SchemaConstants; using UserLoginRecord = models.UserLoginRecord;  public class UserLoginRecordRepository :  AbstractD3ERepository<UserLoginRecord> { public int getTypeIndex (  ) {
 return SchemaConstants.UserLoginRecord ;
 }
 public List < UserLoginRecord > GetByUser (  BaseUser user ) {
  String queryStr="SELECT a._id from _user_login_record a where a._user_id = :user";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"user",user) ;
 return getAllXsByY(query) ;
 }
 }