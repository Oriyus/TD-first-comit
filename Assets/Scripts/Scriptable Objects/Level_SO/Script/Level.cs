namespace TD
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Level", menuName = "TD_Level")]
    public class Level : ScriptableObject
    {
        // What enemies in what wave
        public Wave[] waves;

        // Times when waves will launch
        public List<float> launchTimes;

        // Speed of waves
        public int[] wavesSpeeds;

        // Sockets positions
        public List<Vector2> socketPositions;

        // Path points
        public List<Vector2> pathPoints;
    }
}
