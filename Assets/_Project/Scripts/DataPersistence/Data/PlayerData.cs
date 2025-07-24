using System;

namespace _Project.Scripts.DataPersistence
{
    [Serializable]
    public class PlayerData
    {
        public bool noAdsPurchased { get; private set; }

        public PlayerData()
        {
            noAdsPurchased = false;
        }
    }
}

