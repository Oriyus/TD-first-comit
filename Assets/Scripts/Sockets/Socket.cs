namespace TD
{
    using UnityEngine;

    public class Socket : MonoBehaviour
    {
        public bool occupied = false;
        public GameObject containsUnit = null;

        public void SetOccupied()
        {
            occupied = true;
        }

        public void SetUnoccupied()
        {
            occupied = false;
        }
    }
}
