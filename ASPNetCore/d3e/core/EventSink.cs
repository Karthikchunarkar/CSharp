namespace d3e.core
{
    public class EventSink<T>
    {
        private readonly IObserver<T> _emittor;

        public EventSink(IObserver<T> emittor)
        {
            _emittor = emittor;
        }

        public void Add(T eventItem)
        {
            _emittor.OnNext(eventItem);
        }

        public void AddError(object error)
        {
            if(error is Exception err)
            {
                _emittor.OnError(err);
            }
            else
            {
                _emittor.OnError(new ErrorObjectException(error));
            }
        }

        public void Close()
        {
            _emittor.OnCompleted();
        }
    }
}
