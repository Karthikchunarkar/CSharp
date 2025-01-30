using d3e.core;
using store;

namespace rest
{
    public interface IDocumentReader
    {
        // Read methods
        T ReadEnum<T>(string field) where T : struct, Enum;
        T ReadEmbedded<T>(string field, string type, T exists);
        List<long> ReadLongColl(string field);
        List<string> ReadStringColl(string field);
        List<T> ReadChildColl<T>(string field, string type);
        List<T> ReadUnionColl<T>(string field, string type);
        List<T> ReadEnumColl<T>(string field) where T :  struct, Enum;
        T ReadChild<T>(string field, string type);
        T ReadUnion<T>(string field, string type);
        long ReadLong(string field);
        string ReadString(string field);
        bool Has(string field);
        long ReadInteger(string field);
        double ReadDouble(string field);
        bool ReadBoolean(string field);
        TimeSpan ReadDuration(string field);
        DateTime ReadDateTime(string field);
        TimeOnly ReadTime(string field);
        DateOnly ReadDate(string field);

        // Write methods
        void WriteEnum<T>(string field, T val, T def) where T :  Enum;
        void WriteEmbedded<T>(string field, T exists);
        void WriteLongColl(string field, List<long> coll);
        void WriteStringColl(string field, List<string> coll);
        void WriteChildColl<T>(string field, List<T> coll, string type) where T : DatabaseObject;
        void WriteUnionColl<T>(string field, List<T> coll) where T : DatabaseObject;
        void WriteEnumColl<T>(string field, List<T> coll) where T :  Enum;
        void WriteRefColl<T>(string field, List<T> coll, string type) where T : DatabaseObject;
        void WriteChild<T>(string field, T obj, string type) where T : DatabaseObject;
        void WriteUnion<T>(string field, T obj) where T : DatabaseObject;
        void WriteRef<T>(string field, T obj, string type) where T : DatabaseObject;
        void WriteLong(string field, long val, long def);
        void WriteString(string field, string val, string def);
        void WriteInteger(string field, long val, long def);
        void WriteDouble(string field, double val, double def);
        void WriteBoolean(string field, bool val, bool def);
        void WriteDuration(string field, TimeSpan val);
        void WriteDateTime(string field, DateTime val);
        void WriteTime(string field, TimeOnly val);
        void WriteDate(string field, DateOnly val);
        void WriteDFile(string field, DFile val);
    }
}
