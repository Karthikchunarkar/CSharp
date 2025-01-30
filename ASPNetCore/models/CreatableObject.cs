using store;

namespace models
{
    public abstract class CreatableObject : DatabaseObject
    {

        public override bool _Creatable()
        {
            return true;
        }
    }
}
