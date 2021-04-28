namespace TD
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ItemDropHandler : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider)
            {
                if (hit.collider.tag == "socket" && !hit.collider.gameObject.GetComponent<Socket>().occupied)
                {
                    GameObject newUnit = Instantiate(PlayerUnitsManager.Instance.playerUnitsTypes[0], hit.collider.transform);
                    for (int i = 0; i < SocketsManager.Instance.Sockets.Count; i++)
                    {
                        if (SocketsManager.Instance.Sockets[i] == hit.collider.gameObject)
                        {
                            SocketsManager.Instance.Sockets[i].GetComponent<Socket>().SetOccupied();
                            SocketsManager.Instance.Sockets[i].GetComponent<Socket>().containsUnit = newUnit;
                            newUnit.GetComponent<PlayerUnit>().inSocket = i;
                        }
                    }
                }
            }
        }
    }
}
