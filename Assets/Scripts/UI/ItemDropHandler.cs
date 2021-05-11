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
                OnDropCall(hit);
            }
        }

        public virtual void OnDropCall(RaycastHit2D hit) {}
    }
}
