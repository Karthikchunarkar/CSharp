using d3e.core;

namespace store
{
    public interface ICloneable
    {
        public void CollectChildValues(CloneContext ctx) { }

        public void DeepCloneIntoObj(ICloneable cloned, CloneContext ctx) { }

        public ICloneable CreateNewInstance();
    }
}
