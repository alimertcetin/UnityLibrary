using UnityEngine;
using XIV.UpgradeSystem.Integration;

namespace XIV.UpgradeSystem.Examples
{
    [CreateAssetMenu(menuName = Constants.MenuName + "SpeedUpgrade")]
    public class SpeedUpgradeSO : UpgradeSO<PlayerUpgrade>
    {
        public override bool Equals(IUpgrade<PlayerUpgrade> other)
        {
            if (other is not SpeedUpgradeSO otherUpgrade) return false;
            
            return upgradeLevel == otherUpgrade.upgradeLevel && upgradeType == otherUpgrade.upgradeType;
        }
        
        public override bool IsBetterThan(IUpgrade<PlayerUpgrade> other)
        {
            if (other is not SpeedUpgradeSO otherUpgrade) return false;

            return this.upgradeLevel > otherUpgrade.upgradeLevel;
        }

#if UNITY_EDITOR
        protected override void GetName(out string name, out int instanceID)
        {
            name = nameof(SpeedUpgradeSO) + "_" + upgradeLevel;
            instanceID = this.GetInstanceID();
        }
#endif
    }
}