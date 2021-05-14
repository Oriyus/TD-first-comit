using UnityEngine;

namespace TD
{
    public class TurretCancel : MonoBehaviour
    {
        [SerializeField]
        private Sockets sockets;

        // Unregister unit and destroy it
        public void CancelUnit(int inSocket)
        {
            sockets.Items[inSocket].GetComponent<Socket>().Occupied = false;
            Destroy(gameObject);
        }
    }
}
