namespace repository.jpa ;
 using d3e.core; using java.util; using models; using store;  public class UserDeviceRepository :  AbstractD3ERepository<UserDevice> { public int getTypeIndex (  ) {
 return SchemaConstants.UserDevice ;
 }
 public List < UserDevice > GetByUser (  BaseUser user ) {
  String queryStr="SELECT a._id from _user_device a where a._user_id = :user";
  Query query=em().createNativeQuery(queryStr);
 QueryImplUtil.setParameter(query,"user",user) ;
 return getAllXsByY(query) ;
 }
 }