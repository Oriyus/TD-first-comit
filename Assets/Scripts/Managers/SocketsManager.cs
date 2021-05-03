namespace TD
{
    using UnityEngine;

    public class SocketsManager : MonoBehaviour
    {
        [SerializeField]
        private LevelData level;

        [SerializeField]
        private GameObject socket;

        public Sockets sockets;

        private void InitiateSockets()
        {
            for (int i = 0; i < this.level.socketPositions.Count; i++)
            {
                Vector3 pos = this.level.socketPositions[i];
                this.sockets.Add(Instantiate(this.socket, pos, Quaternion.identity));
            }
        }

        private void Awake()
        {
            InitiateSockets();
        }

        private void OnApplicationQuit()
        {
            this.sockets.Items.Clear();
        }
    }
}
