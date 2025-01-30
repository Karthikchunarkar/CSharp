using System.Text;
using Classes;
using d3e.core;
using gqltosql.schema;

namespace rest.ws
{
    public class RocketMessage
    {
        private const int MAX_SIZE = 1024;
        private byte[] inBuffer;
        private int inPosition;
        private RocketSender sender;
        private byte[] outBuffer;
        private int outPosition;
        private int msgId = 0;
        private bool system;
        private int reqId;
        private long time;

        public byte[] InBuffer { get => inBuffer; set => inBuffer = value; }
        public int InPosition { get => inPosition; set => inPosition = value; }
        public RocketSender Sender { get => sender; set => sender = value; }
        public byte[] OutBuffer { get => outBuffer; set => outBuffer = value; }
        public int OutPosition { get => outPosition; set => outPosition = value; }
        public int MsgId { get => msgId; set => msgId = value; }
        public bool System { get => system; set => system = value; }
        public int ReqId { get => reqId; set => reqId = value; }
        public long Time { get => time; set => time = value; }

        public RocketMessage(byte[] inBuffer, RocketSender sender, bool system)
        {
            this.sender = sender;
            this.inBuffer = inBuffer;
            this.inPosition = 0;
            this.outBuffer = new byte[MAX_SIZE];
            this.outPosition = 0;
            this.system = system;
        }

        public void SetReqId(int reqId)
        {
            this.reqId = reqId;
        }

        public int GetReqId()
        {
            return reqId;
        }

        public byte ReadByte()
        {
            byte b = inBuffer[inPosition++];
            // Log.info("r byte: " + b);
            return b;
        }

        public string ReadString()
        {
            int size = ReadInt();
            if (size == -1)
            {
                // Log.info("r str: null");
                return null;
            }
            string s = Encoding.UTF8.GetString(inBuffer, inPosition, size);
            inPosition += size;
            // Log.info("r str: " + s);
            return s;
        }

        public List<string> ReadStringList()
        {
            int size = ReadInt();
            // Log.info("r str list: " + size);
            var res = new List<string>();
            for (int i = 0; i < size; i++)
            {
                res.Add(ReadString());
            }
            return res;
        }

        public List<int> ReadIntegerList()
        {
            int size = ReadInt();
            // D3ELogger.info("r int list: " + size);
            var res = new List<int>();
            for (int i = 0; i < size; i++)
            {
                res.Add(ReadInt());
            }
            return res;
        }

        public bool ReadBoolean()
        {
            bool b = inBuffer[inPosition++] == 1;
            // Log.info("r bool: " + b);
            return b;
        }

        public long ReadLong()
        {
            long l = DecodeZigZag64(ReadVarLong());
            // Log.info("r long: " + l);
            return l;
        }

        public int ReadInt()
        {
            return (int)ReadLong();
        }

        private long DecodeZigZag64(long value)
        {
            return ((value & 1) == 1 ? -(value >> 1) - 1 : (value >> 1));
        }

        private long EncodeZigZag64(long value)
        {
            return (value << 1) ^ (value >> 63);
        }

        private long ReadVarLong()
        {
            return ReadVarint64SlowPath();
        }

        private long ReadVarint64SlowPath()
        {
            long result = 0;
            for (int shift = 0; shift < 64; shift += 7)
            {
                byte b = inBuffer[inPosition++];
                result |= (long)(b & 0x7F) << shift;
                if ((b & 0x80) == 0)
                {
                    return result;
                }
            }
            throw new Exception("Malformed int");
        }

        public double ReadDouble()
        {
            double d = BitConverter.ToDouble(inBuffer, inPosition);
            inPosition += 8;
            // Log.info("r double: " + d);
            return d;
        }

        private long _FromInts(long hi, long lo)
        {
            return (hi << 32) | lo;
        }

        public void WriteByte(int val)
        {
            // Log.info("w byte: " + val);
            Put((byte)val);
        }

        public void WriteInt(int val)
        {
            WriteLong(val);
        }

