using Classes;
namespace store
{
    public class ValidationFailedException : Exception
    {
        private List<string> _errors;
        private MutateResultStatus _status;
        private bool _statusProvided = false;

        public static ValidationFailedException FromValidationContext(EntityValidationContext ctx)
        {
            // Assumed that ctx has errors
            var errors = ctx.GetErrors().Concat(ctx.GetThrowableErrors()).ToList();
            var e = new ValidationFailedException(errors);

            if (ctx.HasServerError())
            {
                e.SetStatus(MutateResultStatus.ServerError);
                ctx.MarkServerError(false);
            }

            ctx.ShowAllExceptions();
            return e;
        }

        public ValidationFailedException(List<string> errors) : base(errors.ToString())
        {
            _errors = errors;
        }

        public ValidationFailedException(MutateResultStatus status, List<string> errors) : base(errors.ToString())
        {
            _errors = errors;
            _status = status;
            _statusProvided = true;
        }

        public ValidationFailedException(Exception cause) : base("Internal Server Error", cause)
        {
            _errors = new List<string> { "Internal Server Error" };
        }

        public List<string> GetErrors()
        {
            return _errors ?? new List<string>();
        }

        public bool HasStatus()
        {
            return _statusProvided;
        }

        public MutateResultStatus GetStatus()
        {
            return _status;
        }

        public void SetStatus(MutateResultStatus status)
        {
            _status = status;
            _statusProvided = true;
        }
    }
}
