namespace store
{
    public interface Query
    {


        // Returns a list of objects of type T based on the provided type.
        List<T> GetObjectResultList<T>(int type);

        // Returns the first object of type T based on the provided type.
        T GetObjectFirstResult<T>(int type);

        // Executes the query and returns a list of raw results.
        List<object> GetResultList();

        // Executes the query and returns a single result.
        object GetSingleResult();

        // Executes an update query and returns the number of affected rows.
        int ExecuteUpdate();

        // Sets a named parameter value.
        Query SetParameter(string name, object value);
    }
}
