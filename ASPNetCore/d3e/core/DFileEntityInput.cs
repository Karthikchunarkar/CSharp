namespace d3e.core
{
    public class DFileEntityInput
    {
        private string Id { get => Id; set => Id = value; }
        private string Name { get => Name; set => Name = value; }
        private long Size { get => Size; set => Size = value; }

        public string _type()
        {
            return "DFile";
        }
    }
}
