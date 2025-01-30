namespace Lists;
using classes;
using d3e.core;
using gqltosql;
using gqltosql2;
using graphql.language;
using java.util;
using list;
using lists;
using models;
using org.json;
using repository.jpa;
using rest;
using rest.ws;
using store;
public class EventsListImpl : AbsDataQueryImpl
{
    private D3EEntityManagerProvider em;

    private GqlToSql gqlToSql;

    private GqlToSql gqlToSql2;

    private AdminRepository adminRepository;

    public EventsListImpl(D3EEntityManagerProvider em, GqlToSql gqlToSql, AdminRepository adminRepository)
    {
        this.gqlToSql = gqlToSql;
        this.em = em;
        this.adminRepository = adminRepository;
    }
    public EventsListRequest InputToRequest(EventsListIn inputs)
    {
        EventsListRequest request = new EventsListRequest();
        request.PageSize = inputs.pageSize;
        request.Offset = inputs.offset;
        request.OrderBy = inputs.orderBy;
        request.Ascending = inputs.ascending;
        request.Organizer = adminRepository.FindById(inputs.organizer);
        request.ApplyByStatus = inputs.applyByStatus;
        request.Status = inputs.status;
        return request;
    }
    public EventsList Get(EventsListIn inputs)
    {
        EventsListRequest request = InputToRequest(inputs);
        return Get(request);
    }
    public EventsList Get(EventsListRequest request)
    {
        var rows = GetNativeResult(request);
        long count = GetCountResult(request);
        return GetAsStruct(rows, count);
    }
    public EventsList GetAsStruct(List<Event> rows, long count)
    {
        List<Event> result = new ArrayList<>();
        foreach (Event _r1 in rows)
        {
            result.Add(NativeSqlUtil.get(em.get(), _r1.getRef(3), SchemaConstants.Event));
        }
        EventsList wrap = new EventsList();
        wrap.setItems(result);
        wrap.setCount(count);
        return wrap;
    }
    public JSONObject GetAsJson(Field field, EventsListIn inputs)
    {
        EventsListRequest request = InputToRequest(inputs);
        return GetAsJson(field, request);
    }
    public JSONObject GetAsJson(Field field, EventsListRequest request)
    {
        List<Event> rows = GetNativeResult(request);
        long count = GetCountResult(request);
        return GetAsJson(field, rows, count);
    }
    public JSONObject GetAsJson(Field field, List<Event> rows, long count)
    {
        JSONArray array = new JSONArray();
        List<SqlRow> sqlDecl0 = new ArrayList<>();
        foreach (Event _r1 in rows)
        {
            array.put(NativeSqlUtil.getJSONObject(_r1, sqlDecl0));
        }
        gqlToSql.execute("Event", AbstractQueryService.inspect(field, ""), sqlDecl0);
        JSONObject result = new JSONObject();
        result.put("items", array);
        result.put("count", count);
        return result;
    }
    public OutObject GetAsJson(gqltosql2.Field field, EventsListRequest request)
    {
        List<Event> rows = GetNativeResult(request);
        long count = GetCountResult(request);
        return GetAsJson(field, rows, count);
    }
    public OutObject GetAsJson(gqltosql2.Field field, List<Event> rows, long count)
    {
        OutObjectList array = new OutObjectList();
        OutObjectList sqlDecl0 = new OutObjectList();
        foreach (Event _r1 in rows)
        {
            array.Add(NativeSqlUtil.getOutObject(_r1, SchemaConstants.Event, sqlDecl0));
        }
        gqlToSql2.execute("Event", RocketQuery.Inspect2(field, ""), sqlDecl0);
        OutObject result = new OutObject();
        result.AddType(SchemaConstants.EventsList);
        result.Add("items", array);
        result.Add("count", count);
        return result;
    }
    public List<Event> GetNativeResult(EventsListRequest request)
    {
        assertLimitNotNegative(request.getPageSize());
        string sql = "select a._organizer_id a0, a._status a1, a._event_name a2, a._id a3 from _event a where (:param_0 or a._organizer_id = :param_1) and (:param_2 or :param_3 = a._status) order by case when (:param_5) then (a._event_name) end asc, case when NOT(:param_5) then (a._event_name) end desc limit :param_6 offset :param_7";
        var query = em.Get().createNativeQuery(sql);
        setBooleanParameter(query, "param_0", request.getOrganizer() == null);
        setDatabaseObjectParameter(query, "param_1", request.getOrganizer());
        setBooleanParameter(query, "param_2", request.isApplyByStatus() == false);
        setEnumParameter(query, "param_3", request.getStatus());
        setStringParameter(query, "param_4", request.getOrderBy());
        setBooleanParameter(query, "param_5", request.isAscending());
        setIntegerParameter(query, "param_6", request.getPageSize());
        setIntegerParameter(query, "param_7", request.getOffset());
        this.LogQuery(sql, query);
        List<NativeObj> result = NativeSqlUtil.createNativeObj(query.getResultList(), 3);
        return result;
    }
    public long GetCountResult(EventsListRequest request)
    {
        string sql = "select count(a._id) a0 from _event a where (:param_0 or a._organizer_id = :param_1) and (:param_2 or :param_3 = a._status)";
        var query = em.Get().createNativeQuery(sql);
        setBooleanParameter(query, "param_0", request.getOrganizer() == null);
        setDatabaseObjectParameter(query, "param_1", request.getOrganizer());
        setBooleanParameter(query, "param_2", request.isApplyByStatus() == false);
        setEnumParameter(query, "param_3", request.getStatus());
        this.LogQuery(sql, query);
        return GetCountResult(query);
    }
}