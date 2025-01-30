namespace Lists ;
 using classes; using d3e.core; using gqltosql; using gqltosql2; using graphql.language; using java.util; using lists; using models; using org.json; using repository.jpa; using rest; using rest.ws; using store;  public class AllDevicesImpl :  AbsDataQueryImpl { private D3EEntityManagerProvider em ;
 
 private GqlToSql gqlToSql ;
 
 private GqlToSql2 gqlToSql2 ;
 
 private BaseUserRepository baseUserRepository ;
 
 public AllDevicesImpl (  D3EEntityManagerProvider em, GqlToSql gqlToSql, GqlToSql2 gqlToSql2, UserRepository userRepository ) {
  this.gqlToSql=gqlToSql;
  this.gqlToSql2=gqlToSql2;
  this.em=em;
  this.userRepository=userRepository;
 }
 public AllDevicesRequest InputToRequest (  AllDevicesIn inputs ) {
  AllDevicesRequest request=new AllDevicesRequest();
 request.Users = baseUserRepository.FindByIds(inputs.users) ;
 return request ;
 }
 public AllDevices Get (  AllDevicesIn inputs ) {
  AllDevicesRequest request=InputToRequest(inputs);
 return Get(request) ;
 }
 public AllDevices Get (  AllDevicesRequest request ) {
  var rows=GetNativeResult(request);
 return GetAsStruct(rows) ;
 }
 public AllDevices GetAsStruct (  List < UserDevice > rows ) {
  List < UserDevice > result=new ArrayList<>();
 foreach ( UserDevice _r1 in rows ) {
 result.Add(NativeSqlUtil.get(em.get(),_r1.getRef(0),SchemaConstants.UserDevice)) ;
 }
  AllDevices wrap=new AllDevices();
 wrap.setItems(result) ;
 return wrap ;
 }
 public JSONObject GetAsJson (  Field field, AllDevicesIn inputs ) {
  AllDevicesRequest request=InputToRequest(inputs);
 return GetAsJson(field,request) ;
 }
 public JSONObject GetAsJson (  Field field, AllDevicesRequest request ) {
  List < UserDevice > rows=GetNativeResult(request);
 return GetAsJson(field,rows) ;
 }
 public JSONObject GetAsJson (  Field field, List < UserDevice > rows ) {
  JSONArray array=new JSONArray();
  List < SqlRow > sqlDecl0=new ArrayList<>();
 foreach ( UserDevice _r1 in rows ) {
 array.put(NativeSqlUtil.getJSONObject(_r1,sqlDecl0)) ;
 }
 gqlToSql.execute("UserDevice",AbstractQueryService.inspect(field,""),sqlDecl0) ;
  JSONObject result=new JSONObject();
 result.put("items",array) ;
 return result ;
 }
 public OutObject GetAsJson (  gqltosql2.Field field, AllDevicesRequest request ) {
  List < UserDevice > rows=GetNativeResult(request);
 return GetAsJson(field,rows) ;
 }
 public OutObject GetAsJson (  gqltosql2.Field field, List < UserDevice > rows ) {
  OutObjectList array=new OutObjectList();
  OutObjectList sqlDecl0=new OutObjectList();
 foreach ( UserDevice _r1 in rows ) {
 array.Add(NativeSqlUtil.getOutObject(_r1,SchemaConstants.UserDevice,sqlDecl0)) ;
 }
 gqlToSql2.execute("UserDevice",RocketQuery.Inspect2(field,""),sqlDecl0) ;
  OutObject result=new OutObject();
 result.AddType(SchemaConstants.AllDevices) ;
 result.Add("items",array) ;
 return result ;
 }
 public List<UserDevice> GetNativeResult (  AllDevicesRequest request ) {
  string sql="select a._id a0 from _user_device a where a._user_id in (:param_0)";
  var query=em.Get().createNativeQuery(sql);
 setDatabaseObjectListParameter(query,"param_0",request.getUsers()) ;
 this.LogQuery(sql,query) ;
  List < NativeObj > result=NativeSqlUtil.createNativeObj(query.getResultList(),0);
 return result ;
 }
 }