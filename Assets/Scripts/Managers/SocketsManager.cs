namespace TD
{
    using UnityEngine;

    public class SocketsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject socket;

        [SerializeField]
        private Sockets sockets;

        private LevelData level;

        public void Setup(LevelData lvlData)
        {
            this.level = lvlData;
            CreateSockets();
        }

        private void CreateSockets()
        {
            for (int i = 0; i < this.level.socketPositions.Count; i++)
            {
                Vector3 pos = this.level.socketPositions[i];
                this.sockets.Add(Instantiate(this.socket, pos, Quaternion.identity));
            }
        }

        private void OnApplicationQuit()
        {
            this.sockets.Items.Clear();
        }
    }
}
