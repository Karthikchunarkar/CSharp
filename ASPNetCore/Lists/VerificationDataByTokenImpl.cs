namespace Lists ;
 using classes; using d3e.core; using gqltosql; using gqltosql2; using graphql.language; using java.util; using lists; using models; using org.json; using rest; using rest.ws; using store;  public class VerificationDataByTokenImpl :  AbsDataQueryImpl { private D3EEntityManagerProvider em ;
 
 private GqlToSql gqlToSql ;
 
 private GqlToSql2 gqlToSql2 ;
 
 public VerificationDataByTokenImpl (  D3EEntityManagerProvider em, GqlToSql gqlToSql, GqlToSql2 gqlToSql2, UserRepository userRepository ) {
  this.gqlToSql=gqlToSql;
  this.gqlToSql2=gqlToSql2;
  this.em=em;
  this.userRepository=userRepository;
 }
 public VerificationDataByTokenRequest InputToRequest (  VerificationDataByTokenIn inputs ) {
  VerificationDataByTokenRequest request=new VerificationDataByTokenRequest();
 request.Token = inputs.token ;
 return request ;
 }
 public VerificationDataByToken Get (  VerificationDataByTokenIn inputs ) {
  VerificationDataByTokenRequest request=InputToRequest(inputs);
 return Get(request) ;
 }
 public VerificationDataByToken Get (  VerificationDataByTokenRequest request ) {
  var rows=GetNativeResult(request);
 return GetAsStruct(rows) ;
 }
 public VerificationDataByToken GetAsStruct (  List < VerificationData > rows ) {
  List < VerificationData > result=new ArrayList<>();
 foreach ( Verification Data _r1 in rows ) {
 result.Add(NativeSqlUtil.get(em.get(),_r1.getRef(0),SchemaConstants.VerificationData)) ;
 }
  VerificationDataByToken wrap=new VerificationDataByToken();
 wrap.setItems(result) ;
 return wrap ;
 }
 public JSONObject GetAsJson (  Field field, VerificationDataByTokenIn inputs ) {
  VerificationDataByTokenRequest request=InputToRequest(inputs);
 return GetAsJson(field,request) ;
 }
 public JSONObject GetAsJson (  Field field, VerificationDataByTokenRequest request ) {
  List < VerificationData > rows=GetNativeResult(request);
 return GetAsJson(field,rows) ;
 }
 public JSONObject GetAsJson (  Field field, List < VerificationData > rows ) {
  JSONArray array=new JSONArray();
  List < SqlRow > sqlDecl0=new ArrayList<>();
 foreach ( Verification Data _r1 in rows ) {
 array.put(NativeSqlUtil.getJSONObject(_r1,sqlDecl0)) ;
 }
 gqlToSql.execute("VerificationData",AbstractQueryService.inspect(field,""),sqlDecl0) ;
  JSONObject result=new JSONObject();
 result.put("items",array) ;
 return result ;
 }
 public OutObject GetAsJson (  gqltosql2.Field field, VerificationDataByTokenRequest request ) {
  List < VerificationData > rows=GetNativeResult(request);
 return GetAsJson(field,rows) ;
 }
 public OutObject GetAsJson (  gqltosql2.Field field, List < VerificationData > rows ) {
  OutObjectList array=new OutObjectList();
  OutObjectList sqlDecl0=new OutObjectList();
 foreach ( Verification Data _r1 in rows ) {
 array.Add(NativeSqlUtil.getOutObject(_r1,SchemaConstants.VerificationData,sqlDecl0)) ;
 }
 gqlToSql2.execute("VerificationData",RocketQuery.Inspect2(field,""),sqlDecl0) ;
  OutObject result=new OutObject();
 result.AddType(SchemaConstants.VerificationDataByToken) ;
 result.Add("items",array) ;
 return result ;
 }
 public List<VerificationData> GetNativeResult (  VerificationDataByTokenRequest request ) {
  string sql="select a._id a0 from _verification_data a where a._token = :param_0";
  var query=em.Get().createNativeQuery(sql);
 setStringParameter(query,"param_0",request.getToken()) ;
 this.LogQuery(sql,query) ;
  List < NativeObj > result=NativeSqlUtil.createNativeObj(query.getResultList(),0);
 return result ;
 }
 }