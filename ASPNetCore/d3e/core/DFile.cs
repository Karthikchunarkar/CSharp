using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.sun.tools.doclint;
using java.beans;
using Microsoft.EntityFrameworkCore.Infrastructure;
using store;

namespace d3e.core
{
    [Table("_dfile")]
    public class DFile
    {
        private string Name;
        [Key]
        public string Id { get => Id; set => Id = value; }

        private long Size;

        //Transient
        private bool Proxy;

        //[EntityFrameworkInternal]
        private string Repo;

        private string MimeType;

        public long GetSize()
        {
            _CheckProxy();
            return Size;
        }

        public void SetSize(long Size)
        {
            this.Size = Size;
        }

        public string GetName()
        {
            return Name;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public string GetMimeType()
        {
            _CheckProxy();
            return MimeType;
        }

        public void SetMimeType(string mimeType)
        {
            this.MimeType = mimeType;
        }

        public void _MarkProxy(string repo)
        {
            this.Proxy = true;
            this.Repo = repo;
        }



        private void _CheckProxy()
        {
            if (Proxy) 
            {
                Database.Get().UnProxyDfile(this, Repo);
                this.Proxy = false;
            }
        }

        public string GetDownloadUrl()
        {
            return Env.Get().GetBaseHttpUrl() + "/api/download/" + this.Id;
        }
    }
}
