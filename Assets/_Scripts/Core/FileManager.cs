using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Core
{

    //https://github.com/llamacademy/persistent-data/blob/main/Assets/Scripts/JSONDataService.cs
    public static class FileManager
    {
        public static void SaveData(string fileName, object data)
        {
            var jsonData = JsonUtility.ToJson(data);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/" + fileName, jsonData);
        }

        public static T LoadData<T>(string fileName)
        {
            var path = Application.persistentDataPath + "/" + fileName;
            if (System.IO.File.Exists(path))
            {
                var text = System.IO.File.ReadAllText(path);
                //return JsonConvert.DeserializeObject<T>(text);
                return JsonUtility.FromJson<T>(text);
            }
            return default(T);
        }

    }
}
