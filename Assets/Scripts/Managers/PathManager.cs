namespace TD
{
    using UnityEngine;

    public class PathManager : MonoBehaviour
    {
        [SerializeField]
        private LevelData level;

        public void CreatePathPoints()
        {
            for (int i = 0; i < level.pathPoints.Count; i++)
            {
                Vector3 pos = level.pathPoints[i];
                GameObject obj = new GameObject("path point " + i);
                obj.transform.position = pos;
            }
        }

        private void Awake()
        {
            CreatePathPoints();
        }
    }
}