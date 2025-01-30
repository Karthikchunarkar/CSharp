using System.Security.Cryptography;
using System.Text;
using gqltosql.schema;

namespace rest.ws
{
    public class MasterTemplate
    {
        private readonly IModelSchema schema;

        private Dictionary<string, TemplateType> typesByHash = new Dictionary<string, TemplateType>();
        private Dictionary<string, TemplateUsage> usageByHash = new Dictionary<string, TemplateUsage>();
        private Dictionary<string, TemplateClazz> channelsByHash = new Dictionary<string, TemplateClazz>();
        private Dictionary<string, TemplateClazz> rpcsByHash = new Dictionary<string, TemplateClazz>();

        public MasterTemplate(IModelSchema schema)
        {
            this.schema = schema;
            Initialize();
        }

        private void Initialize()
        {
            var allTypes = schema.GetAllTypes();
            var hashByType = new Dictionary<string, TemplateType>();
            foreach (var t in allTypes)
            {
                AddTemplateType(t, hashByType);
            }
            hashByType.Clear();

            var allChannels = schema.GetAllChannels();
            foreach (var c in allChannels)
            {
                AddTemplateChannel(c);
            }

            var allRPCs = schema.GetAllRPCs();
            foreach (var c in allRPCs)
            {
                AddTemplateRPC(c);
            }

            Console.WriteLine("Master Template Ready");
        }

        public string GetByType(string type)
        {
            foreach (var entry in typesByHash)
            {
                if (entry.Value.Model.GetType().Equals(type))
                {
                    return entry.Key;
                }
            }
            return null;
        }

        private void AddTemplateRPC(DClazz c)
        {
            AddClazzToTemplate(c, true);
        }

        private void AddTemplateChannel(DClazz c)
        {
            AddClazzToTemplate(c, false);
        }

        private void AddClazzToTemplate(DClazz c, bool isRPC)
        {
            var md5 = new List<string> { c.Name };
            var allFields = c.Methods.ToList();
            var tt = new TemplateClazz(c, allFields.Count);
            int i = 0;
            foreach (var f in allFields)
            {
                tt.AddMethod(i++, f);
                md5.Add(f.Name);
                foreach (var p in f.Params)
                {
                    var dm = schema.GetType(p.Type);
                    md5.Add(dm.GetType());
                }
                if (isRPC && f.HasReturnType)
                {
                    var dm = schema.GetType(f.ReturnType);
                    md5.Add(dm.GetType());
                }
            }
            string hash = ComputeMD5Hash(md5);
            tt.Hash = hash;
            if (isRPC)
            {
                rpcsByHash[hash] = tt;
            }
            else
            {
                channelsByHash[hash] = tt;
            }
        }

        private TemplateType AddTemplateType(DModel<object> md, Dictionary<string, TemplateType> allTypes)
        {
            if (allTypes.ContainsKey(md.GetType()))
            {
                return allTypes[md.GetType()];
            }
            var tt = new TemplateType(md, md.GetFields().Length);
            allTypes[md.GetType()] = tt;

            var allFields = md.GetFields().ToList();
            if (md.GetModelType() != DModelType.ENUM)
            {
                allFields.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            }
            int i = 0;
            foreach (var f in allFields)
            {
                tt.AddField(i++, f);
            }
            if (md.GetParent() != null)
            {
                var parent = AddTemplateType(md.GetParent(), allTypes);
                tt.SetParent(parent);
            }
            tt.computeHash();
            typesByHash[tt.Hash] = tt;
            return tt;
        }

        public TemplateType GetTemplateType(string typeHash)
        {
            return typesByHash.TryGetValue(typeHash, out var result) ? result : null;
        }

        public TemplateUsage GetUsageTemplate(string usageHash)
        {
            return usageByHash.TryGetValue(usageHash, out var result) ? result : null;
        }

        public TemplateClazz GetChannelTemplate(string channelHash)
        {
            return channelsByHash.TryGetValue(channelHash, out var result) ? result : null;
        }

        public TemplateClazz GetRPCTemplate(string channelHash)
        {
            return rpcsByHash.TryGetValue(channelHash, out var result) ? result : null;
        }

        public void AddTypeTemplate(TemplateType tt)
        {
            typesByHash[tt.Hash] = tt;
        }

        public void AddUsageTemplate(TemplateUsage tu, Template tml)
        {
            var md5 = new List<string>();
            var uts = tu.Types;
            foreach (var ut in uts)
            {
                AddUsageMD5Strings(md5, ut, tml);
            }
            string hash = ComputeMD5Hash(md5);
            tu.Hash = hash;
            // usageByHash[hash] = tu;
        }

        public void AddChannelTemplate(TemplateClazz tc, Template tml)
        {
            AddClazzMethod(tc, tml, false);
        }

        private void AddClazzMethod(TemplateClazz tc, Template tml, bool isRPC)
        {
            var md5 = new List<string> { tc.Clazz.Name };
            foreach (var one in tc.Methods)
            {
                md5.Add(one.Name);
                foreach (var type in one.Params)
                {
                    var dm = tml.GetType(type.Type).Model;
                    md5.Add(dm.GetType());
                }
                if (isRPC && one.HasReturnType)
                {
                    var dm = tml.GetType(one.ReturnType).Model;
                    md5.Add(dm.GetType());
                }
            }
            string hash = ComputeMD5Hash(md5);
            tc.Hash = hash;
            if (isRPC)
            {
                rpcsByHash[hash] = tc;
            }
            else
            {
                channelsByHash[hash] = tc;
            }
        }

        private void AddUsageMD5Strings(List<string> md5, UsageType ut, Template tml)
        {
            var type = tml.GetType(ut.Type);
            if (type.Model == null)
            {
                return;
            }
            md5.Add(type.Model.GetType());
            foreach (var f in ut.Fields)
            {
                var df = type.GetField(f.Field);
                md5.Add(df.Name);
                var types = f.Types;
                foreach (var utt in types)
                {
                    AddUsageMD5Strings(md5, utt, tml);
                }
            }
        }

        public void AddRPCTemplate(TemplateClazz tc, Template template)
        {
            AddClazzMethod(tc, template, true);
        }

        private string ComputeMD5Hash(List<string> input)
        {
            using (var md5 = MD5.Create())
            {
                var inputString = string.Join(",", input);
                var inputBytes = Encoding.UTF8.GetBytes(inputString);
                var hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
