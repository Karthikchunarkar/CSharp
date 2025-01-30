namespace list
{
    public interface ChangesConsumer
    {
        public void WriteListChange(ListChange change);

        public void WriteObjectChange(ObjectChange change);
    }
}
