namespace d3e.core
{
    public interface Criterion
    {
        string Select(Criteria criteria);

        string ToSql(Criteria criteria);
    }
}
