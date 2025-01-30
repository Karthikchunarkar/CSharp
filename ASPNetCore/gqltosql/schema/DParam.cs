namespace gqltosql.schema
{
    public class DParam
    {
        private int _type;
        private bool _collection;

        public DParam(int type) : this(type, false)
        {
            
        }

        public DParam(int type , bool colllection)
        {
            this._type = type;
            this._collection = colllection;
        }

        public int Type 
        {
            get => _type;
            set => _type = value;
        }

        public bool Collection
        { 
            get => this._collection;
            set => this._collection = value;
        }
    }
}
