namespace TD
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    public class TimeManager : MonoBehaviour
    {
        [SerializeField]
        private Level level;

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

        public UnityEvent LaunchWaveEvent;
        public event Action<int,int> OnCreateEnemyEvent;
        public event Action<float> OnWaveTimeChanged;
        public event Action<string> OnLastWave;

        public int EnemyIndex
        {
            get { return this.enemyIndex; }
        }

        public int WaveIndex
        {
            get { return this.wave; }
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
                this.LaunchWaveEvent.Invoke();
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
                OnWaveTimeChanged?.Invoke(this.TimeTillNextWave(this.waveTimeSnapshot));
            }
            else
            {
                OnLastWave?.Invoke("Last Wave!!!");
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