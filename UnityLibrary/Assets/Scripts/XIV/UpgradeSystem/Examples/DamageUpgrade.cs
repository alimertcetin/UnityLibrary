using System;
using UnityEngine;
using XIV.UpgradeSystem.Implementation;

namespace XIV.UpgradeSystem.Examples
{
    [CreateAssetMenu(menuName = Constants.MenuName + "DamageUpgrade")]
    public class DamageUpgrade : Upgrade<PlayerUpgrade>
    {
        public override bool IsBetterThan(IUpgrade<PlayerUpgrade> other)
        {
            if (other is not DamageUpgrade otherUpgrade) return false;

            return this.upgradeLevel > otherUpgrade.upgradeLevel;
        }

        public override bool Equals(IUpgrade<PlayerUpgrade> other)
        {
            if (other is not DamageUpgrade otherUpgrade) return false;
            
            return upgradeLevel == otherUpgrade.upgradeLevel && upgradeType == otherUpgrade.upgradeType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(upgradeLevel, (int)upgradeType);
        }

#if UNITY_EDITOR
        protected override void GetName(out string name, out int instanceID)
        {
            name = nameof(DamageUpgrade) + "_" + upgradeLevel;
            instanceID = this.GetInstanceID();
        }
#endif
    }
}