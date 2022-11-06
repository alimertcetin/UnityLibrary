using System;

namespace XIV.UpgradeSystem.Utils
{
    public static class UpgradeUtils
    {
        public static float BoostUsingPercentage<T>(float currentValue, IUpgrade<T> upgrade)
        where T : Enum
        {
            currentValue += currentValue / 100 * upgrade.upgradePower;
            return currentValue;
        }
    }
}