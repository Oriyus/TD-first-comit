namespace TD
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        private Canvas parentCanvas;
        private Vector3 start;
        private RectTransform rect;
        private LayoutGroup layoutGroup;

        public void OnBeginDrag(PointerEventData eventData)
        {
            start = transform.localPosition;
            this.layoutGroup.enabled = false;
            this.transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            rect.anchoredPosition += eventData.delta / parentCanvas.scaleFactor;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = start;
            this.layoutGroup.enabled = true;
        }

        private void Start()
        {
            this.rect = this.gameObject.GetComponent<RectTransform>();
            this.parentCanvas = this.gameObject.GetComponentInParent<Canvas>();
            this.layoutGroup = this.gameObject.GetComponentInParent<LayoutGroup>();

    }
}
}
