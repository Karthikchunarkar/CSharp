using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using d3e.core;
using models;

namespace store
{
    public class GeneralEntityMutator : EntityMutator
    {
        private readonly Dictionary<string, ExternalSystem> _repos;

        private class Context
        {
            public string Repo { get; set; }
            public HashSet<DatabaseObject> SaveQueue { get; set; } = new HashSet<DatabaseObject>();
            public HashSet<object> DeleteQueue { get; set; } = new HashSet<object>();
            public HashSet<DatabaseObject> DirtyQueue { get; set; } = new HashSet<DatabaseObject>();
            public Dictionary<DatabaseObject, DatabaseObject> Clones { get; } = new Dictionary<DatabaseObject, DatabaseObject>();
            public ValidationContextImpl ValidationContext { get; set; }
            public HashSet<DatabaseObject> ExternalObjects { get; set; } = new HashSet<DatabaseObject>();
            public List<DatabaseObject> PersistObjects { get; set; } = new List<DatabaseObject>();
            public List<DatabaseObject> RemoveObjects { get; set; } = new List<DatabaseObject>();
            public List<object> ActionsDone { get; set; } = new List<object>();
            public List<DFile> PersistFiles { get; set; } = new List<DFile>();
        }

        private static readonly ThreadLocal<Dictionary<string, Context>> _threadLocalMutator = new ThreadLocal<Dictionary<string, Context>>();

        public void MarkDirty(DatabaseObject obj, bool inverse)
        {
            if (!obj._IsEntity())
            {
                if (obj.GetId() == 0 || obj.IsOld)
                {
                    return;
                }
                var tm = TransactionManager.Get();
                if (tm != null && !obj.TransientModel())
                {
                    tm.Update(obj);
                }
                return;
            }
            if (obj.IsNew())
            {
                return;
            }
            if (inverse)
            {
                var tm = TransactionManager.Get();
                if (tm != null && !obj.TransientModel())
                {
                    tm.Update(obj);
                }
                return;
            }
            if (!_repos.TryGetValue(obj._Repo(), out var sys) || !sys.TrackDirty())
            {
                return;
            }
            var ctx = GetContext(obj._Repo());
            var master = FindMaster(obj);
            if (master == null || ctx.SaveQueue.Contains(master) || ctx.DeleteQueue.Contains(master))
            {
                return;
            }
            ctx.DirtyQueue.Add(master);
        }

        private CreatableObject FindMaster(DatabaseObject obj)
        {
            if (obj is CreatableObject creatable)
            {
                return creatable;
            }
            var masterObject = obj._MasterObject();
            return masterObject == null ? null : FindMaster(masterObject);
        }

        public void UnproxyDFile(DFile file, string repo)
        {
            _repos[repo].UnproxyDFile(file);
        }

        public void Unproxy(DatabaseObject obj)
        {
            _repos[obj._Repo()].Unproxy(obj);
        }

        public void UnproxyCollection(D3EPersistanceList<object> list)
        {
            _repos[list.Repo()].UnproxyCollection(list);
        }

        public void Save(DatabaseObject obj, bool internalCall)
        {
            SaveOrUpdate(obj, internalCall);
        }

        public void Update(DatabaseObject obj, bool internalCall)
        {
            SaveOrUpdate(obj, internalCall);
        }

        private void SaveOrUpdate(DatabaseObject obj, bool internalCall)
        {
            var created = CreateContextIfNotExist(obj._Repo());
            var ctx = GetContext(obj._Repo());
            if (ctx.DeleteQueue.Contains(obj))
            {
                return;
            }
            if (!internalCall)
            {
                ctx.ExternalObjects.Add(obj);
            }
            if (obj.IsInConvert || obj.IsDeleted)
            {
                return;
            }
            obj.UpdateMasters(_ => { });
            if (ctx.SaveQueue.Contains(obj))
            {
                return;
            }
            ctx.SaveQueue.Add(obj);
            ctx.DirtyQueue.Remove(obj);
            if (obj._IsEntity())
            {
                _repos[ctx.Repo].CreateId(obj);
                if (!ctx.PersistObjects.Contains(obj))
                {
                    ctx.PersistObjects.Add(obj);
                }
            }
            PreUpdate(obj);
            if (obj._IsEntity())
            {
                _repos[ctx.Repo].CreateId(obj);
            }
            if (!ctx.ActionsDone.Contains(obj))
            {
                ctx.ActionsDone.Add(obj);
            }
            var refs = new List<object>();
            obj.CollectCreatableReferences(refs);
            foreach (var o in refs.OfType<DatabaseObject>().Where(o => !ctx.SaveQueue.Contains(o) && IsActionDone(ctx, o)))
            {
                ctx.DirtyQueue.Add(o);
            }
            ctx.SaveQueue.Remove(obj);
            if (created)
            {
                Finish();
            }
        }

