namespace TD
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class UI : Singleton<UI>
    {
        public Button[] playerUnits;
        public GameObject radialMenu;
        public GameObject[] menuHighlights;
        public Camera uiCamera;
        public TMP_Text waveTimerText;
        public TMP_Text gold;
        public TMP_Text parts;

        private int radialMenuOption = -1;
        private GameObject unitSelected;
        private bool showRadialMenu = false;
        private RectTransform radialMenuRect;
        private Canvas canvas;

        // Check to see if we clicked over player Unit
        public void CheckUnitClicked()
        {
            LayerMask mask = LayerMask.GetMask("UnitsClick");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, Mathf.Infinity, mask);
            if (hit.collider)
            {
                if (hit.collider.tag == "unit click area")
                {
                    unitSelected = hit.collider.gameObject.transform.parent.gameObject;
                    showRadialMenu = true;
                }
            }
            MouseClicked();
        }

        // Set collected parts
        public int SetParts
        {
            set { this.parts.text = value.ToString(); }
        }

        // Set collected gold
        public int SetGold
        {
            set { this.gold.text = value.ToString(); }
        }

        // Set time until next wave
        public float SetTimeForNextWave
        {
            set { this.waveTimerText.text = Mathf.Floor(value).ToString(); }
        }

        public string SetLastWave
        {
            set { this.waveTimerText.text = value; }
        }

        // What happens when mouse is pressed to radial menu
        private void MouseClicked()
        {
            radialMenuOption = -1;
            if (showRadialMenu)
            {
                Vector2 anchoredPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(unitSelected.transform.position), uiCamera, out anchoredPos);
                radialMenuRect.anchoredPosition = anchoredPos;
            }
            radialMenu.SetActive(showRadialMenu);
        }

        // What happens when mouse is released to radial menu
        public void MouseReleased()
        {
            this.radialMenu.SetActive(false);
            switch (radialMenuOption)
            {
                case 0:
                    break;
                case 1:
                    unitSelected.GetComponent<PlayerUnit>().CancelUnit();
                    break;
                case 2:
                    Debug.Log("Upgrade");
                    break;
                case 3:
                    Debug.Log("Unknown");
                    break;
            }
            radialMenuOption = -1;
            showRadialMenu = false;
            unitSelected = null;
        }

        // Check for which section of radial menu is mouse over
        private void CheckRadialMenu()
        {
            Vector2 mousePositionAtRadialMenu = Input.mousePosition - Camera.main.WorldToScreenPoint(unitSelected.transform.position);
            mousePositionAtRadialMenu.Normalize();
            float angle = Mathf.Atan2(mousePositionAtRadialMenu.y, -mousePositionAtRadialMenu.x) / Mathf.PI;
            angle *= 180;
            angle += 90f;
            if (angle< 0)
            {
                angle += 360;
            }
            for (int i = 0; i< 4; i++)
            {
                if (angle > i* 90 && angle<(i + 1) * 90)
                {
                    menuHighlights[i].SetActive(true);
                    radialMenuOption = i;
                }
                else
                {
                    menuHighlights[i].SetActive(false);
                }
            }
        }

        // Give all player units drag and drop component
        private void Start()
        {
            for (int i = 0; i < playerUnits.Length; i++)
            {
                playerUnits[i].gameObject.AddComponent<ItemDragHandler>();
                playerUnits[i].gameObject.AddComponent<ItemDropHandler>();
            }
            this.radialMenuRect = this.radialMenu.GetComponent<RectTransform>();
            this.canvas = gameObject.GetComponentInChildren<Canvas>();
        }

        private void Update()
        {
            if (showRadialMenu)
            {
                CheckRadialMenu();
            }
        }
    }
}
