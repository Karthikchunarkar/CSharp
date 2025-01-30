namespace store
{
    public interface EntityValidationContext
    {
        bool HasErrors();

        // TODO: Change return type as per further requirement
        List<string> GetErrors();

        List<string> GetThrowableErrors()
        {
            return new List<string>();
        }

        void AddFieldError(string field, string error);

        void AddEntityError(string error);

        void AddThrowableError(Exception t, string error)
        {
            // Default implementation does nothing
        }

        EntityValidationContext Child(string field, string identity, long index);

        void MarkServerError(bool value)
        {
            // Default implementation does nothing
        }

        bool HasServerError()
        {
            return false;
        }

        void ShowAllExceptions()
        {
            // Default implementation does nothing
        }

        bool IsInDelete(object obj)
        {
            return false;
        }
    }
}
