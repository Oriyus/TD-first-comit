namespace TD
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        private Canvas canvas;
        private Vector3 start;
        private RectTransform rect;

        public void OnBeginDrag(PointerEventData eventData)
        {
            start = transform.localPosition;
            this.rect = this.gameObject.GetComponent<RectTransform>();
            this.canvas = this.gameObject.GetComponentInParent<Canvas>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = start;
        }
    }
}
