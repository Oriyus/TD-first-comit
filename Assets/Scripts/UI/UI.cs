using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace TD
{
    public class UI : MonoBehaviour
    {
        public RectTransform unitsParent;
        public GameObject radialMenu;
        public GameObject[] menuHighlights;
        public Camera uiCamera;
        public TMP_Text waveTimerText;
        public TMP_Text goldText;
        public TMP_Text partsText;

        [SerializeField]
        private Resource_SO resources;

        private List<GameObject> playerUnits = new List<GameObject>();
        private int radialMenuOption = -1;
        private GameObject unitSelected;
        private bool showRadialMenu = false;
        private RectTransform radialMenuRect;
        private Canvas canvas;

        // Check to see if we clicked over turret
        public void CheckUnitClicked()
        {
            LayerMask mask = LayerMask.GetMask("UnitsClick");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, Mathf.Infinity, mask);
            if (hit.collider)
            {
                if (hit.collider.tag == "unit click area")
                {
                    this.unitSelected = hit.collider.gameObject.transform.parent.gameObject;
                    this.showRadialMenu = true;
                }
            }

            this.MouseClicked();
        }

        // Set collected parts
        public void AddParts(GameObject obj)
        {
            this.resources.currentParts = this.resources.currentParts + obj.GetComponent<Loot_A>().resources;
            this.SetParts();
        }

        public void SetParts()
        {
            this.partsText.text = this.resources.currentParts.ToString();
        }

        // Set gold
        public void SetGold ()
        {
            this.goldText.text = this.resources.currentGold.ToString();
        }

        // Set time until next wave
        public void SetTimeForNextWave(float value)
        {
            this.waveTimerText.text = Mathf.Floor(value).ToString();
        }

        public void SetLastWave(string text)
        {
            this.waveTimerText.text = text;
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
                    unitSelected.GetComponent<TurretCancel>().CancelUnit(unitSelected.GetComponent<SocketOccupied>().SocketIndex);
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

        private void Update()
        {
            if (showRadialMenu)
            {
                CheckRadialMenu();
            }
        }

        private void Start()
        {
            // Get Radial Menu Reference
            this.radialMenuRect = this.radialMenu.GetComponent<RectTransform>();

            // Get Canvas Reference
            this.canvas = gameObject.GetComponentInChildren<Canvas>();

            // Get All Turrets Buttons Reference
            foreach (Transform child in this.unitsParent)
            {
                this.playerUnits.Add(child.gameObject);
            }

            // Initialize starting resource values in UI
            this.partsText.text = "0";
            this.goldText.text = "0";
        }
    }
}
