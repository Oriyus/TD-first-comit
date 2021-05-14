using UnityEngine;

namespace TD
{
    public class TimeManager : MonoBehaviour
    {
        private LevelData level;

        private int waveIndex = 0;
        private int enemyIndex = 0;
        private int wave = 0;
        private int enemiesNumber;

        public float spawnTimeBetweenEnemies = 1f;
        private float enemyCreateTimer = 0f;
        private float countdownWaveTimer = 0f;
        private float waveTimeSnapshot;
        private float levelStartTime;
        private float currentLevelTime;

        private bool initiateEnemieWaveCreation = false;

        public TypeEvent<float> floatEvent = null;
        public TypeEvent<string> stringEvent = null;

        public void Setup(LevelData lvlData)
        {
            this.level = lvlData;
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void UnPauseGame()
        {
            Time.timeScale = 1;
        }

        // Set timer for when to create next enemy
        private void EnemyTimer()
        {
            this.enemyCreateTimer = this.currentLevelTime + this.spawnTimeBetweenEnemies;
        }

        private void GetEnemiesInWave(int index)
        {
            this.enemiesNumber = this.level.waves[index].waveEnemies.Length;
        }

        private float TimeTillNextWave(float end)
        {
            return this.countdownWaveTimer = end - this.currentLevelTime;
        }

        private void Update()
        {
            // Check for wave launching times
            if (this.waveIndex < this.level.launchTimes.Count && this.currentLevelTime >= this.level.launchTimes[this.waveIndex])
            {
                // Launch Wave
                if (this.waveIndex < this.level.launchTimes.Count - 1)
                {
                    // Wave number waveIndex
                    this.waveTimeSnapshot = this.currentLevelTime + this.level.launchTimes[this.waveIndex + 1] - this.level.launchTimes[this.waveIndex];
                }
                this.GetEnemiesInWave(this.waveIndex);
                this.initiateEnemieWaveCreation = true;
                this.EnemyTimer();
                this.waveIndex++;
            }

            // Check for enemy creating times in a wave
            if (this.initiateEnemieWaveCreation)
            {
                if ((this.enemyIndex < this.enemiesNumber) && (this.currentLevelTime >= this.enemyCreateTimer))
                {
                    EnemyTimer();
                    //this.OnCreateEnemyEvent?.Invoke(this.WaveIndex, this.enemyIndex);
                    GameObject newEnemy = Instantiate(this.level.waves[wave].waveEnemies[enemyIndex]);
                    this.enemyIndex++;
                }
                else if (this.enemyIndex == this.enemiesNumber)
                {
                    this.initiateEnemieWaveCreation = false;
                    this.enemyIndex = 0;
                    // next wave
                    this.wave++;
                }
            }

            // Calculate When is the next wave if game not paused
            if (this.waveIndex < this.level.launchTimes.Count)
            {
                floatEvent.Invoke(this.TimeTillNextWave(this.waveTimeSnapshot));
            }
            else
            {
                stringEvent.Invoke("Last Wave !!!");
            }

            // Current level time
            this.currentLevelTime = Time.time - this.levelStartTime;
        }

        private void Awake()
        {
            this.levelStartTime = Time.time;
        }
    }
}