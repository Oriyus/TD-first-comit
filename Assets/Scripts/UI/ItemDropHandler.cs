namespace TD
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ItemDropHandler : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private Sockets sockets;

        public void OnDrop(PointerEventData eventData)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider)
            {
                if (hit.collider.tag == "socket" && !hit.collider.gameObject.GetComponent<Socket>().occupied)
                {
                    GameObject newUnit = Instantiate(PlayerUnitsManager.Instance.playerUnitsTypes[0], hit.collider.transform);
                    for (int i = 0; i < sockets.Items.Count; i++)
                    {
                        if (sockets.Items[i] == hit.collider.gameObject)
                        {
                            sockets.Items[i].GetComponent<Socket>().SetOccupied();
                            sockets.Items[i].GetComponent<Socket>().containsUnit = newUnit;
                            newUnit.GetComponent<PlayerUnit>().inSocket = i;
                        }
                    }
                }
            }
        }
    }
}
