namespace _Project.Scripts.DataPersistence
{
    /// <summary>
    /// Interface for save/load methods.
    /// </summary>
    public interface IPlayerData
    {
        void LoadData(PlayerData data);
        void SaveData(PlayerData data);
    }
}