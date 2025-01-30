namespace Lists ;
 using classes; using d3e.core; using gqltosql; using gqltosql2; using graphql.language; using java.util; using lists; using models; using org.json; using rest; using rest.ws; using store;  public class UsersListImpl :  AbsDataQueryImpl { private D3EEntityManagerProvider em ;
 
 private GqlToSql gqlToSql ;
 
 private GqlToSql2 gqlToSql2 ;
 
 public UsersListImpl (  D3EEntityManagerProvider em, GqlToSql gqlToSql, GqlToSql2 gqlToSql2, UserRepository userRepository ) {
  this.gqlToSql=gqlToSql;
  this.gqlToSql2=gqlToSql2;
  this.em=em;
  this.userRepository=userRepository;
 }
 public UsersListRequest InputToRequest (  UsersListIn inputs ) {
  UsersListRequest request=new UsersListRequest();
 request.PageSize = inputs.pageSize ;
 request.Offset = inputs.offset ;
 request.OrderBy = inputs.orderBy ;
 request.Ascending = inputs.ascending ;
 return request ;
 }
 public UsersList Get (  UsersListIn inputs ) {
  UsersListRequest request=InputToRequest(inputs);
 return Get(request) ;
 }
 public UsersList Get (  UsersListRequest request ) {
  var rows=GetNativeResult(request);
  long count=GetCountResult(request);
 return GetAsStruct(rows,count) ;
 }
 public UsersList GetAsStruct (  List < Admin > rows, long count ) {
  List < Admin > result=new ArrayList<>();
 foreach ( Admin _r1 in rows ) {
 result.Add(NativeSqlUtil.get(em.get(),_r1.getRef(1),SchemaConstants.Admin)) ;
 }
  UsersList wrap=new UsersList();
 wrap.setItems(result) ;
 wrap.setCount(count) ;
 return wrap ;
 }
 public JSONObject GetAsJson (  Field field, UsersListIn inputs ) {
  UsersListRequest request=InputToRequest(inputs);
 return GetAsJson(field,request) ;
 }
 public JSONObject GetAsJson (  Field field, UsersListRequest request ) {
  List < Admin > rows=GetNativeResult(request);
  long count=GetCountResult(request);
 return GetAsJson(field,rows,count) ;
 }
 public JSONObject GetAsJson (  Field field, List < Admin > rows, long count ) {
  JSONArray array=new JSONArray();
  List < SqlRow > sqlDecl0=new ArrayList<>();
 foreach ( Admin _r1 in rows ) {
 array.put(NativeSqlUtil.getJSONObject(_r1,sqlDecl0)) ;
 }
 gqlToSql.execute("Admin",AbstractQueryService.inspect(field,""),sqlDecl0) ;
  JSONObject result=new JSONObject();
 result.put("items",array) ;
 result.put("count",count) ;
 return result ;
 }
 public OutObject GetAsJson (  gqltosql2.Field field, UsersListRequest request ) {
  List < Admin > rows=GetNativeResult(request);
  long count=GetCountResult(request);
 return GetAsJson(field,rows,count) ;
 }
 public OutObject GetAsJson (  gqltosql2.Field field, List < Admin > rows, long count ) {
  OutObjectList array=new OutObjectList();
  OutObjectList sqlDecl0=new OutObjectList();
 foreach ( Admin _r1 in rows ) {
 array.Add(NativeSqlUtil.getOutObject(_r1,SchemaConstants.Admin,sqlDecl0)) ;
 }
 gqlToSql2.execute("Admin",RocketQuery.Inspect2(field,""),sqlDecl0) ;
  OutObject result=new OutObject();
 result.AddType(SchemaConstants.UsersList) ;
 result.Add("items",array) ;
 result.Add("count",count) ;
 return result ;
 }
 public List<Admin> GetNativeResult (  UsersListRequest request ) {
 assertLimitNotNegative(request.getPageSize()) ;
  string sql="select a._full_name a0, a._id a1 from _admin a order by case when (:param_1) then (a._full_name) end asc, case when NOT(:param_1) then (a._full_name) end desc limit :param_2 offset :param_3";
  var query=em.Get().createNativeQuery(sql);
 setStringParameter(query,"param_0",request.getOrderBy()) ;
 setBooleanParameter(query,"param_1",request.isAscending()) ;
 setIntegerParameter(query,"param_2",request.getPageSize()) ;
 setIntegerParameter(query,"param_3",request.getOffset()) ;
 this.LogQuery(sql,query) ;
  List < NativeObj > result=NativeSqlUtil.createNativeObj(query.getResultList(),1);
 return result ;
 }
 public long GetCountResult (  UsersListRequest request ) {
  string sql="select count(a._id) a0 from _admin a";
  var query=em.Get().createNativeQuery(sql);
 this.LogQuery(sql,query) ;
 return GetCountResult(query) ;
 }
 }