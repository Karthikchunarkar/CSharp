
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Classes;
using d3e.core;

namespace store
{
    public class Database
    {
        private static Database _instance;

        private readonly EntityMutator _mutator;
        private readonly D3ETempResourceHandler _saveHandler;

        public void Init()
        {
            _instance = this;
        }

        public static Database Get()
        {
            return _instance;
        }

        public void Save(object obj)
        {
            if (!(obj is DatabaseObject dbObj))
            {
                return;
            }
            _mutator.Save(dbObj, true);
        }

        public void SaveAll(List<object> objects)
        {
            foreach (var obj in objects)
            {
                Save(obj);
            }
        }

        public void Update(object obj)
        {
            if (!(obj is DatabaseObject dbObj))
            {
                return;
            }
            _mutator.Update(dbObj, true);
        }

        public void UpdateAll(List<object> objects)
        {
            foreach (var obj in objects)
            {
                Update(obj);
            }
        }

        public void Delete(object obj)
        {
            if (!(obj is DatabaseObject dbObj))
            {
                return;
            }
            _mutator.Delete(dbObj, true);
        }

        public void DeleteAll(List<object> objects)
        {
            foreach (var obj in objects)
            {
                Delete(obj);
            }
        }

        public void PreUpdate(DatabaseObject obj)
        {
            _mutator.PreUpdate(obj);
        }

        public void PreDelete(DatabaseObject obj)
        {
            _mutator.PreDelete(obj);
        }

        public static void MarkDirty(DatabaseObject obj, bool inverse)
        {
            var database = Get();
            if (database == null)
            {
                return;
            }
            var mutator = database._mutator;
            if (mutator == null)
            {
                return;
            }
            mutator.MarkDirty(obj, inverse);
        }

        public static void CollectCreatableReferences(List<object> refs, DatabaseObject obj)
        {
            if (obj != null)
            {
                obj.CollectCreatableReferences(refs);
            }
        }

        public static void CollectCollectionCreatableReferences(List<object> refs, List<DatabaseObject> coll)
        {
            foreach (var o in coll)
            {
                CollectCreatableReferences(refs, o);
            }
        }

        public void Unproxy(DatabaseObject obj)
        {
            _mutator.Unproxy(obj);
        }

        public void UnproxyCollection(D3EPersistanceList<object> list)
        {
            _mutator.UnproxyCollection(list);
        }

        public void UnproxyDFile(DFile file, string repo)
        {
            _mutator.UnproxyDFile(file, repo);
        }

        public DFile CreateFileWithContent(string name, string content)
        {
            // Convert the content to a byte array
            var bytes = Encoding.UTF8.GetBytes(content);

            // Create a temporary file and write the content to it
            var tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, bytes);

            // Create a FileInfo object for the temporary file
            var fileInfo = new FileInfo(tempFilePath);

            // Save the file using the Save method
            var file = _saveHandler.Save(fileInfo, name);

            File.Delete(tempFilePath);
            return file;
        }

        public bool IsDeleting(object obj)
        {
            if (!(obj is DatabaseObject dbObj))
            {
                return false;
            }
            return _mutator.IsInDelete(dbObj, dbObj._Repo());
        }
    }
}
