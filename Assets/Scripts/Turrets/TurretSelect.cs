using UnityEngine;

namespace TD
{
    public class TurretSelect : MonoBehaviour
    {
        [SerializeField]
        private Turrets liveTurrets;

        [SerializeField]
        private SelectedTurret selected;

        [SerializeField]
        private GameObject highlight;

        public void OnTurretSelected()
        {
            LayerMask mask = LayerMask.GetMask("UnitsClick");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, Mathf.Infinity, mask);
            if (hit.collider)
            {
                if (hit.transform.parent.gameObject == this.transform.gameObject)
                {
                    selected.Selected = this.transform.gameObject;
                    for (int i = 0; i < liveTurrets.Items.Count; i++)
                    {
                        if (liveTurrets.Items[i] == selected.Selected)
                        {
                            highlight.SetActive(true);
                        }
                    }
                }
                else
                {
                    highlight.SetActive(false);
                }
            }
        }
    }
}
