using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NessHappyNess.Drivers
{
    public static class JsonHelper
    {
        #region Read
        public static T ReadFromJsonToObject<T>(string fullPath)
        {
            try
            {
                StreamReader r = new StreamReader(fullPath);
                string json = r.ReadToEnd();
                T values = JsonConvert.DeserializeObject<T>(json);
                r.Close();
                return values;
            }
            catch
            {
                return default(T);
            }
        }
        #endregion

        #region write
        public static void WriteToJson(Object obj, string fullPath)
        {
            try
            {
                // Int
                FileStream fs = new FileStream(fullPath, FileMode.Create);
                StreamWriter str = new StreamWriter(fs);
                string json = JsonConvert.SerializeObject(obj);
                str.WriteLine(json);
                str.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}