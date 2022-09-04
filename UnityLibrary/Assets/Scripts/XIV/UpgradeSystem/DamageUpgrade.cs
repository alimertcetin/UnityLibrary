using System;
using UnityEngine;

namespace XIV.UpgradeSystem
{
    public class DamageUpgrade : IUpgrade<PlayerUpgrade>
    {
        [SerializeField] int level;
        public int upgradeLevel => level;
        public PlayerUpgrade upgradeType => PlayerUpgrade.Damage;
        
        public bool IsBetterThan(IUpgrade<PlayerUpgrade> other)
        {
            if (other is not DamageUpgrade otherUpgrade) return false;

            return this.upgradeLevel > otherUpgrade.upgradeLevel;
        }

        public bool Equals(IUpgrade<PlayerUpgrade> other)
        {
            if (other is not DamageUpgrade otherUpgrade) return false;
            
            return upgradeLevel == otherUpgrade.upgradeLevel && upgradeType == otherUpgrade.upgradeType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(upgradeLevel, (int)upgradeType);
        }
    }

    public class SpeedUpgrade : IUpgrade<PlayerUpgrade>
    {
        public int upgradeLevel { get; }
        public PlayerUpgrade upgradeType => PlayerUpgrade.Speed;

        public bool Equals(IUpgrade<PlayerUpgrade> other)
        {
            if (other is not SpeedUpgrade otherUpgrade) return false;
            
            return upgradeLevel == otherUpgrade.upgradeLevel && upgradeType == otherUpgrade.upgradeType;
        }
        
        public bool IsBetterThan(IUpgrade<PlayerUpgrade> other)
        {
            if (other is not SpeedUpgrade otherUpgrade) return false;

            return this.upgradeLevel > otherUpgrade.upgradeLevel;
        }
    }
}