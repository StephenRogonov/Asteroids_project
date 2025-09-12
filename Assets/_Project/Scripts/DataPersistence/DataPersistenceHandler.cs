using _Project.Scripts.Bootstrap.Configs;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.DataPersistence
{
    public class DataPersistenceHandler
    {
        private FileDataHandler _dataHandler;
        private List<IDataPersistence> _dataPersistenceObjects = new();

        public GameConfig GameConfig { get; private set; }
        public PlayerData PlayerData { get; private set; }

        public event Action PlayerDataChanged;

        public DataPersistenceHandler(FileDataHandler dataHandler, GameConfig gameConfig)
        {
            _dataHandler = dataHandler;
            GameConfig = gameConfig;
        }

        public void AddDataObject(IDataPersistence dataPersistence) => _dataPersistenceObjects.Add(dataPersistence);

        public void RemoveDataObject(IDataPersistence dataPersistence) => _dataPersistenceObjects.Remove(dataPersistence);

        private void NewGame()
        {
            PlayerData = new PlayerData();
            SavePlayerData();
        }

        public void SetRemoteGameConfig(string config)
        {
            GameConfig = JsonConvert.DeserializeObject<GameConfig>(config);
        }

        public async UniTask LoadPlayerData()
        {
            Debug.Log("Load triggered.");

            PlayerData = await _dataHandler.LoadGame();

            if (PlayerData == null)
            {
                Debug.LogWarning("No game data found. New game data will be created.");
                NewGame();
            }
        }

        public async UniTask SavePlayerData()
        {
            Debug.Log("Save triggered.");

            if (PlayerData == null)
            {
                Debug.LogError("No player data found. A New Game must be started before data can be loaded.");
                return;
            }

            foreach (IDataPersistence obj in _dataPersistenceObjects)
            {
                obj.SaveData(PlayerData);
            }

            await _dataHandler.SaveGame(PlayerData);
            PlayerDataChanged?.Invoke();
        }
    }
}

