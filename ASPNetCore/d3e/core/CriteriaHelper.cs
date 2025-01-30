using gqltosql.schema;
using store;

namespace d3e.core
{
    public class CriteriaHelper
    {
        private IModelSchema Schema {  get => Schema; }

        public D3EEntityManagerProvider Provider { get => Provider; }

        private D3EQueryBuilder Builder { get => Builder; }

        private static CriteriaHelper? INS;

        [javax.annotation.PostConstruct]
        public void init()
        {
            INS = this;
        }

        public static CriteriaHelper? get()
        {
            return INS;
        }
    }
}
