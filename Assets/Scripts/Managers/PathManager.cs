namespace TD
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PathManager : Singleton<PathManager>
    {
        public List<Transform> mapPath;

        public List<Transform> Path
        {
            get { return this.mapPath; }
            set { this.mapPath = value; }
        }

        public List<Vector2> PathPoints
        {
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    Vector3 pos = value[i];
                    GameObject obj = new GameObject("path point " + i);
                    obj.transform.position = pos;
                    mapPath.Add(obj.transform);
                }
            }
        }
    }
}