using Microsoft.Extensions.FileProviders;

namespace d3e.core
{
    public interface D3EResourceHandler
    {
        IFileInfo Get(string name);

        FileInfo GetFile(string name);

        DFile Save(DFile file);

        void Persist(DFile file, IFileInfo resource);

        DFile SaveImage(DFile file, List<ImageDimension> resizes);

        bool IsSaved(DFile file);

    }
}