        private static byte ComputeUInt64SizeNoTag(long value)
        {
            if ((value & (~0L << 7)) == 0L) return 1;
            if (value < 0L) return 10;
            byte n = 2;
            if ((value & (~0L << 35)) != 0L) { n += 4; value >>= 28; }
            if ((value & (~0L << 21)) != 0L) { n += 2; value >>= 14; }
            if ((value & (~0L << 14)) != 0L) n += 1;
            return n;
        }

        private void WriteVarLong(long value)
        {
            switch (ComputeUInt64SizeNoTag(value))
            {
                case 1: WriteVarint64OneByte(value); break;
                case 2: WriteVarint64TwoBytes(value); break;
                case 3: WriteVarint64ThreeBytes(value); break;
                case 4: WriteVarint64FourBytes(value); break;
                case 5: WriteVarint64FiveBytes(value); break;
                case 6: WriteVarint64SixBytes(value); break;
                case 7: WriteVarint64SevenBytes(value); break;
                case 8: WriteVarint64EightBytes(value); break;
                case 9: WriteVarint64NineBytes(value); break;
                case 10: WriteVarint64TenBytes(value); break;
            }
        }

        private void WriteVarint64OneByte(long value) => Put((byte)value);
        private void WriteVarint64TwoBytes(long value) { Put((byte)((value & 0x7F) | 0x80)); Put((byte)(value >> 7)); }
        private void WriteVarint64ThreeBytes(long value) { Put((byte)((value & 0x7F) | 0x80)); Put((byte)((value >> 7) & 0x7F | 0x80)); Put((byte)(value >> 14)); }
        private void WriteVarint64FourBytes(long value) { Put((byte)((value & 0x7F) | 0x80)); Put((byte)((value >> 7) & 0x7F | 0x80)); Put((byte)((value >> 14) & 0x7F | 0x80)); Put((byte)(value >> 21)); }
        private void WriteVarint64FiveBytes(long value) { Put((byte)((value & 0x7F) | 0x80)); Put((byte)((value >> 7) & 0x7F | 0x80)); Put((byte)((value >> 14) & 0x7F | 0x80)); Put((byte)((value >> 21) & 0x7F | 0x80)); Put((byte)(value >> 28)); }
        private void WriteVarint64SixBytes(long value) { Put((byte)((value & 0x7F) | 0x80)); Put((byte)((value >> 7) & 0x7F | 0x80)); Put((byte)((value >> 14) & 0x7F | 0x80)); Put((byte)((value >> 21) & 0x7F | 0x80)); Put((byte)((value >> 28) & 0x7F | 0x80)); Put((byte)(value >> 35)); }
        private void WriteVarint64SevenBytes(long value) { Put((byte)((value & 0x7F) | 0x80)); Put((byte)((value >> 7) & 0x7F | 0x80)); Put((byte)((value >> 14) & 0x7F | 0x80)); Put((byte)((value >> 21) & 0x7F | 0x80)); Put((byte)((value >> 28) & 0x7F | 0x80)); Put((byte)((value >> 35) & 0x7F | 0x80)); Put((byte)(value >> 42)); }
        private void WriteVarint64EightBytes(long value) { Put((byte)((value & 0x7F) | 0x80)); Put((byte)((value >> 7) & 0x7F | 0x80)); Put((byte)((value >> 14) & 0x7F | 0x80)); Put((byte)((value >> 21) & 0x7F | 0x80)); Put((byte)((value >> 28) & 0x7F | 0x80)); Put((byte)((value >> 35) & 0x7F | 0x80)); Put((byte)((value >> 42) & 0x7F | 0x80)); Put((byte)(value >> 49)); }
        private void WriteVarint64NineBytes(long value) { Put((byte)((value & 0x7F) | 0x80)); Put((byte)((value >> 7) & 0x7F | 0x80)); Put((byte)((value >> 14) & 0x7F | 0x80)); Put((byte)((value >> 21) & 0x7F | 0x80)); Put((byte)((value >> 28) & 0x7F | 0x80)); Put((byte)((value >> 35) & 0x7F | 0x80)); Put((byte)((value >> 42) & 0x7F | 0x80)); Put((byte)((value >> 49) & 0x7F | 0x80)); Put((byte)(value >> 56)); }
        private void WriteVarint64TenBytes(long value) { Put((byte)((value & 0x7F) | 0x80)); Put((byte)((value >> 7) & 0x7F | 0x80)); Put((byte)((value >> 14) & 0x7F | 0x80)); Put((byte)((value >> 21) & 0x7F | 0x80)); Put((byte)((value >> 28) & 0x7F | 0x80)); Put((byte)((value >> 35) & 0x7F | 0x80)); Put((byte)((value >> 42) & 0x7F | 0x80)); Put((byte)((value >> 49) & 0x7F | 0x80)); Put((byte)((value >> 56) & 0x7F | 0x80)); Put((byte)(value >> 63)); }

