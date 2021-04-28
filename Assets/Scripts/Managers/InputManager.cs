namespace TD
{
    using UnityEngine;

    public class InputManager : Singleton<InputManager>
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                UI.Instance.CheckUnitClicked();
            }

            if (Input.GetMouseButtonUp(0))
            {
                UI.Instance.MouseReleased();
            }
        }
    }
}
