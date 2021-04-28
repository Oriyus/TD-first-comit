namespace TD
{
    public class GameManager : Singleton<GameManager>
    {    
        public void Pause()
        {
            TimeManager.Instance.PauseGame();
        }

        public void UnPause()
        {
            TimeManager.Instance.UnPauseGame();
        }

        private void SetupLevelData()
        {
            TimeManager.Instance.LaunchTimes = LevelDataManager.Instance.LaunchTimes;
            TimeManager.Instance.Waves = LevelDataManager.Instance.Waves;
            SocketsManager.Instance.SocketPositions = LevelDataManager.Instance.SocketPositions;
            PathManager.Instance.PathPoints = LevelDataManager.Instance.PathPoints;
            EnemyManager.Instance.Waves = LevelDataManager.Instance.Waves;
        }

        private void Awake()
        {
            this.SetupLevelData();
        }
    }
}
