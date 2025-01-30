using System.Collections;
using gqltosql.schema;
using list;
using store;

namespace rest.ws
{
    public class ObjectsToSend
    {
        Dictionary<ClientSession, Dictionary<DBObject, Dictionary<DField<object, object>, BitArray>>> _addEmbeddedMap = new Dictionary<ClientSession, Dictionary<DBObject, Dictionary<DField<object, object>, BitArray>>>();
        Dictionary<ClientSession, Dictionary<DBObject, BitArray>> _addMap = new Dictionary<ClientSession, Dictionary<DBObject, BitArray>>();
        Dictionary<ClientSession, List<TypeAndId>> _delMap = new Dictionary<ClientSession, List<TypeAndId>>();

        public void AddEmbedded(ClientSession session, DBObject parent, DField<object, object> parentField, BitArray field)
        {
            if (field.Cast<bool>().All(b => !b)) // Check if the BitArray is empty
            {
                return;
            }
            // Log.info("Sending embedded " + parent + " and field " + field);
            if (!_addEmbeddedMap.TryGetValue(session, out var map2))
            {
                map2 = new Dictionary<DBObject, Dictionary<DField<object, object>, BitArray>>();
                _addEmbeddedMap[session] = map2;
            }
            if (!map2.TryGetValue(parent, out var byField))
            {
                byField = new Dictionary<DField<object, object>, BitArray>();
                map2[parent] = byField;
            }
            if (!byField.TryGetValue(parentField, out var set))
            {
                set = new BitArray(field.Length);
                byField[parentField] = set;
            }
            set.Or(field);
        }

        public void Add(ClientSession session, DBObject @object, BitArray field)
        {
            if (field.Cast<bool>().All(b => !b)) // Check if the BitArray is empty
            {
                return;
            }
            // Log.info("Sending " + @object + " and field " + field);
            if (!_addMap.TryGetValue(session, out var map2))
            {
                map2 = new Dictionary<DBObject, BitArray>();
                _addMap[session] = map2;
            }
            if (!map2.TryGetValue(@object, out var set))
            {
                set = new BitArray(field.Length);
                map2[@object] = set;
            }
            set.Or(field);
        }

        public void Delete(ClientSession session, TypeAndId typeId)
        {
            // Log.info("Sending Delete type: " + typeId.Type + " id: " + typeId.Id);
            if (!_delMap.TryGetValue(session, out var list))
            {
                list = new List<TypeAndId>();
                _delMap[session] = list;
            }
            list.Add(typeId);
        }

        public void Send(D3EWebsocket socket)
        {
            foreach (var session in _addMap.Keys)
            {
                var map2 = _addMap[session];
                if (map2.Count > 0)
                {
                    socket.SendChanges(session, map2);
                }
            }
            foreach (var session in _addEmbeddedMap.Keys)
            {
                var map2 = _addEmbeddedMap[session];
                socket.SendEmbeddedChanges(session, map2);
            }
            foreach (var session in _delMap.Keys)
            {
                var map2 = _delMap[session];
                socket.SendDelete(session, map2);
            }
        }
    }
}
