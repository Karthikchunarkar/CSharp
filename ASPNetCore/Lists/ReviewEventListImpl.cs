namespace Lists ;
 using classes; using d3e.core; using gqltosql; using gqltosql2; using graphql.language; using java.util; using lists; using models; using org.json; using rest; using rest.ws; using store;  public class ReviewEventListImpl :  AbsDataQueryImpl { private D3EEntityManagerProvider em ;
 
 private GqlToSql gqlToSql ;
 
 private GqlToSql2 gqlToSql2 ;
 
 public ReviewEventListImpl (  D3EEntityManagerProvider em, GqlToSql gqlToSql, GqlToSql2 gqlToSql2, UserRepository userRepository ) {
  this.gqlToSql=gqlToSql;
  this.gqlToSql2=gqlToSql2;
  this.em=em;
  this.userRepository=userRepository;
 }
 public ReviewEventListRequest InputToRequest (  ReviewEventListIn inputs ) {
  ReviewEventListRequest request=new ReviewEventListRequest();
 request.ReferenceID = inputs.referenceID ;
 return request ;
 }
 public ReviewEventList Get (  ReviewEventListIn inputs ) {
  ReviewEventListRequest request=InputToRequest(inputs);
 return Get(request) ;
 }
 public ReviewEventList Get (  ReviewEventListRequest request ) {
  var rows=GetNativeResult(request);
  long count=GetCountResult(request);
 return GetAsStruct(rows,count) ;
 }
 public ReviewEventList GetAsStruct (  List < Event > rows, long count ) {
  List < Event > result=new ArrayList<>();
 foreach ( Event _r1 in rows ) {
 result.Add(NativeSqlUtil.get(em.get(),_r1.getRef(1),SchemaConstants.Event)) ;
 }
  ReviewEventList wrap=new ReviewEventList();
 wrap.setItems(result) ;
 wrap.setCount(count) ;
 return wrap ;
 }
 public JSONObject GetAsJson (  Field field, ReviewEventListIn inputs ) {
  ReviewEventListRequest request=InputToRequest(inputs);
 return GetAsJson(field,request) ;
 }
 public JSONObject GetAsJson (  Field field, ReviewEventListRequest request ) {
  List < Event > rows=GetNativeResult(request);
  long count=GetCountResult(request);
 return GetAsJson(field,rows,count) ;
 }
 public JSONObject GetAsJson (  Field field, List < Event > rows, long count ) {
  JSONArray array=new JSONArray();
  List < SqlRow > sqlDecl0=new ArrayList<>();
 foreach ( Event _r1 in rows ) {
 array.put(NativeSqlUtil.getJSONObject(_r1,sqlDecl0)) ;
 }
 gqlToSql.execute("Event",AbstractQueryService.inspect(field,""),sqlDecl0) ;
  JSONObject result=new JSONObject();
 result.put("items",array) ;
 result.put("count",count) ;
 return result ;
 }
 public OutObject GetAsJson (  gqltosql2.Field field, ReviewEventListRequest request ) {
  List < Event > rows=GetNativeResult(request);
  long count=GetCountResult(request);
 return GetAsJson(field,rows,count) ;
 }
 public OutObject GetAsJson (  gqltosql2.Field field, List < Event > rows, long count ) {
  OutObjectList array=new OutObjectList();
  OutObjectList sqlDecl0=new OutObjectList();
 foreach ( Event _r1 in rows ) {
 array.Add(NativeSqlUtil.getOutObject(_r1,SchemaConstants.Event,sqlDecl0)) ;
 }
 gqlToSql2.execute("Event",RocketQuery.Inspect2(field,""),sqlDecl0) ;
  OutObject result=new OutObject();
 result.AddType(SchemaConstants.ReviewEventList) ;
 result.Add("items",array) ;
 result.Add("count",count) ;
 return result ;
 }
 public List<Event> GetNativeResult (  ReviewEventListRequest request ) {
  string sql="select a._reference_id a0, a._id a1 from _event a where :param_0 or a._reference_id = :param_1";
  var query=em.Get().createNativeQuery(sql);
 setBooleanParameter(query,"param_0",request.getReferenceID() == null) ;
 setStringParameter(query,"param_1",request.getReferenceID()) ;
 this.LogQuery(sql,query) ;
  List < NativeObj > result=NativeSqlUtil.createNativeObj(query.getResultList(),1);
 return result ;
 }
 public long GetCountResult (  ReviewEventListRequest request ) {
  string sql="select count(a._id) a0 from _event a where :param_0 or a._reference_id = :param_1";
  var query=em.Get().createNativeQuery(sql);
 setBooleanParameter(query,"param_0",request.getReferenceID() == null) ;
 setStringParameter(query,"param_1",request.getReferenceID()) ;
 this.LogQuery(sql,query) ;
 return GetCountResult(query) ;
 }
 }