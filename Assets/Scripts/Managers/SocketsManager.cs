namespace TD
{
    using System.Collections.Generic;
    using UnityEngine;

    public class SocketsManager : Singleton<SocketsManager>
    {
        [SerializeField]
        private GameObject socket;

        private List<Vector2> socketsPositions;
        public List<GameObject> sockets;

        public List<Vector2> SocketPositions
        {
            set
            {
                this.socketsPositions = value;
                this.InitiateSockets();
            }
        }

        public List<GameObject> Sockets
        {
            get { return this.sockets; }
        }

        private void InitiateSockets()
        {
            for (int i = 0; i < this.socketsPositions.Count; i++)
            {
                Vector3 pos = this.socketsPositions[i];
                this.sockets.Add(Instantiate(this.socket, pos, Quaternion.identity));
            }
        }
    }
}
