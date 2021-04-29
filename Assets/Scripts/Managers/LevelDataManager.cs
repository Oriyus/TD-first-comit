namespace TD
{
    using System.Collections.Generic;
    using UnityEngine;

    public class LevelDataManager : Singleton<LevelDataManager>
    {
        public Level[] levels;

        private Wave[] waves;
        private List<float> launchTimes;
        private List<Vector2> socketPositions;
        private List<Vector2> pathPoints;

        public Wave[] Waves
        {
            get { return this.waves; }
        }

        public List<float> LaunchTimes
        {
            get { return this.launchTimes; }
        }

        public List<Vector2> SocketPositions
        {
            get { return this.socketPositions; }
        }

        public List<Vector2> PathPoints
        {
            get { return this.pathPoints; }
        }

        private void LoadLevelData(int level)
        {
            this.waves = this.levels[level].waves;
            this.launchTimes = this.levels[level].launchTimes;
            this.socketPositions = this.levels[level].socketPositions;
            this.pathPoints = this.levels[level].pathPoints;
        }

        private void Awake()
        {
            this.LoadLevelData(0);
        }
    }
}