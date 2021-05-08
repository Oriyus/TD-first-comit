namespace TD
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MerchantMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject panel;

        [SerializeField]
        private GameObject sellButton;

        [SerializeField]
        private ReusablePartsResource_SO resources;

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
    }
}
