namespace store
{
    public class ValidationContextImpl : EntityValidationContext
    {
        private readonly List<Exception> _exceptions = new List<Exception>();
        private readonly List<string> _errors = new List<string>();
        private readonly List<string> _throwableErrors = new List<string>();
        private bool _serverError;
        private readonly EntityMutator _mutator;
        private readonly string _repo;

        public ValidationContextImpl(EntityMutator mutator, string repo)
        {
            _mutator = mutator;
            _repo = repo;
        }

        public bool HasErrors()
        {
            return _errors.Count > 0 || _throwableErrors.Count > 0;
        }

        public List<string> GetErrors()
        {
            return _errors;
        }

        public List<string> GetThrowableErrors()
        {
            return _throwableErrors;
        }

        public void AddFieldError(string field, string error)
        {
            _errors.Add($"{field}: {error}");
        }

        public void AddEntityError(string error)
        {
            _errors.Add(error);
        }

        public void AddThrowableError(Exception t, string error)
        {
            _exceptions.Add(t);
            AddEntityError(error);
        }

        public EntityValidationContext Child(string field, string identity, long index)
        {
            return this;
        }

        public void MarkServerError(bool value)
        {
            _serverError = value;
        }

        public bool HasServerError()
        {
            return _serverError;
        }

        public void ShowAllExceptions()
        {
            foreach (var exception in _exceptions)
            {
                Console.Error.WriteLine(exception);
            }
            // TODO: Do we clear exceptions?
            _exceptions.Clear();
        }

        public bool IsInDelete(object obj)
        {
            if (_mutator == null)
            {
                return false;
            }
            return _mutator.IsInDelete(obj, _repo);
        }
    }
}
