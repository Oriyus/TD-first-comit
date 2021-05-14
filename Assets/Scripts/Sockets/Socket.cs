using UnityEngine;

namespace TD
{
    public class Socket : MonoBehaviour
    {
        private bool occupied = false;
        private GameObject containsUnit = null;

        public bool Occupied
        {
            set { this.occupied = value; }
            get { return this.occupied; }
        }

        public GameObject Contains
        {
            set { this.containsUnit = value; }
            get { return this.containsUnit; }
        }
    }
}
