using _Project.Scripts.Bootstrap.Configs;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.DataPersistence
{
    public class DataPersistenceHandler
    {
        private FileDataHandler _dataHandler;
        private List<IPlayerData> _dataPersistenceObjects;
        //private GameConfig _gameConfig;
        private PlayerData _gameData;

        public GameConfig GameConfig { get; private set; }

        public DataPersistenceHandler(FileDataHandler dataHandler, GameConfig gameConfig)
        {
            _dataHandler = dataHandler;
            GameConfig = gameConfig;
        }

        //public void Add(IDataPersistence dataPersistence) => _dataPersistenceObjects.Add(dataPersistence);

        //public void Remove(IDataPersistence dataPersistence) => _dataPersistenceObjects.Remove(dataPersistence);
        
        //public void NewGame()
        //{
        //    _gameConfig = new GameConfig();
        //    //_gameData = new GameData();
        //    //SaveGameDataUniTask();
        //}
    
        public async UniTask LoadGameConfigUniTask()
        {
            GameConfig configFromFile = await _dataHandler.LoadConfigUniTask();
            //_gameConfig = await _dataHandler.LoadConfigUniTask();
            
            if (configFromFile == null)
            {
                Debug.LogWarning("No game config loaded. Default config will be used.");
                return;
                //NewGame();
            }

            GameConfig = configFromFile;
        }

        //public async UniTask LoadGameDataUniTask()
        //{
        //    _gameData = await _dataHandler.LoadUniTask();
            
        //    if (_gameData == null)
        //    {
        //        Debug.LogWarning("No game data found. New game data will be created.");
        //        NewGame();
        //    }

        //    //foreach (var obj in _dataPersistenceObjects)
        //    //{
        //    //    obj.LoadData(_gameData);
        //    //}
        //}
    
        //public async UniTask SaveGameDataUniTask()
        //{
        //    if (_gameData == null)
        //    {
        //        Debug.LogError("No game data found. A New Game must be started before data can be loaded.");
        //        return;
        //    }

        //    //foreach (var obj in _dataPersistenceObjects)
        //    //{
        //    //    obj.SaveData(_gameData);
        //    //}
            
        //    await _dataHandler.SaveUniTask(_gameData);
        //}
    }
}

