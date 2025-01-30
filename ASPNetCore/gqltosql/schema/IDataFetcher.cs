namespace gqltosql.schema
{
    public interface IDataFetcher
    {
        object OnPrimitiveValue<T, R>(object value, DField<T, R> df);
        object OnReferenceValue(object value);
        object OnEmbeddedValue(object value);
        object OnPrimitiveList<T, R>(List<R> value, DField<T, List<R>> df);
        object OnReferenceList<R>(List<R> value);
        object OnFlatValue<R>(List<R> value);
        object OnInverseValue<R>(List<R> value);
    }
}
