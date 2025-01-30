namespace store
{
    public interface EntityHelper<T>
    {
        void SetDefaults(T entity);

        void Compute(T entity);

        void ValidateOnCreate(T entity, EntityValidationContext context);

        void ValidateOnUpdate(T entity, EntityValidationContext context);

        bool OnCreate(T obj, bool local, EntityValidationContext context);

        bool OnUpdate(T obj, bool local, EntityValidationContext context);

        bool OnDelete(T obj, bool local, EntityValidationContext context);

        T Clone(T entity);

        T GetById(long input);

        void ValidateOnDelete(T entity, EntityValidationContext deletionContext)
        {

        }

        object? NewInstance()
        {
            return null;
        }

        T? GetOld(long id)
        {
            return default;
        }

        bool Union(Func<bool>[] providers)
        {
            foreach (var provider in providers)
            {
                if (provider())
                {
                    return true;
                }
            }
            return false;
        }

        bool Intersect(params Func<bool>[] providers)
        {
            foreach (var provider in providers)
            {
                if (!provider())
                {
                    return false;
                }
            }
            return true;
        }

        bool Exclude(bool from, bool what) => !what && from;

        public static bool HaveUnDeleted<E>(List<E> list) where E : DatabaseObject
        {
            return list.Any(x => !x.IsDeleted);
        }
    }
}
