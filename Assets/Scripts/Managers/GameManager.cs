namespace TD
{
    public class GameManager : Singleton<GameManager>
    {    
        private void SetupLevelData()
        {
            //SocketsManager.Instance.SocketPositions = LevelDataManager.Instance.SocketPositions;
        }

        private void Awake()
        {
            this.SetupLevelData();
        }
    }
}
