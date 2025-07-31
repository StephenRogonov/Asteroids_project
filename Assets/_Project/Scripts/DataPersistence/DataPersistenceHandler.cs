using _Project.Scripts.Bootstrap.Configs;
using Cysharp.Threading.Tasks;
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

        public void NewGame()
        {
            PlayerData = new PlayerData();
            SavePlayerDataUniTask();
        }

        public async UniTask LoadGameConfigUniTask()
        {
            GameConfig configFromFile = await _dataHandler.LoadConfigUniTask();
            
            if (configFromFile == null)
            {
                Debug.LogWarning("No game config loaded. Default config will be used.");
                return;
            }

            GameConfig = configFromFile;
        }

        public async UniTask LoadPlayerDataUniTask()
        {
            Debug.Log("Load triggered.");

            PlayerData = await _dataHandler.LoadGameUniTask();

            if (PlayerData == null)
            {
                Debug.LogWarning("No game data found. New game data will be created.");
                NewGame();
            }
        }

        public async UniTask SavePlayerDataUniTask()
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

            await _dataHandler.SaveGameUniTask(PlayerData);
            PlayerDataChanged?.Invoke();
        }
    }
}

