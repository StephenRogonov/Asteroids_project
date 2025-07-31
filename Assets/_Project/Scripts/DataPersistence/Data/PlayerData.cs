using System;

namespace _Project.Scripts.DataPersistence
{
    [Serializable]
    public class PlayerData
    {
        public bool NoAdsPurchased { get; set; }

        public PlayerData()
        {
            NoAdsPurchased = false;
        }
    }
}

