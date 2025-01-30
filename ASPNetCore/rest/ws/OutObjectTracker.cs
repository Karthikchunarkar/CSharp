using System;
using gqltosql;

namespace rest.ws
{
    public class OutObjectTracker : IDisposable
    {

        private ClientSession _session;
        private Field _field;
        private OutObject _object;
        private DataChangeTracker _dataChangeTracker;
        private IDisposable _disposable;

        public OutObjectTracker(DataChangeTracker dataChangeTracker, ClientSession session, Field field)
        {
            this._dataChangeTracker = dataChangeTracker;
            this._session = session;
            this._field = field;
        }

        public void Init(OutObject value)
        {
            if (value != null)
            {
                _disposable = _dataChangeTracker.Listen(value, _field, _session);
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
