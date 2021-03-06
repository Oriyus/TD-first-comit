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

        [SerializeField]
        private Upgrade[] rateOfFireUpgrades;

        [SerializeField]
        private Upgrade[] splashUpgrades;

        public StaticEvent FireRateUpgrade = null;
        public StaticEvent SplashUpgrade = null;

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
            FireRateUpgrade.Raise();
            //Turret tur = this.selected.Selected.GetComponent<Turret>();
            //if ((tur.RateOfFireTier < this.rateOfFireUpgrades.Length - 1) && (this.resources.currentGold < this.rateOfFireUpgrades[tur.RateOfFireTier + 1].cost))
            //{
            //    Debug.Log("Not enough Gold for Upgrade");
            //    return;
            //}
            //
            //if (tur.RateOfFireTier < this.rateOfFireUpgrades.Length - 1)
            //{
            //    tur.RateOfFireTier += 1;
            //    tur.bulletSpeed = this.rateOfFireUpgrades[tur.RateOfFireTier].rateOfFire;
            //    this.resources.currentGold -= this.rateOfFireUpgrades[tur.RateOfFireTier].cost;
            //    this.gameObject.GetComponent<UI>().SetGold();
            //}
            //else
            //{
            //    Debug.Log("Turret Upgraded to max Fire Rate");
            //}
        }

        public void OnUpgradeSplash()
        {
            SplashUpgrade.Raise();

            //Turret tur = this.selected.Selected.GetComponent<Turret>();
            //if ((tur.SplashTier < this.splashUpgrades.Length - 1) && (this.resources.currentGold < this.splashUpgrades[tur.SplashTier + 1].cost))
            //{
            //    Debug.Log("Not enough Gold for Upgrade");
            //    return;
            //}
            //
            //if (tur.SplashTier < this.splashUpgrades.Length - 1)
            //{
            //    tur.SplashTier += 1;
            //    tur.splashArea = this.splashUpgrades[tur.SplashTier].splash;
            //    this.resources.currentGold -= this.splashUpgrades[tur.SplashTier].cost;
            //    this.gameObject.GetComponent<UI>().SetGold();
            //}
            //else
            //{
            //    Debug.Log("Turret Upgraded to max Area of Effect");
            //}
        }
    }
}
