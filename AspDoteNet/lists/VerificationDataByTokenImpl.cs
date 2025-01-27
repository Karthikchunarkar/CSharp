namespace lists ;
 using AbsDataQueryImpl = lists.AbsDataQueryImpl; using AbstractQueryService = rest.AbstractQueryService; using ArrayList = java.util.ArrayList; using Autowired = org.springframework.beans.factory.annotation.Autowired; using D3EEntityManagerProvider = store.D3EEntityManagerProvider; using Field = graphql.language.Field; using GqlToSql = gqltosql.GqlToSql; using JSONArray = org.json.JSONArray; using JSONObject = org.json.JSONObject; using List = System.Collections.Generic.List; using NativeSqlUtil = lists.NativeSqlUtil; using OutObject = gqltosql2.OutObject; using OutObjectList = gqltosql2.OutObjectList; using Query = jakarta.persistence.Query; using RocketQuery = rest.ws.RocketQuery; using SchemaConstants = d3e.core.SchemaConstants; using SqlRow = gqltosql.SqlRow; using VerificationData = models.VerificationData; using VerificationDataByToken = classes.VerificationDataByToken; using VerificationDataByTokenIn = classes.VerificationDataByTokenIn; using VerificationDataByTokenRequest = models.VerificationDataByTokenRequest;  public class VerificationDataByTokenImpl :  AbsDataQueryImpl { Autowired private D3EEntityManagerProvider em 
 Autowired private GqlToSql gqlToSql 
 Autowired private gqltosql2.GqlToSql gqlToSql2 
 public VerificationDataByTokenRequest inputToRequest (  VerificationDataByTokenIn inputs ) {
  VerificationDataByTokenRequest request=new VerificationDataByTokenRequest();
 request.Token(inputs.token) ;
 return request ;
 }
 public VerificationDataByToken get (  VerificationDataByTokenIn inputs ) {
  VerificationDataByTokenRequest request=inputToRequest(inputs);
 return get(request) ;
 }
 public VerificationDataByToken get (  VerificationDataByTokenRequest request ) {
  List<NativeObj> rows=getNativeResult(request);
 return getAsStruct(rows) ;
 }
 public VerificationDataByToken getAsStruct (  List < NativeObj > rows ) {
  List < VerificationData > result=new ArrayList<>();
 foreach ( NativeObj _r1 in rows ) {
 result.add(NativeSqlUtil.get(em.get(),_r1.getRef(0),SchemaConstants.VerificationData)) ;
 }
  VerificationDataByToken wrap=new VerificationDataByToken();
 wrap.setItems(result) ;
 return wrap ;
 }
 public JSONObject getAsJson (  Field field, VerificationDataByTokenIn inputs ) {
  VerificationDataByTokenRequest request=inputToRequest(inputs);
 return getAsJson(field,request) ;
 }
 public JSONObject getAsJson (  Field field, VerificationDataByTokenRequest request ) {
  List < NativeObj > rows=getNativeResult(request);
 return getAsJson(field,rows) ;
 }
 public JSONObject getAsJson (  Field field, List < NativeObj > rows ) {
  JSONArray array=new JSONArray();
  List < SqlRow > sqlDecl0=new ArrayList<>();
 foreach ( NativeObj _r1 in rows ) {
 array.put(NativeSqlUtil.getJSONObject(_r1,sqlDecl0)) ;
 }
 gqlToSql.execute("VerificationData",AbstractQueryService.inspect(field,""),sqlDecl0) ;
  JSONObject result=new JSONObject();
 result.put("items",array) ;
 return result ;
 }
 public OutObject getAsJson (  gqltosql2.Field field, VerificationDataByTokenRequest request ) {
  List < NativeObj > rows=getNativeResult(request);
 return getAsJson(field,rows) ;
 }
 public OutObject getAsJson (  gqltosql2.Field field, List < NativeObj > rows ) {
  OutObjectList array=new OutObjectList();
  OutObjectList sqlDecl0=new OutObjectList();
 foreach ( NativeObj _r1 in rows ) {
 array.add(NativeSqlUtil.getOutObject(_r1,SchemaConstants.VerificationData,sqlDecl0)) ;
 }
 gqlToSql2.execute("VerificationData",RocketQuery.inspect2(field,""),sqlDecl0) ;
  OutObject result=new OutObject();
 result.addType(SchemaConstants.VerificationDataByToken) ;
 result.add("items",array) ;
 return result ;
 }
 public List<NativeObj> getNativeResult (  VerificationDataByTokenRequest request ) {
  String sql="select a._id a0 from _verification_data a where a._token = :param_0";
  Query query=em.get().createNativeQuery(sql);
 setStringParameter(query,"param_0",request.getToken()) ;
 this.logQuery(sql,query) ;
  List < NativeObj > result=NativeSqlUtil.createNativeObj(query.getResultList(),0);
 return result ;
 }
 }