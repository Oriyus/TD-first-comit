namespace TD
{
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemyManager : Singleton<EnemyManager>
    {
        private Wave[] waves;
        private List<GameObject> liveEnemies;

        public Wave[] Waves
        {
            get { return this.waves; }
            set { this.waves = value; }
        }

        public List<GameObject> LiveEnemies
        {
            get { return liveEnemies; }
        }

        public void CreateEnemy(int wave, int enemyIndex)
        {
            GameObject newEnemy = Instantiate(this.waves[wave].waveEnemies[enemyIndex]);
            this.liveEnemies.Add(newEnemy);
        }

        public void RemoveLiveEnemy(int index)
        {
            this.liveEnemies.RemoveAt(index);
        }

        private void Awake()
        {
            TimeManager.Instance.OnCreateEnemyEvent += CreateEnemy;
        }

        private void Start()
        {
            this.liveEnemies = new List<GameObject>();
        }
    }
}