using UnityEngine;

namespace TD
{
    public class InputManager : MonoBehaviour
    {
        public StaticEvent leftClickDown = null;
        public StaticEvent leftClickUp = null;
        public StaticEvent rightClickDown = null;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                leftClickDown.Raise();
            }

            if (Input.GetMouseButtonUp(0))
            {
                leftClickUp.Raise();
            }

            if (Input.GetMouseButtonDown(1))
            {
                rightClickDown.Raise();
            }
        }
    }
}
