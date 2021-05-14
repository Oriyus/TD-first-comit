using UnityEngine;

namespace TD
{
    public class TurretSelect : MonoBehaviour
    {
        public void OnTurretSelected()
        {
            LayerMask mask = LayerMask.GetMask("UnitsClick");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, Mathf.Infinity, mask);
            if (hit.collider)
            {
                if (hit.collider.tag == "unit click area")
                {
                    Debug.Log(this.gameObject.name);
                }
            }
        }
    }
}
