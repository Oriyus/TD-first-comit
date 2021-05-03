namespace TD
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ItemDropHandler : MonoBehaviour, IDropHandler
    {
        public LevelEventListener levelListener;

        [SerializeField]
        private Sockets sockets;

        [SerializeField]
        private Turrets turrets;

        public void OnDrop(PointerEventData eventData)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider)
            {
                if (hit.collider.tag == "socket" && !hit.collider.gameObject.GetComponent<Socket>().Occupied)
                {
                    GameObject newUnit = Instantiate(turrets.Items[0], hit.collider.transform);
                    for (int i = 0; i < sockets.Items.Count; i++)
                    {
                        if (sockets.Items[i] == hit.collider.gameObject)
                        {
                            sockets.Items[i].GetComponent<Socket>().Occupied = true;
                            sockets.Items[i].GetComponent<Socket>().Contains = newUnit;
                            newUnit.GetComponent<SocketOccupied>().SocketIndex = i;
                        }
                    }
                }
            }
        }
    }
}
