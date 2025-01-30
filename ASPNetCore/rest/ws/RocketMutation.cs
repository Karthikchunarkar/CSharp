using System.ComponentModel.DataAnnotations;
using d3e.core;
using gqltosql.schema;
using models;
using store;

namespace rest.ws
{
    public class RocketMutation
    {
        private EntityMutator _mutator;
        private Dictionary<string, ExternalSystem> _externalSystems;
        private IModelSchema _schema;

        public void Save(CreatableObject obj)
        {
            bool create = obj.IsNew();
            BaseUser currentUser = CurrentUser.Get();
            int objType = obj._TypeIdx();

            if (create && !PermissionCheckUtil.CanCreate(currentUser._TypeIdx(), obj._TypeIdx()))
            {
                throw AuthFail("Current user type does not have create permissions for this model.");
            }
            if (!create && !PermissionCheckUtil.canUpdate(currentUser._TypeIdx(), obj._TypeIdx()))
            {
                throw AuthFail("Current user type does not have create permissions for this model.");
            }
            DModel<object> dModel = _schema.GetType(objType);
            if (!dModel.IsExternal())
            {
                this._mutator.Save(obj, false);
            }
            else
            {
                ExternalSystem externalSystem = this._externalSystems.Get(dModel.GetExternal());
                externalSystem.Save(obj, false);
            }
        }

        private ValidationException AuthFail(String msg)
        {
            return new ValidationException(msg);
        }

        public void Delete(CreatableObject obj, bool local)
        {
            BaseUser currentUser = CurrentUser.Get();
            int objType = obj._typeIdx();

            if (!PermissionCheckUtil.canDelete(currentUser._typeIdx(), obj._TypeIdx()))
            {
                throw AuthFail("Current user type does not have delete permissions for this model.");
            }
            DModel<object> dModel = _schema.GetType(objType);
            if (!dModel.TsExternal())
            {

                this._mutator.Delete(obj, false);
            }
            else
            {
                ExternalSystem externalSystem = this._externalSystems.Get(dModel.GetExternal());
                externalSystem.Delete(obj, false);
            }
        }
    }
}
