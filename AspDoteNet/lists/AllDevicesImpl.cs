namespace lists ;
 using AbsDataQueryImpl = lists.AbsDataQueryImpl; using AbstractQueryService = rest.AbstractQueryService; using AllDevices = classes.AllDevices; using AllDevicesIn = classes.AllDevicesIn; using AllDevicesRequest = models.AllDevicesRequest; using ArrayList = java.util.ArrayList; using Autowired = org.springframework.beans.factory.annotation.Autowired; using BaseUserRepository = repository.jpa.BaseUserRepository; using D3EEntityManagerProvider = store.D3EEntityManagerProvider; using Field = graphql.language.Field; using GqlToSql = gqltosql.GqlToSql; using JSONArray = org.json.JSONArray; using JSONObject = org.json.JSONObject; using List = System.Collections.Generic.List; using NativeSqlUtil = lists.NativeSqlUtil; using OutObject = gqltosql2.OutObject; using OutObjectList = gqltosql2.OutObjectList; using Query = jakarta.persistence.Query; using RocketQuery = rest.ws.RocketQuery; using SchemaConstants = d3e.core.SchemaConstants; using SqlRow = gqltosql.SqlRow; using UserDevice = models.UserDevice;  public class AllDevicesImpl :  AbsDataQueryImpl { Autowired private D3EEntityManagerProvider em 
 Autowired private GqlToSql gqlToSql 
 Autowired private gqltosql2.GqlToSql gqlToSql2 
 Autowired private BaseUserRepository baseUserRepository 
 public AllDevicesRequest inputToRequest (  AllDevicesIn inputs ) {
  AllDevicesRequest request=new AllDevicesRequest();
 request.Users(baseUserRepository.findByIds(inputs.users)) ;
 return request ;
 }
 public AllDevices get (  AllDevicesIn inputs ) {
  AllDevicesRequest request=inputToRequest(inputs);
 return get(request) ;
 }
 public AllDevices get (  AllDevicesRequest request ) {
  List<NativeObj> rows=getNativeResult(request);
 return getAsStruct(rows) ;
 }
 public AllDevices getAsStruct (  List < NativeObj > rows ) {
  List < UserDevice > result=new ArrayList<>();
 foreach ( NativeObj _r1 in rows ) {
 result.add(NativeSqlUtil.get(em.get(),_r1.getRef(0),SchemaConstants.UserDevice)) ;
 }
  AllDevices wrap=new AllDevices();
 wrap.setItems(result) ;
 return wrap ;
 }
 public JSONObject getAsJson (  Field field, AllDevicesIn inputs ) {
  AllDevicesRequest request=inputToRequest(inputs);
 return getAsJson(field,request) ;
 }
 public JSONObject getAsJson (  Field field, AllDevicesRequest request ) {
  List < NativeObj > rows=getNativeResult(request);
 return getAsJson(field,rows) ;
 }
 public JSONObject getAsJson (  Field field, List < NativeObj > rows ) {
  JSONArray array=new JSONArray();
  List < SqlRow > sqlDecl0=new ArrayList<>();
 foreach ( NativeObj _r1 in rows ) {
 array.put(NativeSqlUtil.getJSONObject(_r1,sqlDecl0)) ;
 }
 gqlToSql.execute("UserDevice",AbstractQueryService.inspect(field,""),sqlDecl0) ;
  JSONObject result=new JSONObject();
 result.put("items",array) ;
 return result ;
 }
 public OutObject getAsJson (  gqltosql2.Field field, AllDevicesRequest request ) {
  List < NativeObj > rows=getNativeResult(request);
 return getAsJson(field,rows) ;
 }
 public OutObject getAsJson (  gqltosql2.Field field, List < NativeObj > rows ) {
  OutObjectList array=new OutObjectList();
  OutObjectList sqlDecl0=new OutObjectList();
 foreach ( NativeObj _r1 in rows ) {
 array.add(NativeSqlUtil.getOutObject(_r1,SchemaConstants.UserDevice,sqlDecl0)) ;
 }
 gqlToSql2.execute("UserDevice",RocketQuery.inspect2(field,""),sqlDecl0) ;
  OutObject result=new OutObject();
 result.addType(SchemaConstants.AllDevices) ;
 result.add("items",array) ;
 return result ;
 }
 public List<NativeObj> getNativeResult (  AllDevicesRequest request ) {
  String sql="select a._id a0 from _user_device a where a._user_id in (:param_0)";
  Query query=em.get().createNativeQuery(sql);
 setDatabaseObjectListParameter(query,"param_0",request.getUsers()) ;
 this.logQuery(sql,query) ;
  List < NativeObj > result=NativeSqlUtil.createNativeObj(query.getResultList(),0);
 return result ;
 }
 }