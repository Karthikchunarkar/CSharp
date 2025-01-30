namespace d3e.core
{
    public class ImageDimension
    {
        public int Width { get => Width; set => Width = value; }
        public int Height { get => Height; set => Height = value; }

        public ImageDimension()
        {

        }

        public ImageDimension(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
}
