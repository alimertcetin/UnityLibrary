using UnityEngine;
using XIV.UpgradeSystem.Implementation;

namespace XIV.UpgradeSystem.Examples
{
    public class Upgradeable : MonoBehaviour
    {
        public UpgradeContainer<PlayerUpgrade> playerUpgrades;
        
        [ContextMenu("Fix Name", false, -1)]
        public virtual void FixName()
        {
            
        }
    }
}
