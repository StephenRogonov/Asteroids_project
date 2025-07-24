using _Project.Scripts.Bootstrap.Configs;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace _Project.Scripts.DataPersistence
{
    public class FileDataHandler
    {
        private readonly string _dataDirPath = Application.persistentDataPath;
        private readonly string _configsFileName = "config_data.game";

        public async UniTask UpdateGameConfigsUniTask(string remoteConfig)
        {
            string fullPath = Path.Combine(_dataDirPath, _configsFileName);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? string.Empty);

                string dataToStore = remoteConfig;

                using FileStream stream = new FileStream(fullPath, FileMode.Create);
                using StreamWriter writer = new StreamWriter(stream);
                writer.Write(dataToStore);
            }
            catch (Exception e)
            {
                Debug.LogError("Can't save data to " + fullPath + "\n" + e.Message);
            }
        }

        public async UniTask<GameConfig> LoadConfigUniTask()
        {
            string fullPath = Path.Combine(_dataDirPath, _configsFileName);
            GameConfig loadedData = null;

            if (File.Exists(fullPath))
            {
                try
                {
                    string dataToLoad;
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using StreamReader reader = new StreamReader(stream);
                        dataToLoad = reader.ReadToEnd();
                    }

                    loadedData = JsonConvert.DeserializeObject<GameConfig>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("Can't load data from " + fullPath + "\n" + e.Message);
                }
            }

            return loadedData;
        }

        //public async UniTask<GameData> LoadUniTask()
        //{
        //    string fullPath = Path.Combine(_dataDirPath, _configsFileName);
        //    GameData loadedData = null;

        //    if (File.Exists(fullPath))
        //    {
        //        try
        //        {
        //            string dataToLoad;
        //            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
        //            {
        //                using StreamReader reader = new StreamReader(stream);
        //                dataToLoad = reader.ReadToEnd();
        //            }

        //            loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad);
        //        }
        //        catch (Exception e)
        //        {
        //            Debug.LogError("Can't load data from " + fullPath + "\n" + e.Message);
        //        }
        //    }

        //    return loadedData;
        //}

        //public async UniTask SaveUniTask(GameData data)
        //{
        //    string fullPath = Path.Combine(_dataDirPath, _configsFileName);

        //    try
        //    {
        //        Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? string.Empty);

        //        string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented);

        //        using FileStream stream = new FileStream(fullPath, FileMode.Create);
        //        using StreamWriter writer = new StreamWriter(stream);
        //        writer.Write(dataToStore);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.LogError("Can't save data to " + fullPath + "\n" + e.Message);
        //    }
        //}
    }
}

