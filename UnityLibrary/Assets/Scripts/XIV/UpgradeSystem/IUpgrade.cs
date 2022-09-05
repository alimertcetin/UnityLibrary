using System;
using System.Linq;

namespace XIV.UpgradeSystem
{
    public interface IUpgrade<T> : IEquatable<IUpgrade<T>>
        where T : Enum
    {
        public T upgradeType { get; }
        public int upgradeLevel { get; }
        public float upgradePower { get; }
        
        public bool IsBetterThan(IUpgrade<T> other);
    }
}
