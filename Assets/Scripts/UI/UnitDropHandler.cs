namespace TD
{
    using UnityEngine;

    public class UnitDropHandler : ItemDropHandler
    {
        [SerializeField]
        private Sockets sockets;

        [SerializeField]
        private Turrets turrets;

        public override void OnDropCall(RaycastHit2D hit)
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