        public void WriteStringList(List<string> list)
        {
            // Log.info("w str list: " + list.Count);
            WriteList(list, WriteString);
        }

        public void WriteString(string val)
        {
            if (val == null)
            {
                WriteInt(-1);
                return;
            }
            byte[] bytes = Encoding.UTF8.GetBytes(val);
            WriteInt(bytes.Length);
            foreach (byte b in bytes)
            {
                Put(b);
            }
        }

        public void WriteLong(long val)
        {
            // Log.info("w long: " + val);
            WriteVarLong(EncodeZigZag64(val));
        }

        public void WriteLongList(List<long> val)
        {
            WriteList(val, WriteLong);
        }

        public void WriteIntegerList(List<int> ints)
        {
            // Log.info("w int list: " + ints.Count);
            WriteList(ints, WriteInt);
        }

        public void WriteGeolocation(Geolocation loc)
        {
            WriteInt(0);
            WriteDouble(loc.Latitude);
            WriteDouble(loc.Longitude);
        }

        public void WriteBlob(Blob blob)
        {
            if (blob == null)
            {
                WriteInt(-1);
                return;
            }
            WriteInt(0); // Write the flag first
            byte[] arr = blob.Bytes();
            WriteInt(arr.Length);
            foreach (byte b in arr)
            {
                WriteByte(b);
            }
        }

        public void WriteBlobList(List<Blob> list)
        {
            WriteList(list, WriteBlob);
        }

        private void WriteList<T>(List<T> list, Action<T> writeSingle)
        {
            if (list == null)
            {
                WriteInt(-1);
                return;
            }
            WriteInt(list.Count);
            foreach (var x in list)
            {
                writeSingle(x);
            }
        }

        private void Put(byte b)
        {
            if (outPosition < outBuffer.Length)
            {
                outBuffer[outPosition++] = b;
            }
            else
            {
                Flush(false);
                Put(b);
            }
        }

        public void Flush()
        {
            Flush(true);
        }

