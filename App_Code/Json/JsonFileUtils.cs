using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Json
{
    public static class JsonFileUtils
    {
        private static readonly JsonSerializerSettings _options = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        private static string dataFolder = Path.GetFullPath("./Data/");



        public static void Write(object obj, Type type, string fileName)
        {
            if (!CreateDataFolder())
                return;

            if (obj.GetType() != type)
                throw new TypeAccessException("Le Type : "+type.Name+" est different de l'objet donné : "+obj.GetType().Name);

            var jsonString = JsonConvert.SerializeObject(obj, type, Formatting.Indented, _options);
            File.WriteAllText(Path.Combine(dataFolder, fileName.MatchJsonFilename()), jsonString);

        }

        public static T Read<T>(string fileName)
        {
            string combinedPath = Path.Combine(dataFolder, fileName.MatchJsonFilename());
            if (!File.Exists(combinedPath))
            {
                return default(T);
            }

            string jsonString = File.ReadAllText(combinedPath);
            if (string.IsNullOrEmpty(jsonString))
                return default(T);

            T jsonObject = JsonConvert.DeserializeObject<T>(jsonString);
            return jsonObject;

      
        }


        public static List<T> UpdateJson<T>(this List<T> list,string filename)
        {
            if (list == null)
                return default(List<T>);

            JsonFileUtils.Write(list, typeof(List<T>), filename);
            list = JsonFileUtils.Read<List<T>>(filename);
            return list;
        }

        public static bool DeleteJson(string filename)
        {
            try
            {
                string combinedPath = Path.Combine(dataFolder, filename.MatchJsonFilename());
                if (!File.Exists(combinedPath))
                {
                    return true;
                }

                File.Delete(combinedPath);
                return true;
            }
            catch
            {
                return false;
            }

           
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
