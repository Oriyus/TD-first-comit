namespace TD
{
    using UnityEngine;

    public class UpgradeDropHandler : ItemDropHandler
    {
        public override void OnDropCall(RaycastHit2D hit)
        {
            // TODO what hapens when we drop upgrade on a turret
            base.OnDropCall(hit);
        }
    }
}