        private void Flush(bool isLast)
        {
            if (outPosition == 0) return; // Nothing to flush
            try
            {
                byte[] data = new byte[outPosition];
                Array.Copy(outBuffer, data, outPosition);
                outPosition = 0;
                sender.SendMessage(data, msgId, system);
                if (isLast) msgId = 0; else msgId++;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        public void Forward()
        {
            int limit = inBuffer.Length;
            inPosition = 0;
            msgId = 0;
            while (limit > MAX_SIZE)
            {
                try
                {
                    byte[] data = new byte[MAX_SIZE];
                    Array.Copy(inBuffer, inPosition, data, 0, MAX_SIZE);
                    inPosition += MAX_SIZE;
                    sender.SendMessage(data, msgId, system);
                    msgId++;
                    limit -= MAX_SIZE;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
            try
            {
                byte[] data = new byte[limit];
                Array.Copy(inBuffer, inPosition, data, 0, limit);
                sender.SendMessage(data, msgId, system);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        public void WriteNull()
        {
            // Log.info("w null: ");
            WriteInt(-1);
        }

        public void WriteBoolean(bool val)
        {
            // Log.info("w bool: " + val);
            Put((byte)(val ? 1 : 0));
        }

        public void WriteBooleanList(List<bool> list)
        {
            WriteList(list, WriteBoolean);
        }

        public void WriteDouble(double val)
        {
            // Log.info("w double: " + val);
            byte[] bytes = new byte[8];
            BitConverter.GetBytes(val).CopyTo(bytes, 0);
            foreach (byte b in bytes)
            {
                Put(b);
            }
        }

        public void WriteDoubleList(List<double> list)
        {
            WriteList(list, WriteDouble);
        }

        public void WriteDateTime(DateTime val)
        {
            // Log.info("w date time: " + val);
            if (val == null)
            {
                WriteInt(-1);
                return;
            }
            WriteString(val.ToString("o")); // ISO 8601 format
        }

        public void WriteDFile(DFile value)
        {
            if (value == null)
            {
                WriteString(null);
                return;
            }
            WriteString(value.Id);
            WriteString(value.GetName());
            WriteLong(value.GetSize());
            WriteString(value.GetMimeType());
        }

        public void WritePrimitiveField(object val, DField<object, object> field, Template template)
        {
            // Log.info("w " + field.PrimitiveType + " : " + field.Name);
            if (val == null)
            {
                WriteNull();
                return;
            }
            switch (field.GetPrimitiveType())
            {
                case FieldPrimitiveType.Boolean:
                    WriteBoolean((bool)val);
                    break;
                case FieldPrimitiveType.DFile:
                    WriteDFile((DFile)val);
                    break;
                case FieldPrimitiveType.Date:
                    if (val is DateOnly date)
                    {
                        WriteInt(date.Year);
                        WriteInt(date.Month);
                        WriteInt(date.Day);
                    }
                    else
                    {
                        WriteInt(0);
                        WriteInt(0);
                        WriteInt(0);
                    }
                    break;
                case FieldPrimitiveType.DateTime:
                    if (val is DateTime dateTime)
                    {
                        WriteLong(new DateTimeOffset(dateTime).ToUnixTimeMilliseconds());
                    }
                    else
                    {
                        WriteLong(0);
                    }
                    break;
                case FieldPrimitiveType.Double:
                    WriteDouble((double)val);
                    break;
                case FieldPrimitiveType.Duration:
                    if (val is TimeSpan duration)
                    {
                        WriteLong((long)duration.TotalMilliseconds);
                    }
                    else
                    {
                        WriteLong((long)val);
                    }
                    break;
                case FieldPrimitiveType.Enum:
                    int typeIdx = template.ToClientTypeIdx(field.Reference.GetIndex());
                    TemplateType et = template.GetType(typeIdx);
                    if (val is string name)
                    {
                        int fid = 0;
                        foreach (var f in et.Fields)
                        {
                            if (f.Name == name)
                            {
                                WriteInt(fid);
                                return;
                            }
                            fid++;
                        }
                        WriteInt(0);
                    }
                    else if (val is Enum enm)
                    {
                        int cfid = et.ToClientIdx(enm.GetHashCode());
                        WriteInt(cfid);
                    }
                    break;
                case FieldPrimitiveType.Integer:
                    WriteLong((long)val);
                    break;
                case FieldPrimitiveType.String:
                    WriteString((string)val);
                    break;
                case FieldPrimitiveType.Time:
                    if (val is TimeSpan t1)
                    {
                        WriteLong(t1.Seconds * 1000);
                    } 
                    else if(val is TimeOnly t2)
                    {
                        TimeSpan timeSpan = t2.ToTimeSpan();
                        WriteLong(timeSpan.Seconds * 1000);    
                    }
                    else
                    {
                        WriteLong(0);
                    }
                    break;
                case FieldPrimitiveType.Blob:
                    WriteBlob((Blob)val);
                    break;
                case FieldPrimitiveType.Geolocation:
                    WriteGeolocation((Geolocation)val);
                    break;
                default:
                    throw new Exception("Unsupported type: " + val.GetType());
            }
        }

        public object ReadPrimitive(DField<object, object> field, Template template)
        {
            switch (field.GetPrimitiveType())
            {
                case FieldPrimitiveType.Boolean:
                    return ReadBoolean();
                case FieldPrimitiveType.DFile:
                    return ReadDFile();
                case FieldPrimitiveType.Date:
                    return ReadDate();
                case FieldPrimitiveType.DateTime:
                    return ReadDateTime();
                case FieldPrimitiveType.Double:
                    return ReadDouble();
                case FieldPrimitiveType.Duration:
                    long mill = ReadLong();
                    return TimeSpan.FromMilliseconds(mill);
                case FieldPrimitiveType.Enum:
                    return ReadEnum(field.Reference.GetIndex(), template);
                case FieldPrimitiveType.Integer:
                    return ReadLong();
                case FieldPrimitiveType.String:
                    return ReadString();
                case FieldPrimitiveType.Time:
                    return ReadTime();
                case FieldPrimitiveType.Blob:
                    return ReadBlob();
                case FieldPrimitiveType.Geolocation:
                    return ReadGeolocation();
                default:
                    throw new Exception("Unsupported type: " + field.GetPrimitiveType());
            }
        }

        public TimeSpan ReadTime()
        {
            long lng = ReadLong();
            if (lng == -1) return default;
            return new TimeSpan(lng * 10000); // Ticks are in 100-nanosecond intervals
        }

        public DateTime ReadDateTime()
        {
            long lng = ReadLong();
            if (lng == -1) return default;
            return DateTimeOffset.FromUnixTimeMilliseconds(lng).DateTime;
        }

        public object ReadEnum(int enumType, Template template)
        {
            int field = ReadInt();
            int typeIdx = template.ToClientTypeIdx(enumType);
            TemplateType et = template.GetType(typeIdx);
            return et.Fields[field].GetValue(null);
        }

        public DateOnly ReadDate()
        {
            int year = ReadInt();
            if (year == -1) return default;
            int month = ReadInt();
            int day = ReadInt();
            return new DateOnly(year, month, day);
        }

        public DFile ReadDFile()
        {
            string str = ReadString();
            if (str == null) return null;
            DFile file = new DFile();
            file.Id = str;
            file.SetName(ReadString());
            file.SetSize(ReadLong());
            file.SetMimeType(ReadString());
            return file;
        }

        public Geolocation ReadGeolocation()
        {
            int val = ReadInt();
            if (val == -1) return null;
            double lat = ReadDouble();
            double lng = ReadDouble();
            return new Geolocation(lat, lng);
        }

        public Blob ReadBlob()
        {
            int sz = ReadInt();
            if (sz == -1) return null;
            byte[] arr = new byte[sz];
            for (int i = 0; i < sz; i++)
            {
                arr[i] = ReadByte();
            }
            return new Blob(arr);
        }

        public void WriteEnum<T>(T enm) where T : Enum
        {
            throw new Exception("Unsupported enum: " + enm.GetType());
        }

        public void WriteMessage(RocketMessage msg)
        {
            // TODO: Implement if needed
        }

        public void Discard()
        {
            outPosition = 0;
        }

        public void Rewind()
        {
            inPosition = 0;
        }

        public void SetSender(RocketSender sender)
        {
            this.sender = sender;
        }

        public void WriteLocalDate(DateOnly date)
        {
            if (date == null)
            {
                WriteInt(-1);
            }
            else
            {
                WriteInt(date.Year);
                WriteInt(date.Month);
                WriteInt(date.Day);
            }
        }

        public void WriteLocalDateList(List<DateOnly> list)
        {
            WriteList(list, WriteLocalDate);
        }

        public void WriteLocalDateTime(DateTime result)
        {
            WriteDateTime(result);
        }

        public void WriteLocalDateTimeList(List<DateTime> result)
        {
            WriteList(result, WriteLocalDateTime);
        }

        public void WriteLocalTime(TimeSpan result)
        {
            if (result == null)
            {
                WriteLong(-1);
            }
            else
            {
                WriteLong((long) (result.TotalNanoseconds / (1000 * 1000)));
            }
        }

        public void WriteLocalTimeList(List<TimeSpan> result)
        {
            WriteList(result, WriteLocalTime);
        }

        public void WriteDuration(TimeSpan result)
        {
            if (result == null)
            {
                WriteLong(-1);
                return;
            }
            long nanos = (long)result.TotalNanoseconds;
            long micros = nanos / 1000;

            WriteLong(micros);
        }

        public void WriteDurationList(List<TimeSpan> result)
        {
            WriteList(result, WriteDuration);
        }

        public void WriteDFileList(List<DFile> result)
        {
            WriteList(result, WriteDFile);
        }

        public void WriteGeolocationList(List<Geolocation> result)
        {
            WriteList(result, WriteGeolocation);
        }
    }
}
