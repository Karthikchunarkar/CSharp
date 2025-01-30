
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using d3e.core;

namespace store
{
    public class EntityHelperService
    {
        private static EntityHelperService Instance;

        public Dictionary<string, EntityHelper<object>> EntityHelpers { get => EntityHelpers; set => EntityHelpers = value; }
        public Dictionary<string, ExternalSystem> ExternalSystems { get => ExternalSystems; set => ExternalSystems = value; }

        public static EntityHelperService GetInstance()
        {
            return Instance;
        }

        public EntityHelper<object> Get(string name)
        {
            return EntityHelpers[name];
        }

        public EntityHelper<object> GetByObject(object obj) 
        {
            return EntityHelpers[obj.GetType().Name];
        }

        public void Set(string name, EntityHelper<object> helper)
        {
            EntityHelpers[name] = helper;
        }

        public DBObject Get(string type, long id)
        {
            return (DBObject) EntityHelpers[type].GetById(id);
        }

        public ExternalSystem GetExternalSystem(string external) 
        {
           return ExternalSystems[external];
        }
    }
}
