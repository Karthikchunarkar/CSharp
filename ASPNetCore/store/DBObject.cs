
using System.ComponentModel.DataAnnotations.Schema;

namespace store
{
    public abstract class DBObject
    {
        [NotMapped]
        protected long LocalId { get; set; }

        [NotMapped]
        private DBChange _changes;

        [NotMapped]
        protected int ChildIdx { get; set; } = -1;

        [NotMapped]
        protected bool InProxy { get; set; }

        protected DBObject()
        {
            _changes = new DBChange(_FieldsCount(), OnPropertySet, OnPropertyUnset);
        }

        public void SetLocalId(long localId)
        {
            LocalId = localId;
        }

        public long GetLocalId()
        {
            return LocalId;
        }

        protected abstract int _FieldsCount();

        public virtual long GetId()
        {
            return 0;
        }

        public virtual void SetId(long id)
        {
        }

        public abstract int _TypeIdx();

        public abstract string _Type();

        public DBChange _Changes()
        {
            return _changes;
        }

        public virtual DatabaseObject _MasterObject()
        {
            return null;
        }

        protected void _CheckProxy()
        {
            var master = _MasterObject();
            if (master != null)
            {
                master._CheckProxy();
            }
        }

        public virtual bool IsOld()
        {
            return false;
        }

        public void FieldChanged(int field, object oldValue, object newValue)
        {
            if (InProxy || IsOld())
            {
                return;
            }
            _changes.OnFieldChange(field, oldValue, newValue);
        }

        public void CollFieldChanged(int field, object oldValue, object newValue)
        {
            if (InProxy || IsOld())
            {
                return;
            }
            _changes.OnCollFieldChange(field, oldValue, newValue);
        }

        public void InvCollFieldChanged(int field, object oldValue)
        {
            if (InProxy || IsOld())
            {
                return;
            }
            _changes.OnInvCollFieldChange(field, oldValue);
        }

        public void ChildFieldChanged(int field, bool set)
        {
            if (InProxy || IsOld())
            {
                return;
            }
            _changes.OnChildFieldChange(field, set);
        }

        public void ChildCollFieldChanged(int field, bool set, List<object> list)
        {
            if (InProxy || IsOld())
            {
                return;
            }
            _changes.OnChildCollFieldChange(field, set, list);
        }

        public virtual void _ClearChanges()
        {
            _changes = new DBChange(_FieldsCount(), OnPropertySet, OnPropertyUnset);
        }

        protected virtual void OnPropertySet(bool inverse)
        {
            InformChangeToMaster(true);
        }

        protected void OnPropertyUnset()
        {
            InformChangeToMaster(false);
        }

        protected void InformChangeToMaster(bool set)
        {
            var master = _MasterObject();
            if (master == null)
            {
                return;
            }
            master.HandleChildChange(ChildIdx, set);
            master.InformChangeToMaster(set);
        }

        public void _SetChildIdx(int childIdx)
        {
            ChildIdx = childIdx;
        }

        public void _UpdateChanges()
        {
            if (!_Changes().IsEmpty())
            {
                OnPropertySet(false);
            }
        }

        public void _ClearChildIdx()
        {
            ChildIdx = -1;
        }

        public virtual string DisplayName()
        {
            return null;
        }

        public void _SetMasterObject(DBObject master)
        {
        }

        public virtual string _Repo()
        {
            return "system";
        }

        protected virtual void HandleChildChange(int childIdx, bool set)
        {
        }
    }
}
