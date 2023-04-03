using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json
{
    public static class JsonFileUtils
    {
        private static readonly JsonSerializerSettings _options = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        private static string dataFolder = Path.GetFullPath("../../Data/");


        public static void SimpleWrite(object obj, string fileName)
        {
            if (!CreateDataFolder())
                return;

            var jsonString = JsonConvert.SerializeObject(obj, _options);
            File.WriteAllText(Path.Combine(dataFolder, fileName.Replace("/", String.Empty)), jsonString);
        }

        public static void PrettyWrite(object obj, string fileName)
        {

            if (!CreateDataFolder())
                return;

            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented, _options);
            File.WriteAllText(Path.Combine(dataFolder,fileName.Replace("/",String.Empty)), jsonString);
        }

        public static T Read<T>(string fileName)
        {
            string jsonString = File.ReadAllText(Path.Combine(dataFolder, fileName.Replace("/", String.Empty)));
            T jsonObject = JsonConvert.DeserializeObject<T>(jsonString);
            return jsonObject;
        }


        public static bool CreateDataFolder()
        {
            try
            {
                if (!Directory.Exists(dataFolder))
                {
                    Directory.CreateDirectory(dataFolder);
                }

                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e);
               
            }
            return false;
        }
    }
}