        private bool IsActionDone(Context ctx, DatabaseObject obj)
        {
            return obj.IsNew() && !ctx.ActionsDone.Contains(obj);
        }

        public void PreUpdate(DatabaseObject entity)
        {
            var helper = _repos[entity._Repo()] as EntityHelper<DatabaseObject>;
            if (helper != null)
            {
                var ctx = GetContext(entity._Repo());
                helper.SetDefaults(entity);
                helper.Compute(entity);
                if (IsActionDone(ctx, entity))
                {
                    helper.OnCreate(entity, ctx.ExternalObjects.Contains(entity), ctx.ValidationContext);
                }
                else
                {
                    if (entity.CanCreateOldObject())
                    {
                        entity.CreateOldObject();
                    }
                    helper.OnUpdate(entity, ctx.ExternalObjects.Contains(entity), ctx.ValidationContext);
                }
                try
                {
                    if (IsActionDone(ctx, entity))
                    {
                        helper.ValidateOnCreate(entity, ctx.ValidationContext);
                    }
                    else
                    {
                        helper.ValidateOnUpdate(entity, ctx.ValidationContext);
                    }
                }
                catch (Exception e)
                {
                    ctx.ValidationContext.MarkServerError(true);
                    ctx.ValidationContext.AddThrowableError(e, "Something went wrong.");
                }
                if (ctx.ValidationContext.HasErrors())
                {
                    throw ValidationFailedException.FromValidationContext(ctx.ValidationContext);
                }
            }
            var tm = TransactionManager.Get();
            if (entity.IsNew())
            {
                tm.Add(entity);
            }
            else
            {
                tm.Update(entity);
            }
        }

        public void PreDelete(DatabaseObject entity)
        {
            entity.IsDeleted = true;
            var helper = _repos[entity._Repo()] as EntityHelper<DatabaseObject>;
            var ctx = GetContext(entity._Repo());
            if (helper != null)
            {
                helper.ValidateOnDelete(entity, ctx.ValidationContext);
            }
            if (ctx.ValidationContext.HasErrors())
            {
                throw ValidationFailedException.FromValidationContext(ctx.ValidationContext);
            }
            TransactionManager.Get().Delete(entity);
            if (helper != null)
            {
                helper.OnDelete(entity, ctx.ExternalObjects.Contains(entity), ctx.ValidationContext);
            }
            if (ctx.ValidationContext.HasErrors())
            {
                throw ValidationFailedException.FromValidationContext(ctx.ValidationContext);
            }
        }

        public void Clear()
        {
            ClearContext();
        }

        private void ClearContext()
        {
            _threadLocalMutator.Value = null;
        }

        private void ClearContext(string repo)
        {
            var all = _threadLocalMutator.Value;
            if (all != null)
            {
                all.Remove(repo);
            }
        }

        private bool CreateContextIfNotExist(string repo)
        {
            var all = _threadLocalMutator.Value;
            if (all == null)
            {
                all = new Dictionary<string, Context>();
                _threadLocalMutator.Value = all;
            }
            if (all.ContainsKey(repo))
            {
                return false;
            }
            var ctx = new Context
            {
                Repo = repo,
                ValidationContext = new ValidationContextImpl(this, repo)
            };
            all[repo] = ctx;
            return true;
        }

        private Context GetContext(string repo)
        {
            var all = _threadLocalMutator.Value;
            if (all == null)
            {
                all = new Dictionary<string, Context>();
                _threadLocalMutator.Value = all;
            }
            if (all.TryGetValue(repo, out var context))
            {
                return context;
            }
            var ctx = new Context
            {
                Repo = repo,
                ValidationContext = new ValidationContextImpl(this, repo)
            };
            all[repo] = ctx;
            return ctx;
        }

