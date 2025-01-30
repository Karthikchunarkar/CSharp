namespace repository.jpa;
using d3e.core;
using models;
using store;

public class BaseUserRepository : AbstractD3ERepository<BaseUser>
{
    protected override int GetTypeIndex()
    {
        return SchemaConstants.BaseUser;
    }
    public List<BaseUser> GetByDevices(UserDevice devices)
    {
        String queryStr = "SELECT a._id from _base_user a where a._devices_id = :devices";
        Query query = Em().CreateNativeQuery(queryStr);
        QueryImplUtil.SetParameter(query, "devices", devices);
        return GetAllXsByY(query);
    }
}