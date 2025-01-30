using System.Collections.ObjectModel;
using d3e.core;

namespace store
{
    public abstract class DatabaseObject : DBObject, ICloneable
    {
        protected long _Id;

        private static ILogger _logger;
        private static int MAX_SAVE_COUNT = 20;
        private static int OBJ_LOG_COUNT = 5;
        private bool _isDeleted;
        public bool _isImportingFromXml;
        private bool _isDirty;
        private bool _isNew = true;
        private bool _needsUpdate;
        private bool _isDeleteProcessed;
        protected bool _needSaveProcess = true;
        protected bool _needDeleteProcess = true;
        private int _onSaveCount;
        private bool _isGotId;
        protected DBSaveStatus saveStatus = DBSaveStatus.New;
        private bool _isInConvert;
        private bool _loaded;
        private string _docString;
        private bool _proxy;

        public long Id { get { return _Id; } }
        public bool IsOld { get => IsOld; set => IsOld = value; }
        public bool IsDeleted { get => _isDeleted; set => _isDeleted = value; }
        public bool IsInConvert { get => IsInConvert1; set => IsInConvert1 = value; }
        public bool IsInConvert1 { get => _isInConvert; set => _isInConvert = value; }

        protected object ToLogStr(Collection<object> col)
        {
            return col.Count;
        }

        protected object ToLogStr(DatabaseObject db)
        {
            if (db == null)
            {
                return null;
            }
            return db.Id;
        }

        public void SetId(long v)
        {
            this._Id = v;
            if (LocalId == 0)
            {
                LocalId = v;
            }
        }

        internal void VisitChildern(Func<object, object> value)
        {
            throw new NotImplementedException();
        }

        public void SetNeedDeleteProcess(bool needDeleteProcess)
        {
            this._needDeleteProcess = needDeleteProcess;
        }

        protected bool IsComponent()
        {
            return false;
        }

        public void SetNeedSaveProcess(bool needSaveProcess)
        {
            this._needSaveProcess = needSaveProcess;
        }

        public void SetDeletedProcessed(bool isDeleteProcessed)
        {
            this._isDeleteProcessed = isDeleteProcessed;
        }

        public bool IsDeleteProcessed()
        {
            return _isDeleteProcessed;
        }

        public void recordLog()
        {
        }
        public override bool Equals(object a)
        {
            bool isEquals = false;
            if ((a is DatabaseObject))
            {
                DatabaseObject aObj = (DatabaseObject)a;
                if ((!aObj.IsNew() && !this.IsNew()))
                {
                    isEquals = (aObj.Id == this.Id);
                }
                else
                {
                    isEquals = (aObj == this);
                }
            }
            return isEquals;
        }

        public DatabaseObject Clone()
        {
            return null;
        }

        public void CloneInstance(DatabaseObject clone)
        {
            clone.SetId(this.Id);
            clone.SetSaveStatus(this.GetSaveStatus());
        }

        public bool IsNew()
        {
            return Id == 0 || saveStatus == DBSaveStatus.New;
        }

        public void SetNew(bool isNew)
        {
            this._isNew = isNew;
        }

        public void VisitChildren(Action<DBObject> visitor)
        {
        }

        public void UpdateMasters(Action<DatabaseObject> visitor)
        {
            UpdateFlat(this);
        }

        private void UpdateFlat(DatabaseObject databaseObject)
        {
            throw new NotImplementedException();
        }

        public DatabaseObject _MasterObject()
        {
            return null;
        }

        public bool TransientModel()
        {
            return false;
        }

        public void CollectChildValues(CloneContext ctx)
        {

        }

        public void DeepCloneIntoObj(ICloneable cloned, CloneContext ctx)
        {
            DatabaseObject db = (DatabaseObject)cloned;
            db.SetId(Id);
            db.SetSaveStatus(this.GetSaveStatus());
        }

        public void recordOld(CloneContext ctx)
        {
            this.SetOld(ctx.GetFromCache(this));
        }

        public DatabaseObject GetOld()
        {
            return null;
        }

        protected bool CanMarkDirty()
        {
            return _loaded || !_IsEntity();
        }

        public bool CanCreateOldObject()
        {
            return NeedOldObject() && !(IsOld || IsNew() || GetOld() != null);
        }

        protected bool NeedOldObject()
        {
            return true;
        }

        public void CreateOldObject()
        {
            CloneContext ctx = new CloneContext(false, true);
            _CheckProxy();
            ctx.StartClone(this);
            recordOld(ctx);
        }
        public void PostLoad()
        {
            _loaded = true;
        }

        public virtual bool _Creatable()
        {
            return false;
        }

        public void _MarkDirty()
        {
            _isDirty = true;
            DatabaseObject master = _MasterObject();
            if (master != null)
            {
                master._MarkDirty();
            }
            if (saveStatus == DBSaveStatus.Saved)
            {
                saveStatus = DBSaveStatus.Changed;
            }
        }

        public void _ClearDirty()
        {
            _isDirty = false;
        }

        public bool _IsDirty()
        {
            return _isDirty;
        }
        protected override void OnPropertySet(bool inverse)
        {
            _MarkDirty();
            {
                DatabaseObject obj = this;
                do
                {
                    if (obj._Creatable())
                    {
                        Database.MarkDirty(obj, inverse);
                        break;
                    }
                    obj = obj._MasterObject();
                } while (obj != null);
            }
            if (CanCreateOldObject())
            {
                DatabaseObject obj = this;
                do
                {
                    if (obj._Creatable())
                    {
                        if (obj.CanCreateOldObject())
                        {
                            obj.CreateOldObject();
                        }
                        break;
                    }
                    obj = obj._MasterObject();
                } while (obj != null);
            }
            InformChangeToMaster(true);
        }

        public void SetOld(DatabaseObject old)
        {
        }
        public override void _ClearChanges()
        {
            base._ClearChanges();
            SetOld(null);
        }

        public void CollectCreatableReferences(List<object> _refs)
        {
        }

        public DBSaveStatus GetSaveStatus()
        {
            return this.saveStatus;
        }

        public void SetSaveStatus(DBSaveStatus ss)
        {
            this.saveStatus = ss;
        }

        public bool _IsEntity()
        {
            return false;
        }

        protected void _HandleChildChange(int _childIdx, bool set)
        {
        }

        protected void _CheckProxy()
        {
            if (this._proxy && !this.InProxy && saveStatus != DBSaveStatus.Deleted)
            {
                this._proxy = false;
                this.InProxy = true;
                Database.Get().Unproxy(this);
                this.InProxy = false;
            }
        }

        public void _MarkProxy()
        {
            this._proxy = true;
        }

        public void _ClearProxy()
        {
            this._proxy = false;
        }

        public bool _IsInProxy()
        {
            return this.InProxy;
        }

        public void _MarkInProxy()
        {
            this.InProxy = true;
        }

        public void _ClearInProxy()
        {
            this.InProxy = false;
        }

        public string GetIdent()
        {
            return _Type() + "-" + Id;
        }

        public void _SetDoc(string doc)
        {
            this._docString = doc;
        }

        public string _GetDoc()
        {
            return _docString;
        }

        public new string _Repo()
        {
            return "system";
        }

        public abstract ICloneable CreateNewInstance();
    }
}
