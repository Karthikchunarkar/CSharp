namespace Lists ;
 using classes; using d3e.core; using gqltosql; using gqltosql2; using graphql.language; using java.util; using lists; using models; using org.json; using rest; using rest.ws; using store;  public class CalendarEventListImpl :  AbsDataQueryImpl { private D3EEntityManagerProvider em ;
 
 private GqlToSql gqlToSql ;
 
 private GqlToSql2 gqlToSql2 ;
 
 public CalendarEventListImpl (  D3EEntityManagerProvider em, GqlToSql gqlToSql, GqlToSql2 gqlToSql2, UserRepository userRepository ) {
  this.gqlToSql=gqlToSql;
  this.gqlToSql2=gqlToSql2;
  this.em=em;
  this.userRepository=userRepository;
 }
 public CalendarEventList Get (  ) {
  var rows=GetNativeResult();
  long count=GetCountResult();
 return GetAsStruct(rows,count) ;
 }
 public CalendarEventList GetAsStruct (  List < Event > rows, long count ) {
  List < Event > result=new ArrayList<>();
 foreach ( Event _r1 in rows ) {
 result.Add(NativeSqlUtil.get(em.get(),_r1.getRef(1),SchemaConstants.Event)) ;
 }
  CalendarEventList wrap=new CalendarEventList();
 wrap.setItems(result) ;
 wrap.setCount(count) ;
 return wrap ;
 }
 public JSONObject GetAsJson (  Field field ) {
  List < Event > rows=GetNativeResult();
  long count=GetCountResult();
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
 public OutObject GetAsJson (  gqltosql2.Field field ) {
  List < Event > rows=GetNativeResult();
  long count=GetCountResult();
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
 result.AddType(SchemaConstants.CalendarEventList) ;
 result.Add("items",array) ;
 result.Add("count",count) ;
 return result ;
 }
 public List<Event> GetNativeResult (  ) {
  string sql="select a._status a0, a._id a1 from _event a where a._status = :param_0";
  var query=em.Get().createNativeQuery(sql);
 setEnumParameter(query,"param_0",EventStatus.Approved) ;
 this.LogQuery(sql,query) ;
  List < NativeObj > result=NativeSqlUtil.createNativeObj(query.getResultList(),1);
 return result ;
 }
 public long GetCountResult (  ) {
  string sql="select count(a._id) a0 from _event a where a._status = :param_0";
  var query=em.Get().createNativeQuery(sql);
 setEnumParameter(query,"param_0",EventStatus.Approved) ;
 this.LogQuery(sql,query) ;
 return GetCountResult(query) ;
 }
 }