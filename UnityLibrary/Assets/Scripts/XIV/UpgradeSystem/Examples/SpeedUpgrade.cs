using UnityEngine;
using XIV.UpgradeSystem.Implementation;

namespace XIV.UpgradeSystem.Examples
{
    [CreateAssetMenu(menuName = Constants.MenuName + "SpeedUpgrade")]
    public class SpeedUpgrade : Upgrade<PlayerUpgrade>
    {
        public override bool Equals(IUpgrade<PlayerUpgrade> other)
        {
            if (other is not SpeedUpgrade otherUpgrade) return false;
            
            return upgradeLevel == otherUpgrade.upgradeLevel && upgradeType == otherUpgrade.upgradeType;
        }
        
        public override bool IsBetterThan(IUpgrade<PlayerUpgrade> other)
        {
            if (other is not SpeedUpgrade otherUpgrade) return false;

            return this.upgradeLevel > otherUpgrade.upgradeLevel;
        }

#if UNITY_EDITOR
        protected override void GetName(out string name, out int instanceID)
        {
            name = nameof(SpeedUpgrade) + "_" + upgradeLevel;
            instanceID = this.GetInstanceID();
        }
#endif
    }
}