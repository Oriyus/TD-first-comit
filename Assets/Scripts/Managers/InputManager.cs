namespace TD
{
    using UnityEngine;

    public class InputManager : MonoBehaviour
    {
        public StaticEvent leftClickDown = null;
        public StaticEvent leftClickUp = null;

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
        }
    }
}
