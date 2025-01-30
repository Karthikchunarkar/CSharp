namespace gqltosql
{
    public interface IValue
    {
        object Read(object[] row, OutObject obj);
    }
}
