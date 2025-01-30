using System;
using gqltosql;

namespace rest.ws
{
    public class ObjectTracker : IDisposable
    {
        private DataChangeTracker _dataChangeTracker;
        private ClientSession _session;
        private Field _field;
        private IDisposable _disposable;

        public ObjectTracker(DataChangeTracker dataChangeTracker, ClientSession session, Field field)
        {
            this._dataChangeTracker = dataChangeTracker;
            this._session = session;
            this._field = field;
        }

        public void Init(object one)
        {
            _disposable = _dataChangeTracker.Listen(one, _field, _session);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
