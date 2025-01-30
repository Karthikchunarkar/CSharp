using System.Collections;

namespace rest.ws
{
    public class Key
    {
        readonly int type;
        readonly BitArray fields;

        public Key(int type, BitArray fields)
        {
            this.type = type;
            this.fields = fields;
        }
        public override bool Equals(object obj)
        {
            if (obj is Key)
            {
                Key other = (Key)obj;
                return other.type == type && object.Equals(other.fields, this.fields);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return type + fields.GetHashCode();
        }
    }
}