        public void Finish()
        {
            try
            {
                var all = _threadLocalMutator.Value;
                if (all != null)
                {
                    foreach (var ctx in all.Values.ToList())
                    {
                        Finish(ctx);
                    }
                }
            }
            finally
            {
                ClearContext();
            }
        }

        private void Finish(Context ctx)
        {
            try
            {
                DoFinish(ctx);
            }
            finally
            {
                ClearContext(ctx.Repo);
            }
        }

        private bool DoFinish(Context ctx)
        {
            if (ctx.DirtyQueue.Count == 0)
            {
                var em = _repos[ctx.Repo].GetEntityManager();
                foreach (var file in ctx.PersistFiles)
                {
                    em.PersistFile(file);
                }
                foreach (var obj in ctx.PersistObjects)
                {
                    em.Persist(obj);
                    obj._SetDoc(null);
                }
                foreach (var obj in ctx.RemoveObjects)
                {
                    em.Delete(obj);
                }
                return true;
            }
            var dirtySet = new HashSet<DatabaseObject>(ctx.DirtyQueue);
            ctx.DirtyQueue.Clear();
            foreach (var obj in dirtySet)
            {
                SaveOrUpdate(obj, true);
            }
            return DoFinish(ctx);
        }

        public bool Delete(DatabaseObject obj, bool internalCall)
        {
            var created = CreateContextIfNotExist(obj._Repo());
            var ctx = GetContext(obj._Repo());
            if (ctx.DeleteQueue.Contains(obj))
            {
                return false;
            }
            ctx.DeleteQueue.Add(obj);
            var done = DeleteInternal(obj, internalCall);
            ctx.DeleteQueue.Remove(obj);
            if (created)
            {
                Finish(ctx);
            }
            return done;
        }

        private bool DeleteInternal(DatabaseObject entity, bool internalCall)
        {
            if (entity.IsDeleted)
            {
                return false;
            }
            var ctx = GetContext(entity._Repo());
            if (!internalCall)
            {
                ctx.ExternalObjects.Add(entity);
            }
            ctx.PersistObjects.Remove(entity);
            PreDelete(entity);
            if (entity._IsEntity() && !ctx.RemoveObjects.Contains(entity))
            {
                ctx.RemoveObjects.Add(entity);
            }
            return true;
        }

        public void PerformDeleteOrphan<T>(ICollection<T> oldList, ICollection<T> newList) where T : DatabaseObject
        {
            var deletedList = oldList.Except(newList).ToList();
            oldList.Clear();
            foreach (var item in newList)
            {
                oldList.Add(item);   
            }
            foreach (var t in deletedList)
            {
                Delete(t, true);
            }
        }

        public EntityHelper<T> GetHelperByInstance<T>(object fullType) where T : DatabaseObject
        {
            if (fullType is DBObject dbObject)
            {
                return _repos[dbObject._Repo()].GetHelperByInstance(fullType);
            }
            if (fullType is D3EImage)
            {
                return _repos["system"].GetHelper("D3EImage");
            }
            return null;
        }

        public void ProcessOnLoad(object entity, string repo)
        {
            var helper = _repos[repo].GetHelper<DatabaseObject, EntityHelper<T>>(entity.GetType().Name);
            var ctx = GetContext(repo);
            if (helper != null && !ctx.Clones.ContainsKey((DatabaseObject)entity))
            {
                var clone = helper.Clone((DatabaseObject) entity);
                ctx.Clones[(DatabaseObject)entity] = clone;
            }
        }

        public bool IsInDelete(object obj, string repo)
        {
            var ctx = GetContext(repo);
            return ctx.DeleteQueue.Contains(obj) || ctx.RemoveObjects.Contains(obj);
        }

        void EntityMutator.SaveOrUpdate(DatabaseObject obj, bool local)
        {
            throw new NotImplementedException();
        }

        public bool Delete<T>(T obj, bool local) where T : DatabaseObject
        {
            throw new NotImplementedException();
        }

        public void PeformDeleteOrphan<T>(ICollection<T> oldList, Collection<T> newList) where T : DatabaseObject
        {
            throw new NotImplementedException();
        }

        public H GetHelperByInstance<T, H>(object fullType)
            where T : DatabaseObject
            where H : EntityHelper<T>
        {
            throw new NotImplementedException();
        }
    }
}