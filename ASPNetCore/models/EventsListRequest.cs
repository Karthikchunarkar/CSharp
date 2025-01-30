namespace models;
using classes;
using d3e.core;
using store;
public class EventsListRequest : CreatableObject
{
    public static int PAGESIZE = 0;

    public static int OFFSET = 1;

    public static int ORDERBY = 2;

    public static int ASCENDING = 3;

    public static int ORGANIZER = 4;

    public static int APPLYBYSTATUS = 5;

    public static int STATUS = 6;

    private int pageSize { get; set; } = 0;

    private int offset { get; set; } = 0;

    private string orderBy { get; set; }
    private bool ascending { get; set; } = false;

    private Admin organizer { get; set; }
    private bool applyByStatus { get; set; } = false;

    private EventStatus status { get; set; } = EventStatus.Approved;

    public EventsListRequest()
    {
    }
    public int TypeIdx()
    {
        return SchemaConstants.EventsListRequest;
    }
    public string Type()
    {
        return "EventsListRequest";
    }
    public new void UpdateMasters(Action<DatabaseObject> visitor)
    {
        base.UpdateMasters(visitor);
    }
    public new void VisitChildren(Action<DBObject> visitor)
    {
        base.VisitChildren(visitor);
    }
    public int PageSize()
    {
        _CheckProxy();
        return this.pageSize;
    }
    public void PageSize(int pageSize)
    {
        _CheckProxy();
        if (object.Equals(this.pageSize, pageSize))
        {
            return;
        }
        FieldChanged(PAGESIZE, this.pageSize, pageSize);
        this.pageSize = pageSize;
    }
    public int Offset()
    {
        _CheckProxy();
        return this.offset;
    }
    public void Offset(int offset)
    {
        _CheckProxy();
        if (object.Equals(this.offset, offset))
        {
            return;
        }
        FieldChanged(OFFSET, this.offset, offset);
        this.offset = offset;
    }
    public string OrderBy()
    {
        _CheckProxy();
        return this.orderBy;
    }
    public void OrderBy(string orderBy)
    {
        _CheckProxy();
        if (object.Equals(this.orderBy, orderBy))
        {
            return;
        }
        FieldChanged(ORDERBY, this.orderBy, orderBy);
        this.orderBy = orderBy;
    }
    public bool IsAscending()
    {
        _CheckProxy();
        return this.ascending;
    }
    public void Ascending(bool ascending)
    {
        _CheckProxy();
        if (object.Equals(this.ascending, ascending))
        {
            return;
        }
        FieldChanged(ASCENDING, this.ascending, ascending);
        this.ascending = ascending;
    }
    public Admin Organizer()
    {
        _CheckProxy();
        return this.organizer;
    }
    public void Organizer(Admin organizer)
    {
        _CheckProxy();
        if (object.Equals(this.organizer, organizer))
        {
            return;
        }
        FieldChanged(ORGANIZER, this.organizer, organizer);
        this.organizer = organizer;
    }
    public bool IsApplyByStatus()
    {
        _CheckProxy();
        return this.applyByStatus;
    }
    public void ApplyByStatus(bool applyByStatus)
    {
        _CheckProxy();
        if (object.Equals(this.applyByStatus, applyByStatus))
        {
            return;
        }
        FieldChanged(APPLYBYSTATUS, this.applyByStatus, applyByStatus);
        this.applyByStatus = applyByStatus;
    }
    public EventStatus Status()
    {
        _CheckProxy();
        return this.status;
    }
    public void Status(EventStatus status)
    {
        _CheckProxy();
        if (object.Equals(this.status, status))
        {
            return;
        }
        FieldChanged(STATUS, this.status, status);
        this.status = status;
    }
    public string DisplayName()
    {
        return "EventsListRequest";
    }
    public bool equals(Object a)
    {
        return a is EventsListRequest && base.Equals(a);
    }
    public EventsListRequest DeepClone(bool clearId)
    {
        CloneContext ctx = new CloneContext(clearId);
        return ctx.StartClone(this);
    }
    public void DeepCloneIntoObj(ICloneable dbObj, CloneContext ctx)
    {
        base.DeepCloneIntoObj(dbObj, ctx);
        EventsListRequest _obj = ((EventsListRequest)dbObj);
        _obj.PageSize(pageSize);
        _obj.Offset(offset);
        _obj.OrderBy(orderBy);
        _obj.Ascending(ascending);
        _obj.Organizer(organizer);
        _obj.ApplyByStatus(applyByStatus);
        _obj.Status(status);
    }
    public EventsListRequest CloneInstance(EventsListRequest cloneObj)
    {
        if (cloneObj == null)
        {
            cloneObj = new EventsListRequest();
        }
        base.CloneInstance(cloneObj);
        cloneObj.PageSize(this.PageSize());
        cloneObj.Offset(this.Offset());
        cloneObj.OrderBy(this.OrderBy());
        cloneObj.Ascending(this.IsAscending());
        cloneObj.Organizer(this.Organizer());
        cloneObj.ApplyByStatus(this.IsApplyByStatus());
        cloneObj.Status(this.Status());
        return cloneObj;
    }
    public bool TransientModel()
    {
        return true;
    }
    public void CollectCreatableReferences(List<Object> _refs)
    {
        base.CollectCreatableReferences(_refs);
        _refs.Add(this.organizer);
    }
    public bool IsEntity()
    {
        return true;
    }

    public override EventsListRequest CreateNewInstance()
    {
        throw new NotImplementedException();
    }

    protected override int _FieldsCount()
    {
        throw new NotImplementedException();
    }

    public override int _TypeIdx()
    {
        throw new NotImplementedException();
    }

    public override string _Type()
    {
        throw new NotImplementedException();
    }
}