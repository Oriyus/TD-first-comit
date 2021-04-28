namespace TD
{
    using UnityEngine;

    public class GameManager : Singleton<GameManager>
    {    
        private float enemySpeed;
        private float timeTillNextWave;
        private int collectedGold = 0;
        private int collectedReusableParts = 0;

        public int Parts
        {
            set
            {
                this.collectedReusableParts += value;
                UI.Instance.SetParts = this.collectedReusableParts;
            }
        }

        public int Gold
        {
            set
            {
                this.collectedGold += value;
                UI.Instance.SetGold = this.collectedGold;
            }
            get { return this.collectedGold; }
        }

        public float EnemySpeed
        {
            get { return this.enemySpeed; }
        }

        public float NextWaveTime
        {
            set
            {
                UI.Instance.SetTimeForNextWave = value;               
            }
            get { return this.timeTillNextWave; }
        }

        public string LastWave
        {
            set
            {
                UI.Instance.SetLastWave = value;
            }
        }

        public void Pause()
        {
            TimeManager.Instance.PauseGame();
        }

        public void UnPause()
        {
            TimeManager.Instance.UnPauseGame();
        }

        public void CreateEnemy()
        {
            this.enemySpeed = LevelDataManager.Instance.WavesSpeeds[TimeManager.Instance.WaveIndex];
            EnemyManager.Instance.CreateEnemy(TimeManager.Instance.WaveIndex, TimeManager.Instance.EnemyIndex);
        }

        private void SetupLevelData()
        {
            TimeManager.Instance.LaunchTimes = LevelDataManager.Instance.LaunchTimes;
            TimeManager.Instance.Waves = LevelDataManager.Instance.Waves;
            SocketsManager.Instance.SocketPositions = LevelDataManager.Instance.SocketPositions;
            PathManager.Instance.PathPoints = LevelDataManager.Instance.PathPoints;
            EnemyManager.Instance.Waves = LevelDataManager.Instance.Waves;

            this.Gold = 0;
            this.Parts = 0;
        }

        private void Awake()
        {
            this.SetupLevelData();
        }

        private void Start()
        {
            TimeManager.Instance.CreateEnemyEvent.AddListener(CreateEnemy);
        }
    }
}
