using UnityEngine;

namespace TD
{
    public class MerchantMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject panel;

        [SerializeField]
        private GameObject sellButton;

        [SerializeField]
        private Resource_SO resources;

        [SerializeField]
        private SelectedTurret selected;

        public void OpenMerchantMenu()
        {
            if (panel != null)
            {
                Animator animator = panel.GetComponent<Animator>();
                if (animator != null)
                {
                    bool isOpen = animator.GetBool("open");

                    animator.SetBool("open", !isOpen);
                }
            }
        }

        public void OnSellButtonPressed()
        {
            this.resources.currentGold += this.resources.currentParts;
            this.resources.currentParts = 0;
            this.gameObject.GetComponent<UI>().SetGold();
            this.gameObject.GetComponent<UI>().SetParts();
        }

        public void OnUpgradeFireRate()
        {
            this.selected.Selected.GetComponent<Turret>().bulletSpeed += 1f;
        }
    }
}
